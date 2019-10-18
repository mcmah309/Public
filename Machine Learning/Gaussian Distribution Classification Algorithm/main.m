prior1 = 0.2;
prior2 = 0.8;
[X_trn, y_trn, X_tst, y_tst] = ReadData('training_data.txt', 'test_data.txt');


[m1,m2,S1,S2] = CalculateMeanIndepCov(X_trn, y_trn);

g1 = CalculateGaussianDiscr(X_tst, m1, S1, prior1);
g2 = CalculateGaussianDiscr(X_tst, m2, S2, prior2);



error_rate = CalculateErrorRate(g1, g2, y_tst);
disp(strcat("Independent Covariance: ", num2str(error_rate,3)));

[m1,m2,S] = CalculateMeanSameCov(X_trn, y_trn, prior1, prior2);

g1 = CalculateGaussianDiscr(X_tst, m1, S, prior1);
g2 = CalculateGaussianDiscr(X_tst, m2, S, prior2);

error_rate = CalculateErrorRate(g1, g2, y_tst);

disp(strcat("Shared Covariance: ", num2str(error_rate,3)));
