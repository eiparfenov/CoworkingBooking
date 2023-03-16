namespace CoworkingBooking.ViewModels;

public class ReservationVm
{
    public int Id { get; set; }
    public string? Student { get; set; }
    public string? Equipment { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}