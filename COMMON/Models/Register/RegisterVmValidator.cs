using FluentValidation;

namespace Common.Models.Register
{
    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(4).NotEmpty();
            
            RuleFor(x=>x.Password).MinimumLength(4).NotEmpty();

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });
           
        }
    }
}