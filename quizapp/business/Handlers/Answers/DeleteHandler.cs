using business.Commands.Answers;
using data.Infrastructures;

namespace business.Handlers.Answers;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<AnswerDeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(AnswerDeleteCommand request, CancellationToken cancellationToken)
    {
        var ans = await _unitOfWork.AnswerRepo.GetByIdAsync(request.Id);
        if (ans == null) return false;

        _unitOfWork.AnswerRepo.Delete(ans);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
