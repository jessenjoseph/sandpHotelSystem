using HotelBookingManager.DataManager;
using HotelBookingManager.DataModels;

namespace HotelBookingManager.BookingManager
{
    public class BookingManager : IBookingManager
    {
        private readonly IHotelDataRepository _hotelDataRepository;

        public BookingManager(IHotelDataRepository hotelDataRepository)
        {
            _hotelDataRepository = hotelDataRepository ?? throw new ArgumentNullException(nameof(hotelDataRepository));
        }

        public async Task<bool> IsRoomAvailable(int roomNumberToCheck, DateTime date)
        {
            var availableRoomsForDate = await _hotelDataRepository.GetAvailableRoomsForDate(date);
            return availableRoomsForDate.Any(ar => ar.RoomNumber == roomNumberToCheck);
        }

        public async Task AddBooking(string guest, int roomNumberToReserve, DateTime dateToReserve)
        {
            var newBooking = new BookingDetails(roomNumberToReserve, dateToReserve, guest, "Staff");

            if (!_hotelDataRepository.CheckReservationIsPossible(guest, roomNumberToReserve, dateToReserve, newBooking))
            {
                Console.WriteLine($"Cannot Reserve Room {roomNumberToReserve} for {dateToReserve.Date} as its already reserved For Date");
            }
            else
            {
                await _hotelDataRepository.SaveBooking(newBooking);
            }
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsForDate(DateTime dateTime)
        {
            return await _hotelDataRepository.GetAvailableRoomsForDate(dateTime);
        }
    }
}
