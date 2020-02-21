
function [dW, dV] = BackwardPropagation(X, y_label, Y_pred, Z, V, eta)
    %A=[n, y_label[n,1]+1]=1
    [row,column]=size(Y_pred);
    A=zeros(row,column);
    g=length(y_label);
    for n=1:g
        A(n, y_label(n,1) +1)=1;
    end
    dV = eta*(Z)'*(A - Y_pred);
    q=[];
    q=(A-Y_pred)*(V(2:end,:))';
    [rows,columns]=size(Y_pred);
    c = ones(1,rows,'double');
    c = c';
    X = [c X] ;
    dW=eta*X'*(q.*Z(:,2:end).*(1-Z(:,2:end)));
    
    
end

