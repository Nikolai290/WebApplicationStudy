using FluentValidation;
using WebApplication3.Models.ViewModels.Positions;

namespace WebApplication3.Models.FluentValidation {
    public class PositionsViewModelValidator : AbstractValidator<PositionsViewModel> {
        public PositionsViewModelValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(5, 50);
            RuleFor(x => x.Subname).NotNull().NotEmpty().Length(5, 50);
        }
    }
}
