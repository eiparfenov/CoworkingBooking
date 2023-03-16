using CoworkingBooking.Data;
using CoworkingBooking.DataObjects;
using CoworkingBooking.Models;
using CoworkingBooking.ViewModels;
using Mapster;

namespace CoworkingBooking.Services;

public class BookingService
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _configuration;
    
    public BookingService(ApplicationDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task Fill()
    {
        await _db.Equipments.AddRangeAsync(new[]
        {
            new Equipment() { Name = "Ноутбук", Amount = 15 },
            new Equipment() { Name = "Vive Pro", Amount = 2 },
            new Equipment() { Name = "Valve Index", Amount = 1 },
            new Equipment() { Name = "Microsoft hololens 1", Amount = 4 },
        });
        await _db.SaveChangesAsync();
    }

    public async Task<EquipmentVm[]> GetAllEquipment()
    {
        return _db.Equipments.ProjectToType<EquipmentVm>().ToArray();
    }

    public async Task<EquipmentFreeTimeVm> GetEquipmentFreeTime(int equipmentId, DateOnly date)
    {
        var reservations = _db.Reservations.Where(x => equipmentId == x.EquipmentId && date == x.Date);
        var equipment = await _db.Equipments.FindAsync(equipmentId);
        var slots = new int[48];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = equipment!.Amount;
        }

        foreach (var reservation in reservations)
        {
            for (int slot = reservation.StartSlot - 1; slot < reservation.EndSlot; slot++)
            {
                if (slot >= 0 && slot < slots.Length)
                {
                    slots[slot]--;
                }
            }
        }

        var result = new List<int>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] > 0)
            {
                result.Add(i);
            }
        }

        return new EquipmentFreeTimeVm()
        {
            FreeSlots = result.ToArray(),
        };
    }

    public async Task<int?> AddReservation(AddReservationDto reservationDto)
    {
        var reservation = reservationDto.Adapt<Reservation>();
        await _db.Reservations.AddAsync(reservation);
        await _db.SaveChangesAsync();
        return reservation.Id;
    }
}