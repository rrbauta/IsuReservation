namespace IsuReservation.Models.Response;

public class IsuErrorResponse : IsuBaseModel
{
    public string? ErrorDescription { get; set; }

    public IsuErrorResponse(string? error)
    {
        if (string.IsNullOrEmpty(error)) ErrorDescription = "Data received was not string. Please report urgently.";

        if (error != null && !error.Contains("|"))
            ErrorDescription = $"Error was not a valid formatted resource error string. Raw data: {error}";

        ErrorDescription = error?.Split("|").First().Trim();
    } // ctor
} // class