
function [X_trn, y_trn, X_val, y_val, X_tst, y_tst] = ReadNormalizedOptdigitsDataset(training_filename, validation_filename, test_filename)
X_trn=[];
y_trn=[];
X_val=[];
y_val=[];
y_tst=[];
trainingdata = load(training_filename);
testdata = load(test_filename);
validationdata=load(validation_filename);
X_trn=trainingdata(:,1:end-1);
mean_X_trn=mean(X_trn);
std_X_trn=std(X_trn);
X_trn=(X_trn - mean_X_trn)./(std_X_trn+1e-16);
y_trn = trainingdata(:,end);
X_tst = (testdata(:,1:end-1)-mean_X_trn)./(std_X_trn+1e-16);
y_tst = testdata(:,end);
X_val = (validationdata(:,1:end-1)-mean_X_trn)./(std_X_trn+1e-16);
y_val = validationdata(:,end);


end

