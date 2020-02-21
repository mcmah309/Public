
function y_pred = PredictWithTree(root_node,x)
    while(isempty(root_node.class))
       if(x(root_node.feature) == 0)
            root_node  = root_node.child0;
       else
           root_node = root_node.child1;
       end
    end
    y_pred=root_node.class;
end

