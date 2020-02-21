
function bestf = SplitAttribute(X,y)
    [row,column]=size(X);
    entropy=[];
    for i=1:column
        y0=y(X(:,i)==0,1);
        y1=y(X(:,i)==1,1);
        Ip = SplitEntropy(y0,y1);
        entropy(i)=Ip;
    end
    min=entropy(1);
    index=1;
    for i=1:length(entropy)
        if(entropy(i) < min)
            min = entropy(i);
            index=i;
        end
    end
    bestf=index;
end

