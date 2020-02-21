
function [Y,Z] = ForwardPropagation(X, W, V)
    [rows,columns] = size(X);
    c = ones(1,rows,'double');
    c = c';
    X = [c X] ;
    f = X*W;
    Z=Sigmoid(f); %hidden output
    Z = [c Z] ;
    t= Z*V;
    [rows,columns] = size(t);
    for n=1:rows
        temp=sum(exp(t(n,:)));
        for i=1:columns
            t(n,i)=exp(t(n,i))/temp;
        end
    end
    Y=t;
end

