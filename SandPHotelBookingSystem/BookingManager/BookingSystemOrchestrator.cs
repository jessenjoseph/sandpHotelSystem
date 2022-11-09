using HotelBookingManager.DataManager;
using HotelBookingManager.Helper;

namespace HotelBookingManager.BookingManager
{
    public class BookingSystemOrchestrator
    {
        private readonly IBookingManager _bookingManager;
        private readonly IHotelDataRepository _hotelDataRepository;

        public BookingSystemOrchestrator(IBookingManager bookingManager, IHotelDataRepository hotelDataRepository)
        {
            _bookingManager = bookingManager ?? throw new ArgumentNullException(nameof(bookingManager));
            _hotelDataRepository = hotelDataRepository ?? throw new ArgumentNullException(nameof(hotelDataRepository));
        }

        public async Task<bool> RunBookingSystemAsync(DateTime date)
        {
            try
            {
                var availableRoomsForDate = await _bookingManager.GetAvailableRoomsForDate(date);

                if (!availableRoomsForDate.Any())
                {
                    Console.WriteLine($"No Rooms available to reserve for Date {date}");
                    return false;
                }
                await _hotelDataRepository.InitialiseCacheForGivenDate(date);

                if (await _bookingManager.IsRoomAvailable(ConstantsHelper.Room101, date))
                {
                    await _bookingManager.AddBooking(ConstantsHelper.Guest1, ConstantsHelper.Room101, date);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong {ex.Message}");
            }
        }
    }
}
