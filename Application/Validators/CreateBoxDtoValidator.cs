using Application.Dto;
using FluentValidation;

namespace Application.Validators
{
    public class CreateBoxDtoValidator : AbstractValidator<CreateBoxDto>
    {
        public CreateBoxDtoValidator()
        {
            #region CutterID
            RuleFor(x => x.CutterID).NotEmpty().WithMessage("Box can not have an empty cutter ID value");
            RuleFor(x => x.CutterID).GreaterThan(0).WithMessage("Box can not have a cutter ID value less than 1");
            RuleFor(x => x.CutterID).LessThan(1000).WithMessage("Box can not have a cutter ID value greater than 999");
            #endregion

            #region Fefco
            RuleFor(x => x.Fefco).NotEmpty().WithMessage("Box can not have an empty fefco value");
            RuleFor(x => x.Fefco).GreaterThan(0).WithMessage("Box can not have a fefco value less than 1");
            RuleFor(x => x.Fefco).LessThan(1000).WithMessage("Box can not have a fefco value greater than 999");
            #endregion

            #region Length
            RuleFor(x => x.Length).NotEmpty().WithMessage("Box can not have an empty length value");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Box can not have a length value less than 1");
            RuleFor(x => x.Length).LessThan(10000).WithMessage("Box can not have a length value greater than 9999");
            #endregion

            #region Width
            RuleFor(x => x.Width).NotEmpty().WithMessage("Box can not have an empty width value");
            RuleFor(x => x.Width).GreaterThan(0).WithMessage("Box can not have a width value less than 1");
            RuleFor(x => x.Width).LessThan(10000).WithMessage("Box can not have a width value greater than 9999");
            #endregion

            #region Height
            RuleFor(x => x.Height).NotEmpty().WithMessage("Box can not have an empty height value");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Box can not have a height value less than 1");
            RuleFor(x => x.Height).LessThan(10000).WithMessage("Box can not have a height value greater than 9999");
            #endregion
        }
    }
}
