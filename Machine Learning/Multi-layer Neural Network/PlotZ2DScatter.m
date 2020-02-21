
function PlotZ2DScatter(z,y)
    figure('Name','2D Scatter');
    x=string(y);
    for i=0:9
        scatter(z(y==i,2),z(y==i,3),10,y(y==i,:));hold on;
    end
    xlabel('Z(2)');
    ylabel('Z(3)');
    legend('0','1','2','3','4','5','6','7','8','9');hold off
    %text(y,z(:,2),x);
%%%%
end