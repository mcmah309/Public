
function [X_trn, y_trn, X_tst, y_tst] = ReadData(training_filename, test_filename)
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

end

