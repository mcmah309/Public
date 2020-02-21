function PlotZ3DScatter(Z,y)
    figure('Name','3D Scatter');
    for i=1:10
        scatter3(Z(y==i,2),Z(y==i,3),Z(y==i,4),10,y(y==i,:));hold on;
    end
    xlabel('Z(2)');
    ylabel('Z(3)');
    zlabel('Z(4)');
    legend('0','1','2','3','4','5','6','7','8','9');hold off
end

