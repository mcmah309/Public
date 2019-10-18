function [W,mu] = PCA(X,k)
covx = cov(X);
mu = mean(X,1);
[V,D] = eig(covx);
[d,ind] = sort(diag(D));
Ds = D(ind,ind);
Vs = V(:,ind);

W=Vs(:,end-k+1:end);
end

