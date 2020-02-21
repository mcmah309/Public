
% read data: call ReadNormalizedOptdigitsDataset
[X_trn, y_trn, X_val, y_val, X_tst, y_tst] = ReadNormalizedOptdigitsDataset("optdigits_train.txt", "optdigits_valid.txt", "optdigits_test.txt");

Hs = [4,8,12,16,20,24];
training_error = zeros(length(Hs),1);
validation_error = zeros(length(Hs),1);

% check training and validation error for each option of H
for i=1:length(Hs)
    H = Hs(i);

    % train MLP using current H using MLPTrain
    [Y_pred,Z,W,V] = MLPTrain(X_trn, y_trn, H);

    % calculate error rate for Y predicted to the training set using CalculateErrorRate
    training_error(i) = CalculateErrorRate(Y_pred,y_trn);

    fprintf('Training set error rate when H=%d: %f\n', H, training_error(i));
    
    % Predict Y for the validation set, using ForwardPropagation
    [Y_pred,Z] = ForwardPropagation(X_val, W, V);

    % calculate error rate for Y predicted to the validation set using CalculateErrorRate
    validation_error(i) = CalculateErrorRate(Y_pred,y_val);
    
    fprintf('Validation set error rate when H=%d: %f\n', H, validation_error(i));
    
end

% Plot training and validation error using PlotTrainingValidationError
    PlotTrainingValidationError(Hs,training_error, validation_error);%figure 1

% train MLP using the best number of hidden units MLPTrain
    [Y_pred,Z,W,V] = MLPTrain(X_trn, y_trn, 4);

% Predict Y for the test set, using ForwardPropagation
    [Y_pred,Z] = ForwardPropagation(X_tst, W, V);

% calculate error rate for Y predicted to the test set using CalculateErrorRate
    test_error = CalculateErrorRate(Y_pred,y_tst);

fprintf('Test set error rate when H=%d: %f\n', Hs(1), test_error);


% Train the MLP with 2 hidden units, using MLPTrain
    [Y_pred,Z,W,V] = MLPTrain(X_trn, y_trn, 2);

% Predict Y for the validation and test set, using ForwardPropagation
    [Y_pred_val,Z_val] = ForwardPropagation(X_val, W, V);
    [Y_pred_tst,Z_tst] = ForwardPropagation(X_tst, W, V);

% Scatter showing Z for the training, validation and test set in
% separate figures, using PlotZ2DScatter for each figure
    PlotZ2DScatter(Z,y_trn);%figure 2
    PlotZ2DScatter(Z_val,y_val);%figure 3
    PlotZ2DScatter(Z_tst,y_tst);%figure 4
 

% Train the MLP with 3 hidden units, using MLPTrain
    [Y_pred,Z,W,V] = MLPTrain(X_trn, y_trn, 3);

% Predict Y for the validation and test set, using ForwardPropagation
    [Y_pred_val,Z_val] = ForwardPropagation(X_val, W, V);
    [Y_pred_tst,Z_tst] = ForwardPropagation(X_tst, W, V);
    
% 3D scatter showing Z for the training, validation and test set in
% separate figures, using PlotZ3DScatter for each figure
    PlotZ3DScatter(Z,y_trn);%figure 5
    PlotZ3DScatter(Z_val,y_val);%figure 6
    PlotZ3DScatter(Z_tst,y_tst);%figure 7
