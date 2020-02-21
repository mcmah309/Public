
function [X_trn, y_trn, X_val, y_val, X_tst, y_tst] = ReadData(training_filename, validation_filename, test_filename)
training=load(training_filename);
validation=load(validation_filename);
test=load(test_filename);
X_trn=training(:,1:end-1);
y_trn=training(:,end);
X_val=validation(:,1:end-1);
y_val=validation(:,end);
X_tst=test(:,1:end-1);
y_tst=test(:,end);

end
