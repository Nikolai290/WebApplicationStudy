using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Models.FluentValidation {
    public class PositionsAddDTOValidator : AbstractValidator<PositionsAddDTO> {
        public PositionsAddDTOValidator() {
            RuleFor(PositionsAddDTO => PositionsAddDTO.Name).NotNull().NotEmpty().Length(5, 50);
            RuleFor(PositionsAddDTO => PositionsAddDTO.Subname).NotNull().NotEmpty().Length(5, 50);
        }
    }
}
