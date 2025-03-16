using business.Commands.Questions;
using data.Infrastructures;

namespace business.Handlers.Questions;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<QuestionDeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(QuestionDeleteCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.Id);
        if (ques == null) return false;

        _unitOfWork.QuestionRepo.Delete(ques);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
