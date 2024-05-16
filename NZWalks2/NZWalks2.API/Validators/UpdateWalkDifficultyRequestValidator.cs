using FluentValidation;

namespace NZWalks2.API.Validators
{
    public class UpdateWalkDifficultyRequestValidator:AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
