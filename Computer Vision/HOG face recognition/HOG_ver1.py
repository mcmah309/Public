from cv2 import cv2
import numpy as np
import matplotlib.pyplot as plt
import math

def get_differential_filter():
    filter_x = [[1,1,1],
                [0,0,0],
                [-1,-1,-1]]
    filter_y = [[1,0,-1],
                [1,0,-1],
                [1,0,-1]]
    return filter_x, filter_y


def filter_image(im, filter):
    im_f = np.zeros((np.size(im,1),np.size(im,0)))
    #padding
    im_temp = cv2.copyMakeBorder(
        im,
        top=1,
        bottom=1,
        left=1,
        right=1,
        borderType=cv2.BORDER_REPLICATE
    )
    #
    for i in range(0, np.size(im,0)-3):
        for j in range(0, np.size(im,1)-3): #x-direction
            #w=im_temp[j:j+3,i:i+3]
            #o=np.multiply(im_temp[j:3,i:3],filter)
            im_f[j,i] = np.sum(np.multiply(im_temp[j:j+3,i:i+3],filter))
    return im_f


def get_gradient(im_dx, im_dy):
    grad_mag=np.zeros((np.size(im_dx,1),np.size(im_dx,0)))
    grad_angle=np.zeros((np.size(im_dx,1),np.size(im_dx,0)))
    y=np.size(im_dx,0)
    x=np.size(im_dx,1)
    for i in range(0, y):
        for j in range(0, x): #x-direction
            grad_mag[j,i]=math.sqrt(math.pow(im_dx[j,i],2)+math.pow(im_dy[j,i],2))
            grad_angle[j,i]=(math.atan2(im_dy[j,i],(im_dx[j,i])))*(180/math.pi)
            ##grad_angle[j,i]=abs(math.atan(im_dy[j,i]/(im_dx[j,i] + 0.001)))*(180/math.pi)
            if(grad_angle[j,i] < 0):
                grad_angle[j,i]+=180
    #imgplot = plt.imshow(grad_mag)
    #plt.show()
    #imgplot2 = plt.imshow(grad_angle)
    #plt.show()
    #print(grad_mag)
    return grad_mag, grad_angle


def build_histogram(grad_mag, grad_angle, cell_size):
    M=math.floor(np.size(grad_mag,0)/cell_size)
    N=math.floor(np.size(grad_mag,1)/cell_size) #x-direction
    ori_histo=np.zeros((N,M,6))
    for i in range(0,M):
        for j in range (0,N):
            for x in range(0,cell_size):
                for y in range(0,cell_size):
                    if((grad_angle[x+cell_size*i,y+cell_size*j] >= 165 and grad_angle[x+cell_size*i,y+cell_size*j] <180) or (grad_angle[x+cell_size*i,y+cell_size*j] >=0 and grad_angle[x+cell_size*i,y+cell_size*j]<15)):
                        ori_histo[i,j,0]+= grad_mag[x+cell_size*i,y+cell_size*j]
                    elif(grad_angle[x+cell_size*i,y+cell_size*j] >= 15 and grad_angle[x+cell_size*i,y+cell_size*j] <45):
                        ori_histo[i,j,1]+= grad_mag[x+cell_size*i,y+cell_size*j]
                    elif(grad_angle[x+cell_size*i,y+cell_size*j] >=45 and grad_angle[x+cell_size*i,y+cell_size*j] <75):
                        ori_histo[i,j,2]+= grad_mag[x+cell_size*i,y+cell_size*j]
                    elif(grad_angle[x+cell_size*i,y+cell_size*j] >=75 and grad_angle[x+cell_size*i,y+cell_size*j] <105):
                        ori_histo[i,j,3]+= grad_mag[x+cell_size*i,y+cell_size*j]
                    elif(grad_angle[x+cell_size*i,y+cell_size*j] >=105 and grad_angle[x+cell_size*i,y+cell_size*j] <135):
                        ori_histo[i,j,4]+= grad_mag[x+cell_size*i,y+cell_size*j]
                    elif(grad_angle[x+cell_size*i,y+cell_size*j] >=135 and grad_angle[x+cell_size*i,y+cell_size*j] <165):
                        ori_histo[i,j,5]+= grad_mag[x+cell_size*i,y+cell_size*j]
    return ori_histo


def get_block_descriptor(ori_histo, block_size):
    # To do
    x=math.floor(np.size(ori_histo,1)-(block_size-1))
    y=math.floor(np.size(ori_histo,0)-(block_size-1))
    zed=int(math.pow(block_size,2))
    ori_histo_normalized=np.zeros((x,y,zed, 6))#6 for the number of bins, each block has a normalized histogram for each block, hence zed
    denominator=0
    count=0
    for i in range(0,x):
        for j in range(0,y):
            for z in range(0, block_size):
                for w in range(0, block_size):
                    for k in range(0,6):
                        denominator+= math.pow(ori_histo[i+w,j+z, k],2)
            denominator=math.sqrt(denominator+math.pow(0.001,2))
            for z in range(0, block_size):
                for w in range(0, block_size):
                    for k in range(0,6):
                        ori_histo_normalized[i,j,count,k]=ori_histo[i+w,j+z,k]/denominator
                    count+=1
            count=0
            denominator=0
                
    return ori_histo_normalized


def extract_hog(im):
    # convert grey-scale image to double format
    im = im.astype('float') / 255.0
    #Get differential images using get_differential_filter and filter_image
    filter_x, filter_y = get_differential_filter()
    filteredx = filter_image(im, filter_x)
    filteredy = filter_image(im, filter_y)
    #Compute the gradients using get_gradient
    grad_mag, grad_angle = get_gradient(filteredx,filteredy)
    #Build the histogram of oriented gradients for all cells using build_histogram
    ori_hist = build_histogram(grad_mag, grad_angle, 8)
    #Build the descriptor of all blocks with normalization using get_block_descriptor
    ori_histo_normalized = get_block_descriptor(ori_hist, 2)
    #Return a long vector (hog) by concatenating all block descriptors.
    hog=ori_histo_normalized.flatten()
    # visualize to verify
    visualize_hog(im, hog, 8, 2)

    return hog


# visualize histogram of each block
def visualize_hog(im, hog, cell_size, block_size):
    num_bins = 6
    max_len = 7  # control sum of segment lengths for visualized histogram bin of each block
    im_h, im_w = im.shape
    num_cell_h, num_cell_w = int(im_h / cell_size), int(im_w / cell_size)
    num_blocks_h, num_blocks_w = num_cell_h - block_size + 1, num_cell_w - block_size + 1
    histo_normalized = hog.reshape((num_blocks_h, num_blocks_w, block_size**2, num_bins))
    histo_normalized_vis = np.sum(histo_normalized**2, axis=2) * max_len  # num_blocks_h x num_blocks_w x num_bins
    angles = np.arange(0, np.pi, np.pi/num_bins)
    mesh_x, mesh_y = np.meshgrid(np.r_[cell_size: cell_size*num_cell_w: cell_size], np.r_[cell_size: cell_size*num_cell_h: cell_size])
    mesh_u = histo_normalized_vis * np.sin(angles).reshape((1, 1, num_bins))  # expand to same dims as histo_normalized
    mesh_v = histo_normalized_vis * -np.cos(angles).reshape((1, 1, num_bins))  # expand to same dims as histo_normalized
    plt.imshow(im, cmap='gray', vmin=0, vmax=1)
    for i in range(num_bins):
        plt.quiver(mesh_x - 0.5 * mesh_u[:, :, i], mesh_y - 0.5 * mesh_v[:, :, i], mesh_u[:, :, i], mesh_v[:, :, i],
                   color='white', headaxislength=0, headlength=0, scale_units='xy', scale=1, width=0.002, angles='xy')
    plt.show()


def face_recognition(I_target, I_template):
    x_targ=np.size(I_target,1)
    y_targ=np.size(I_target,0)
    x_temp=np.size(I_template, 1)
    y_temp=np.size(I_template,0)
    ahog=extract_hog(I_template)
    mean=np.mean(ahog)
    ahog=ahog-mean
    anorm=np.linalg.norm(ahog)
    #bounding_boxes=np.zeros((x_targ*y_targ,3))
    bounding_boxes=[]
    x=x_targ - x_temp
    y=y_targ - y_temp
    for i in range(0,y):
        for j in range(0,x):
            b=I_target[i:i+x_temp,j:j+y_temp]
            bhog=extract_hog(b)
            #mean=np.mean(bhog)
            bhog=bhog-np.mean(bhog)
            #bnorm=np.linalg.norm(bhog)
            #bounding_boxes[j*i +j, 0]=j
            #bounding_boxes[j*i +j, 1]=i

            z=(np.dot(ahog,bhog))/(anorm*np.linalg.norm(bhog))
            if(z>0.3):
               bounding_boxes.append([j,i,z])
    area=x_temp*y_temp
    end=[]
    toBeRemoved=[]
    while(True):#Intersection of Union
        Max=0
        xLocation=0
        yLocation=0
        count=0
        for i in bounding_boxes:
            if(i[2] > Max):
                Max = i[2]
                xLocation=i[0]
                yLocation=i[1]
            count+=1
        if(count==0):
            break
        bounding_boxes.remove([xLocation, yLocation, Max])
        end.append([xLocation, yLocation, Max])
        for i in bounding_boxes:
            xLocation2=i[0]
            yLocation2=i[1]
            value=i[2]
            x1=max(xLocation,xLocation2)
            y1=max(yLocation,yLocation2)
            x2=min(xLocation+x_temp, xLocation2+x_temp)
            y2=min(yLocation+y_temp, yLocation2+y_temp)
            #if(x1<=min(xLocation,xLocation2)+x_temp and y1<=min(yLocation,yLocation2)+y_temp):
            intersectionArea=max(x2-x1,0)*max(y2-y1,0)
            IOU=intersectionArea/(2*area - intersectionArea)
            if(IOU > 0.5):
                toBeRemoved.append([xLocation2, yLocation2, value])
        for i in toBeRemoved:
            bounding_boxes.remove(i)
        toBeRemoved=[]
    return  np.asarray(end)


def visualize_face_detection(I_target,bounding_boxes,box_size):

    hh,ww,cc=I_target.shape

    fimg=I_target.copy()
    for ii in range(bounding_boxes.shape[0]):

        x1 = bounding_boxes[ii,0]
        x2 = bounding_boxes[ii, 0] + box_size 
        y1 = bounding_boxes[ii, 1]
        y2 = bounding_boxes[ii, 1] + box_size

        if x1<0:
            x1=0
        if x1>ww-1:
            x1=ww-1
        if x2<0:
            x2=0
        if x2>ww-1:
            x2=ww-1
        if y1<0:
            y1=0
        if y1>hh-1:
            y1=hh-1
        if y2<0:
            y2=0
        if y2>hh-1:
            y2=hh-1
        fimg = cv2.rectangle(fimg, (int(x1),int(y1)), (int(x2),int(y2)), (255, 0, 0), 1)
        cv2.putText(fimg, "%.2f"%bounding_boxes[ii,2], (int(x1)+1, int(y1)+2), cv2.FONT_HERSHEY_SIMPLEX , 0.5, (0, 255, 0), 2, cv2.LINE_AA)


    plt.figure(3)
    plt.imshow(fimg, vmin=0, vmax=1)
    plt.show()




if __name__=='__main__':
    print("start\n")
    im = cv2.imread('cameraman.tif', 0)
    hog = extract_hog(im)

    I_target= cv2.imread('target.png', 0)
    #MxN image

    I_template = cv2.imread('template.png', 0)
    #mxn  face template

    bounding_boxes=face_recognition(I_target, I_template)

    I_target_c= cv2.imread('target.png')
    # MxN image (just for visualization)
    visualize_face_detection(I_target_c, bounding_boxes, I_template.shape[0])
    #visualization code.
