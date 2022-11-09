using HotelBookingManager.DataModels;
using HotelBookingManager.Helper;
using System.Collections.Concurrent;

namespace HotelBookingManager.DataManager
{
    public class HotelDataRepository : IHotelDataRepository
    {
        private readonly ConcurrentDictionary<string, BookingDetails> _bookingDetailsForRoomAndDateCache = new();

        public async Task InitialiseCacheForGivenDate(DateTime sinceDate)
        {
            if (_bookingDetailsForRoomAndDateCache.Count == 0)
            {
                var reservedRoomsSinceDate = (await GetAllBookings()).Where(rrd => rrd.ReservationDate.Date == sinceDate.Date);

                if (reservedRoomsSinceDate.Any())
                {
                    foreach (var reservedRoomDetails in reservedRoomsSinceDate)
                    {
                        _bookingDetailsForRoomAndDateCache.TryAdd(
                            GetKey(reservedRoomDetails.RoomNumber, reservedRoomDetails.ReservationDate),
                            reservedRoomDetails);
                    }
                }
            }
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsForDate(DateTime dateTime)
        {
            var allBookings = await GetAllBookings();
            var allRooms = await GetAllRooms();
            var bookedRoomsForDate = allBookings.Where(b => b.ReservationDate.Date == dateTime.Date).Select(t => t.RoomNumber).Distinct();
            var availableRoomsForDate = allRooms.Where(ar => !bookedRoomsForDate.Contains(ar.RoomNumber));
            return availableRoomsForDate;
        }

        public bool CheckReservationIsPossible(string guest, int roomNumberToReserve, DateTime dateToReserve, BookingDetails newBooking)
        {
            var roomForDateKey = GetKey(roomNumberToReserve, dateToReserve);
            //currentNewBooking = _bookingDetailsForRoomAndDateCache.GetOrAdd(roomForDateKey, newBooking);

            return _bookingDetailsForRoomAndDateCache.TryAdd(roomForDateKey, newBooking);
        }
        private string GetKey(int roomNumberToReserve, DateTime date) => $"{roomNumberToReserve}_{date.Date}";

        private async Task<IEnumerable<BookingDetails>> GetAllBookings()
        {
            return new List<BookingDetails>()
            {
                new BookingDetails(ConstantsHelper.Room101, ConstantsHelper.CheckInDate10Dec, ConstantsHelper.Guest1, ConstantsHelper.HotelBookingManager1.StaffName),
                new BookingDetails(ConstantsHelper.Room101, ConstantsHelper.CheckInDate11Dec, ConstantsHelper.Guest1, ConstantsHelper.HotelBookingManager1.StaffName),
                new BookingDetails(ConstantsHelper.Room201, ConstantsHelper.CheckInDate11Dec, ConstantsHelper.Guest2, ConstantsHelper.HotelBookingManager1.StaffName),
                new BookingDetails(ConstantsHelper.Room205, ConstantsHelper.CheckInDate11Dec, ConstantsHelper.Guest3, ConstantsHelper.HotelBookingManager2.StaffName)
            };
        }

        private async Task<IEnumerable<Room>> GetAllRooms()
        {
            return new List<Room>()
            {
                new Room(ConstantsHelper.Room101, "1st"),
                new Room(ConstantsHelper.Room201, "2nd"),
                new Room(ConstantsHelper.Room205, "2nd"),
                new Room(ConstantsHelper.Room207, "2nd"),
                new Room(ConstantsHelper.Room303, "3rd")
            };
        }

        public async Task<bool> SaveBooking(BookingDetails bookingDetails)
        {
            throw new NotImplementedException();
        }
    }
}