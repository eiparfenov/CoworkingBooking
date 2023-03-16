using CoworkingBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBooking.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    public ApplicationDbContext(): base()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=booking.db");
    }
}