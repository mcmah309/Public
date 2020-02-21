
function Ip = SplitEntropy(y0,y1)
    Ip1=0;
    Ip0=0;
    if(length(y0) ~= 0)
        for i=1:10
            w=y0(y0==i-1);
            w=length(w);
            if(w==0)
                w = length(y0);
            end
            Ip0 = Ip0 + (-w/length(y0))*log2(w/length(y0));
        end
    end
    if(length(y1) ~= 0)
        for i=1:10
            w=y1(y1==i-1);
            w=length(w);
            if(w==0)
                w = length(y1);
            end
            Ip1 = Ip1 + (-w/length(y1))*log2(w/length(y1));
        end
    end
    Ip = Ip0*(length(y0)/(length(y1)+length(y0))) + Ip1*(length(y1)/(length(y1)+length(y0)));


%{
    x=length(y0)/(length(y0)+length(y1));
    if(x==1 | x==0 | isnan(x))
        y0en=0;
    else
        y0en=-x*log2(x) - (1-x)*log2(1-x);
    end
    
    %x=sum(y1)/length(y1);
    x=length(y1)/(length(y0)+length(y1));
    if(x==1 | x==0 | isnan(x))
        y1en =0;
    else
        y1en=-x*log2(x) - (1-x)*log2(1-x);
    end
    
    Ip=y1en+y0en;
    %}

    %{
    entropy0=0;
    for i=1:10
        x=length(y0(y0==i-1));
        if(x==0 | x == length(y0))
            entropy0=entropy0;
        else
            entropy0 = entropy0 - (x/length(y0))*log2(x/length(y0));
        end
    end
    entropy1=0;
    for i=1:10
        x=length(y1(y1==i-1));
        if(x==0 | x == length(y1))
            entropy1=entropy1;
        else
            entropy1 = entropy1 - (x/length(y1))*log2(x/length(y1));
        end
    end
    Ip = entropy0 + entropy1;

    %}
%%%% 
end

