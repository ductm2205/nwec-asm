using business.Commands.Answers;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Answers;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.QuestionId) ?? throw new EntityNotFoundException();

        var ans = await _unitOfWork.AnswerRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        ans.Content = request.Content;
        ans.IsCorrect = request.IsCorrect;
        ans.IsActive = request.IsActive;
        ans.QuestionId = ques.Id;
        ans.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.AnswerRepo.Update(ans);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
