using HotelBookingManager.DataModels;

namespace HotelBookingManager.BookingManager
{
    public interface IBookingManager
    {
        Task AddBooking(string guest, int roomNumberToReserve, DateTime dateToReserve);
        Task<IEnumerable<Room>> GetAvailableRoomsForDate(DateTime dateTime);
        Task<bool> IsRoomAvailable(int roomNumberToCheck, DateTime date);
    }
}