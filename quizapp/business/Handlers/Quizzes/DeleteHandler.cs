using business.Commands;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<DeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.Id);
        if (quiz == null) return false;

        _unitOfWork.QuizRepo.Delete(quiz);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
