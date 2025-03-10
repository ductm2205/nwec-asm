using business.Commands.Questions;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Questions;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();
        
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        ques.Content = request.Content;
        ques.QuestionType = request.QuestionType;
        ques.QuizId = quiz.Id;
        ques.IsActive = request.IsActive;
        ques.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.QuestionRepo.Update(ques);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
