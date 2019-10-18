[X_trn, y_trn, X_tst, y_tst] = ReadDataset("optdigits_train.txt", "optdigits_test.txt");

for k=[1,2,3,4]
    [y_tst_pred, error_rate] = KNN(X_trn, y_trn, X_tst, y_tst, k);
    disp(strcat('All features, k=', num2str(k), ' error rate: ', num2str(error_rate,3)));
end
[W,mu] = PCA(X_trn,2);
X_trn_proc = ProjectDatapoints(W,mu,X_trn);
X_tst_proc = ProjectDatapoints(W,mu,X_tst);
figure
for k=[1,2,3,4]
    [y_tst_pred, error_rate] = KNN(X_trn_proc, y_trn, X_tst_proc, y_tst, k);
    disp(strcat('2 principal components, k=', num2str(k), ' error rate: ', num2str(error_rate,3)));
    subplot(2,4,k);
    Plot2DProjectedDatapoints(X_tst_proc,y_tst_pred);
end
[W,mu] = PCA(X_trn,3);
X_trn_proc = ProjectDatapoints(W,mu,X_trn);
X_tst_proc = ProjectDatapoints(W,mu,X_tst);
for k=[1,2,3,4]
    [y_tst_pred, error_rate] = KNN(X_trn_proc, y_trn, X_tst_proc, y_tst, k);    
    disp(strcat('3 principal components, k=', num2str(k), ' error rate: ', num2str(error_rate,3)));
    subplot(2,4,4+k);
    Plot3DProjectedDatapoints(X_tst_proc,y_tst_pred);
end
[rows,columns] = size(X_trn);
[W,mu] = PCA(X_trn,columns);
X_trn_proc = ProjectDatapoints(W,mu,X_trn);
X_tst_proc = ProjectDatapoints(W,mu,X_tst);
for k=[1,2,3,4]
    [y_tst_pred, error_rate] = KNN(X_trn_proc, y_trn, X_tst_proc, y_tst, k);    
    disp(strcat('64 principal components, k=', num2str(k), ' error rate: ', num2str(error_rate,3)));
    subplot(2,4,4+k);
    Plot3DProjectedDatapoints(X_tst_proc,y_tst_pred);
end