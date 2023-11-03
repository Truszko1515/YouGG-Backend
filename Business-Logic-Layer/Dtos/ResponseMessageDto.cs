public class ResponseMessageDto
{
    public Status status { get; set; }
}

public class Status
{
    public string message { get; set; }
    public int status_code { get; set; }
}
