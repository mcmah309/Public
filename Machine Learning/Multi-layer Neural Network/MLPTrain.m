
function [Y_pred,Z,W,V] = MLPTrain(X_trn, y_trn, H)

    K = 10;
    D = size(X_trn,2);
    maxiter = 1000;
    eta = 0.05;

    rng(1);
    W = -0.01 + (0.01+0.01)*rand(D+1,H);
    rng(2);
    V = -0.01 + (0.01+0.01)*rand(H+1,K);

    % ForwardPropagation and ErrorFunction
    [Y_pred,Z] = ForwardPropagation(X_trn, W, V);
    E= ErrorFunction(y_trn,Y_pred);

    % Batch Gradient Descent
    for iter=1:maxiter

        % Find updates dW and dV, using BackwardPropagation, and update W
        % and V
        [dW, dV] = BackwardPropagation(X_trn, y_trn, Y_pred, Z, V, eta);
        W=W+dW;
        V=V+dV;

        % Find new Y and Z, using ForwardPropagation
        % Calculate new value of error function, using ErrorFunction
        [Y_pred,Z] = ForwardPropagation(X_trn, W, V);
        E_new = ErrorFunction(y_trn,Y_pred);

        % Check convergence and stop if abs(E_new-E) <= 0.2
        if (abs(E_new-E) <= 0.2)
            %fprintf("%d\n",iter);
            return
        end
        E = E_new;
    end
    temp =1 ;
end
