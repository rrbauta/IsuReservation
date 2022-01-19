namespace IsuReservation.Models.ViewModel;

public class ContactViewModel
{
    /// <summary>
    ///     Contact identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Contact name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Contact phone number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Contact Birth Date.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    ///     Contact type identifier
    /// </summary>
    public Guid ContactTypeId { get; set; }

    /// <summary>
    ///     Contact type name
    /// </summary>
    public string ContactTypeName { get; set; }
}