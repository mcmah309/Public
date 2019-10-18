function [y_tst_pred, error_rate] = KNN(X_trn, y_trn, X_tst, y_tst, k)

Mdl = fitcknn(X_trn,y_trn,'NumNeighbors',k);
y_tst_pred = predict(Mdl, X_tst);
[row,columns] = size(y_tst);
i=1;
wrong =0;
while(i<row+1)
    if(y_tst(i) ~= y_tst_pred(i))
        wrong = wrong +1;
    end
    i=i+1;
 
end
error_rate = wrong/row;

end

