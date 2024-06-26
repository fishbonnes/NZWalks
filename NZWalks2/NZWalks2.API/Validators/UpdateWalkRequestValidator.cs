﻿using FluentValidation;

namespace NZWalks2.API.Validators
{
    public class UpdateWalkRequestValidator:AbstractValidator<Models.DTO.UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
