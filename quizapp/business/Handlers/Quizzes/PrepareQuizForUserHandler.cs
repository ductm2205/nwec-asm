using business.Commands.Quizzes;
using core.Exceptions;
using core.Models.Responses;
using core.Models.Responses.Quizzes;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class PrepareQuizForUserHandler : BaseHandler<PrepareQuizForUserCommand, QuizPrepareInfoResponse>
{
    public PrepareQuizForUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<QuizPrepareInfoResponse> HandleCommand(PrepareQuizForUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.UserId) ?? throw new EntityNotFoundException();

        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();

        var userResp = new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
        };

        var resp = new QuizPrepareInfoResponse
        {
            Id = Guid.NewGuid(),
            Title = quiz.Title,
            Description = quiz.Description,
            Duration = quiz.Duration,
            ThumbnailUrl = quiz.ThumbnailUrl,
            QuizCode = request.QuizCode,
            User = userResp,
        };

        return resp;
    }

}
