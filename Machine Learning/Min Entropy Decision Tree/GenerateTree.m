
function node = GenerateTree(X,y,theta)

    % auxiliar data structure
    node = [];
    node.child0 = []; % will contain a child branch for feature=0
    node.child1 = []; % will contain a child branch for feature=1
    node.feature = []; % will contain the feature used to split this node
    node.class = []; % will contain the predicted class, when the node is a leaf

    % check the node entropy using NodeEntropy
    % if the entropy is smaller than theta, set the "node.class" with the
    % class that occurs with higher frequency in X and return. 
    % the node is a leaf    
    I = NodeEntropy(y);
    [row, column] = size(X);
    if(I<theta)
        node.class = mode(y);
        return
    
    % else (not leaf)
    % finds the feature that will be used to split the non-leaf, using
    % SplitAttribute.
    % set the feature number to the 'feature' attribute of 'node'
    else
        bestf=SplitAttribute(X,y);
        node.feature = bestf;
    end
    
    % split X into X0 and X1 and y into y0 and y1, according to the values 
    % of the selected feature. 
    % Then, call GenerateTree twice. Call once using X0 and y0, and once using
    % X1 and y1
    % set the returned nodes to node.child0 and node.child1
    X0=X(X(:,bestf)==0,:);
    X1=X(X(:,bestf)==1,:);
    y0=y(X(:,bestf)==0,:);
    y1=y(X(:,bestf)==1,:);
    nodex0 = GenerateTree(X0,y0,theta);
    nodex1 = GenerateTree(X1,y1,theta);
    node.child0=nodex0;
    node.child1=nodex1;
    %{
    if(isnan(node.child0.class))
                node.child0.class = mode(y);
    end
    node.child1=nodex1;
    if(isnan(node.child1.class))
                node.child1.class = mode(y);
    end 
    %}

end

