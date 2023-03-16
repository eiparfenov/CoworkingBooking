namespace CoworkingBooking.Shared;

public static class SlotToTimeParser
{
    public static TimeOnly TimeFromSlot(int slot)
    {
        return new TimeOnly(10, 0).AddMinutes(10 * slot);
    }
}