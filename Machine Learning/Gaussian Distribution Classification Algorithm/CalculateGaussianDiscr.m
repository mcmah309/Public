
function g = CalculateGaussianDiscr(X, m, S, prior)
g = [];
[rows,columns]=size(X);
i=1;
while(i<rows +1)

    g(i) = -0.5*log(det(S))- 0.5*(X(i,:)*(S^-1)*X(i,:)'-2*X(i,:)*(S^-1)*m' + m*(S^-1)*m')+log(prior);
    i=i+1;
end

end

