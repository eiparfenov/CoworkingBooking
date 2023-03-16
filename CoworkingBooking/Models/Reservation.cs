namespace CoworkingBooking.Models;

public class Reservation
{
    public int Id { get; set; }
    public string Student { get; set; }
    public int StartSlot { get; set; }
    public int EndSlot { get; set; }
    public DateOnly Date { get; set; }
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
}