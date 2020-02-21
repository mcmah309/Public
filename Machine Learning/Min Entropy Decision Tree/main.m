%% Read training, validation and test set using ReadData
   [X_trn, y_trn, X_val, y_val, X_tst, y_tst] = ReadData("optdigits_train.txt", "optdigits_valid.txt", "optdigits_test.txt");

%% Parameter selection
thetas=[0.01,0.05,0.1,0.2,0.4,0.8,1.0,2.0];

val_errors = zeros(length(thetas),1);

for theta_idx=1:length(thetas)
    theta = thetas(theta_idx);

    % generate a decision tree for the current theta value using GenerateTree
    node = GenerateTree(X_trn,y_trn,theta);

    % makes predictions for each training datapoint, calling PredictWithTree
    % for each of them. Then, the error rate is found on the training set
    [row, column] = size(X_tst);
    y_pred=[];
    for i=1:row
        y_pred(i) = PredictWithTree(node,X_tst(i,:));
    end
    wrong = 0;
    for i =1:length(y_tst);
        if(y_tst(i) ~= y_pred(i))
            wrong =wrong + 1;
        end
    end
    error_rate = wrong/length(y_tst);

    fprintf('(theta=%f) Training set error rate: %.4f\n', theta, error_rate);

    % makes predictions for each training datapoint, calling PredictWithTree
    % for each of them. Then, the error rate is found on the training set
     [row, column] = size(X_val);
    y_pred=[];
    for i=1:row
        y_pred(i) = PredictWithTree(node,X_val(i,:));
    end
    wrong = 0;
    for i =1:length(y_val);
        if(y_val(i) ~= y_pred(i))
            wrong =wrong + 1;
        end
    end
    error_rate = wrong/length(y_val);
    fprintf('(theta=%f) Validation set error rate: %.4f\n', theta, error_rate);
    val_errors(theta_idx) = error_rate;
end

%% Test set prediction
theta=0.2;
node = GenerateTree(X_trn,y_trn,theta);

% makes predictions for each training datapoint, calling PredictWithTree
% for each of them. Then, the error rate is found on the training set
     [row, column] = size(X_tst);
    y_pred=[];
    for i=1:row
        y_pred(i) = PredictWithTree(node,X_tst(i,:));
    end
    wrong = 0;
    for i =1:length(y_tst);
        if(y_tst(i) ~= y_pred(i))
            wrong =wrong + 1;
        end
    end
    error_rate = wrong/length(y_tst);
fprintf('(theta=%f) Test set error rate: %.4f\n', theta, error_rate);
