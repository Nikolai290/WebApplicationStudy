using FluentValidation;
using WebApplication3.Models.Entities;

namespace WebApplication3.Models.FluentValidation {
    public class PositionValidator : AbstractValidator<Position> {
        public PositionValidator() {
            RuleFor(position => position.Name).NotNull().NotEmpty().Length(5, 50);
            RuleFor(position => position.Subname).NotNull().NotEmpty().Length(5, 50);
        }
    }
}
