%%
% The ReadOptdigitsDataset function reads the Optdigits dataset
%
% The parameters received are:
% - training_filename: string containing the training data file path
% - test_filename: string containing the test data file path
%
% The function should return:
% - X_trn (n_trn x m): Training datapoints matrix, where n_trn is the 
% number of training data points, and m is the number of features
% - y_trn (n_trn x 1): Vector that should contain the labels of the 
% training_filename file (last column)
% - X_tst (n_tst x m): Test datapoints matrix, where n_tst is the number of 
% test data points, and m is the number of features
% - y_tst (n_tst x 1) Vector that should contain the labels of the 
% test_filename file (first column)
%
function [X_trn, y_trn, X_tst, y_tst] = ReadDataset(training_filename, test_filename)
%%%% YOUR CODE STARTS HERE
X_trn=[];
y_trn=[];
X_tst=[];
y_tst=[];
trainingdata = load(training_filename);
testdata = load(test_filename);
X_trn = trainingdata(:,1:end-1);
y_trn = trainingdata(:,end);
X_tst = testdata(:,1:end-1);
y_tst = testdata(:,end);
%%%%
end

