namespace CoworkingBooking.DataObjects;

public class AddReservationDto
{
    public int StartSlot { get; set; }
    public int EndSlot { get; set; }
    public string? Student { get; set; }
    public int EquipmentId { get; set; }
    public DateOnly Date { get; set; }
}