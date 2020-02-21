
function I = NodeEntropy(y)
    I=0;
    if(length(y) ~= 0)
        for i=1:10
            w=y(y==i-1);
            w=length(w);
            if(w==0)
                w = length(y);
            end
            I = I + (-w/length(y))*log2(w/length(y));
        end
    end

    %{
    entropy=0;
    for i=1:10
        x=length(y(y==i-1));
        if(x==0 | x == length(y))
            entropy=entropy;
        else
            entropy = entropy - (x/length(y))*log2(x/length(y));
        end
    end
    I=entropy;
    %}
%%%%
end

