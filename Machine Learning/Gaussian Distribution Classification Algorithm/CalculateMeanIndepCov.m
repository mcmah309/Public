
function [m1,m2,S1,S2] = CalculateMeanIndepCov(X, y)
m1=[];
m2=[];
S1 = [];
S2=[];       
m1=mean(X(y==1,:),1);
m2=mean(X(y==2,:),1);
S1 = cov(X(y==1,:));
S2 = cov(X(y==2,:));
end

