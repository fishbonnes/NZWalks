﻿using FluentValidation;

namespace NZWalks2.API.Validators
{
    public class AddWalkDifficultyRequestValidator:AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x=>x.Code).NotEmpty();
        }
    }
}
