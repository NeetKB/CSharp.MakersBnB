namespace MakersBnB.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    //the ? means that the string can be null
    //can help track errors related to specific HTTP requests

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    //purpose is to determine whether the request id should be displayed in the error view
}
