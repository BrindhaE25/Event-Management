namespace EventManagement.Models.Dtos;

public class ResponseDto
{
    public object response { get; set; }
    public string message { get; set; } = "";
    public bool isSuccess { get; set; } = true;
}