using System.Data;
using FluentValidation;

namespace ADMINPANEL.ViewModels.EditUser
{
    public class EditUserVmValidator : AbstractValidator<EditUserVm>
    {
        public EditUserVmValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}