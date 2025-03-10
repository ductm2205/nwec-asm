using System;
using business.Commands;
using data.Infrastructures;

namespace business.Handlers.Questions;

public class DeleteHandler : BaseHandler<DeleteCommand, bool>
{
    public DeleteHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.Id);
        if (ques == null) return false;

        _unitOfWork.QuestionRepo.Delete(ques);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
