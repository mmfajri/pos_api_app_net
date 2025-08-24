using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.AuthDTO;

public class RegisterDTO
{
	public string? Username { get; set; }
	public string? Password { get; set; }

	public static explicit operator Account(RegisterDTO registerDTO){
		return new Account {
			Guid = Guid.NewGuid(),
			UserName = registerDTO.Username,
			Password = registerDTO.Password,
		}
	}
}


