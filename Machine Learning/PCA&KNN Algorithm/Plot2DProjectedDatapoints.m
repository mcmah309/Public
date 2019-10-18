
function Plot2DProjectedDatapoints(X,y_pred)
scatter(X(y_pred==0,1),X(y_pred==0,2),'*','g');hold on;
scatter(X(y_pred==1,1),X(y_pred==1,2),'*','b');hold on;
scatter(X(y_pred==2,1),X(y_pred==2,2),'*','c');hold on;
scatter(X(y_pred==3,1),X(y_pred==3,2),'*','d');hold on;
scatter(X(y_pred==4,1),X(y_pred==4,2),'*','r');hold on;
scatter(X(y_pred==5,1),X(y_pred==5,2),'*','f');hold on;
scatter(X(y_pred==6,1),X(y_pred==6,2),'*','g');hold on;
scatter(X(y_pred==7,1),X(y_pred==7,2),'*','h');hold on;
scatter(X(y_pred==8,1),X(y_pred==8,2),'*','k');hold on;
scatter(X(y_pred==9,1),X(y_pred==9,2),'*','m');hold on;
%%%%
end

