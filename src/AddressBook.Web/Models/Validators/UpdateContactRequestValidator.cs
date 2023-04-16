using FluentValidation;
using System.Linq;

namespace AddressBook.Api.Models.Validators
{
    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage("Name is required.")
              .MaximumLength(50)
              .WithMessage("Maximum length of name is 50.");
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.")
                .MaximumLength(150)
                .WithMessage("Maximum length of name is 150."); ;
            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of birth is required.");
            RuleFor(x => x.TelephoneNumbers)
                .Must(tn => tn.Any())
                .WithMessage("At least one telephone number is required.")
                .Must(tn => tn.Count <= 10)
                .WithMessage("No more than 10 telephone numbers are allowed.")
                .Must(tn => !tn.GroupBy(x => x).Any(group => group.Count() > 1))
                 .WithMessage("Telephone numbers must not contain duplicates.");
        }
    }
}
