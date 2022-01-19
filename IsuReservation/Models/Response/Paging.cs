namespace IsuReservation.Models.Response;

public class Paging<T> where T : class
{
    public int ActualPage { get; set; }

    public int RecordsPerPage { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<T> Outcome { get; set; }
}