
function error_rate = CalculateErrorRate(Y_pred,y_label)
    [row,columns] = size(Y_pred);
    i=1;
    wrong =0;
    largest_value=0;
    largest_prob=0;
    while(i<row+1)
        for j=1:columns
            if(Y_pred(i,j)>largest_prob)
                largest_prob = Y_pred(i,j);
                largest_value = j-1;
            end
        end
        if(y_label(i) ~= largest_value)
            wrong = wrong +1;
        end
        i=i+1;
        largest_prob=0;
        largest_value=0;
    end
    error_rate = wrong/row;
end