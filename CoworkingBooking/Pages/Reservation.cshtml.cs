using CoworkingBooking.Data;
using CoworkingBooking.Shared;
using CoworkingBooking.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBooking.Pages;

public class ReservationPageModel : PageModel
{
    public ReservationVm? Reservation { get; set; }
    private ApplicationDbContext _db;

    public ReservationPageModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async void OnGet([FromRoute] int reservationId)
    {
        var reserv = await _db.Reservations
            .Include(t => t.Equipment)
            .FirstOrDefaultAsync(x => x.Id == reservationId);
        if(reserv == null) return;

        Reservation = new ReservationVm()
        {
            StartTime = SlotToTimeParser.TimeFromSlot(reserv.StartSlot),
            EndTime = SlotToTimeParser.TimeFromSlot(reserv.EndSlot),
            Date = reserv.Date,
            Equipment = reserv.Equipment.Name,
            Student = reserv.Student
        };
    }
}