
function PlotTrainingValidationError(Hs,training_error, validation_error)
        figure('Name','Training Plot Validation');
        scatter(Hs, training_error,'*','g');hold on;
        scatter(Hs, validation_error,'*','r');hold on;
        xlabel('Number of Hidden Nodes');
        ylabel('Error Rate');
        legend('Training Error','Validation Error');hold off

end
