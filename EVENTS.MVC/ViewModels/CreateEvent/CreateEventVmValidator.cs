using System;
using EVENTS.MVC.Validators;
using FluentValidation;

namespace EVENTS.MVC.ViewModels.CreateEvent
{
    public class CreateEventVmValidator : AbstractValidator<CreateEventVm>
    {
        public CreateEventVmValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(500).NotEmpty();
            
            RuleFor(x => x.Title)
                .MaximumLength(30).NotEmpty();

            RuleFor(x => x.Starts > DateTime.Now).NotEmpty();
            RuleFor(x => x.Ends > DateTime.Now).NotEmpty();

            RuleFor(x => x.Starts).LessThan(x=>x.Ends);
            
            RuleFor(x => x.Photo)
                .Must(value => value.ContentType.Contains("image"))
                .WithMessage("invalid file type")
                .SetValidator(new FileValidator());
        }
    }
}