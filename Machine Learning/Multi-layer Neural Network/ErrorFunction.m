
function E = ErrorFunction(y_label,Y_pred)
    E=0;
    [row,column]=size(Y_pred);
    g=length(y_label);
    A=zeros(g,column);
    for n=1:g
        A(n, y_label(n,1) +1)=1;
    end
    for n=1:g
        E = E - A(n,:)*log(Y_pred(n,:).');
    end
    %E=E/g;
end
%{
    E=0;
    [row,columns] = size(y_label);
    i=1;
    wrong =0;
    largest_value=0;
    largest_prob=0;
    while(i<row+1)
        for j=1:10
            if(Y_pred(i,j)>largest_prob)
                largest_prob = Y_pred(i,j);
                largest_value = j-1;
            end
        end
        if(y_label(i) ~= largest_value)
            wrong = wrong +1;
        end
        i=i+1;
    end
    E = wrong/row;
end
%}



