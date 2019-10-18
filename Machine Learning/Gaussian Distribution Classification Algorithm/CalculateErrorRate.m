
function error_rate = CalculateErrorRate(g1, g2, y_tst)
[row,columns] = size(y_tst);
i=1;
wrong =0;
while(i<row+1)
    if(g1(i) > g2(i) && y_tst(i) ~=1)
        wrong = wrong +1;
    elseif(g1(i) < g2(i) && y_tst(i) ~=2)
        wrong = wrong +1;
    end
    i=i+1;
end
error_rate = wrong/row;

end

