
function [m1,m2,S] = CalculateMeanSameCov(X, y, prior1, prior2)
m1=[];
m2=[];
S1 = [];
S2=[];

x1=[];
x2=[];
S1 = cov(X(y==1,:));
S2 = cov(X(y~=1, :));
m1=mean(X(y==1,:),1);
m2=mean(X(y==2,:),1);

S=prior1*S1 + prior2*S2;

end
