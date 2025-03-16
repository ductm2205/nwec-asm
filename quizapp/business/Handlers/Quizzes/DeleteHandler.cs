using business.Commands.Quizzes;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<QuizDeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(QuizDeleteCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.Id);
        if (quiz == null) return false;

        _unitOfWork.QuizRepo.Delete(quiz);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
