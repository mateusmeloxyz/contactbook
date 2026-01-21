using FluentValidation;
using ContactBook.Application.Commands.AddContact;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Validators
{
    public class AddContactCommandValidator : AbstractValidator<AddContactCommand>
    {
        public AddContactCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email must follow email format");

            RuleFor(c => c.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .MinimumLength(11)
                .WithMessage("Phone number must contain at least 11 digits");
        }

    }
}
