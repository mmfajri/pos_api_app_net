using pos_api_app.Models.Entities;
using pos_api_app.Utilities;

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
			UserName = registerDTO.Username ?? string.Empty,
			Password = Hashing.HashPassword(registerDTO.Password ?? string.Empty),
			Email = registerDTO.Email ?? null
		};

	}
}


