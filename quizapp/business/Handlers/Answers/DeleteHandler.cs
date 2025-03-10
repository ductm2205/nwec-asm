using business.Commands;
using data.Infrastructures;

namespace business.Handlers.Answers;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<DeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var ans = await _unitOfWork.AnswerRepo.GetByIdAsync(request.Id);
        if (ans == null) return false;

        _unitOfWork.AnswerRepo.Delete(ans);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
