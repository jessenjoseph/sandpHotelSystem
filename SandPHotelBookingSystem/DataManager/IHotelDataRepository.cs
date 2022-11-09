using HotelBookingManager.DataModels;

namespace HotelBookingManager.DataManager
{
    public interface IHotelDataRepository
    {
        Task InitialiseCacheForGivenDate(DateTime sinceDate);
        bool CheckReservationIsPossible(string guest, int roomNumberToReserve, DateTime dateToReserve, BookingDetails newBooking);
        Task<IEnumerable<Room>> GetAvailableRoomsForDate(DateTime dateTime);
        Task<bool> SaveBooking(BookingDetails bookingDetails);
    }
}