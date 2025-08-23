namespace pos_api_app.DTOs.ResponseDTO;

public class ResponseDTO<T>
{
	public int StatusCode { get; set; }
	public string Message { get; set; } = string.Empty;
	public T? Data { get; set; }
}
