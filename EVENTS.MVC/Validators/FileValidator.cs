using System;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace EVENTS.MVC.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(f => f.Length)
                .Must(bytes => bytes <= 10_000_000)
                .WithMessage("File Size is larger than allowed");

            RuleFor(f => f.ContentType)
                .Must(ct =>
                    ct.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase)
                    || ct.Equals("image/jpg", StringComparison.OrdinalIgnoreCase)
                    || ct.Equals("image/png", StringComparison.OrdinalIgnoreCase)
                    || ct.Equals("image/gif", StringComparison.OrdinalIgnoreCase)
                ).WithMessage("unsupported file extension");
        }
    }
}