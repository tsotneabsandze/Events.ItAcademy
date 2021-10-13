using System;
using FluentValidation;

namespace ADMINPANEL.ViewModels.Approval
{
    public class ApprovalVmValidator : AbstractValidator<ApprovalVm>
    {
        public ApprovalVmValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CanBeEditedTill).GreaterThan(DateTime.Now);
        }
    }
}