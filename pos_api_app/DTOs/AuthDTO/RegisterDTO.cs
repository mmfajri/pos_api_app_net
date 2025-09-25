using pos_api_app.Models.Entities;

namespace pos_api_app.DTOs.AuthDTO;

public class RegisterDTO
{
	public string? Username { get; set; }
	public string? Password { get; set; }
	public string? Email { get; set; }

	public static explicit operator Account(RegisterDTO registerDTO)
	{
		return new Account
		{
			UserName = registerDTO.Username ?? null,
			Password = registerDTO.Password ?? null,
			Email = registerDTO.Email ?? null
		};

	}
}


