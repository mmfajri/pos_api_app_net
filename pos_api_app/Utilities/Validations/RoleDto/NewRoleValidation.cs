using pos_api_app.DTOs.RolesDTO;
using FluentValidation;

namespace pos_api_app.Utilities.Validations.RoleDto;

public class NewRoleValidation : AbstractValidator<NewRoleDTO>
{
    public NewRoleValidation()
    {
        RuleFor(attr => attr.Name)
            .NotEmpty().WithMessage("Name is Required");
    }
}
