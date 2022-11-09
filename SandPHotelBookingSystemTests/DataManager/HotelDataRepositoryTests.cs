using HotelBookingManager.DataModels;
using HotelBookingManager.Helper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingManager.DataManager.Tests
{
    public class HotelDataRepositoryTests
    {
        public readonly static DateTime CheckInDate11Dec = ConstantsHelper.CheckInDate11Dec;

        private readonly HotelDataRepository _sut = new HotelDataRepository();

        public class GetAvailableRoomsForDateTests : HotelDataRepositoryTests
        {
            [Theory]
            [InlineData(ConstantsHelper.Room101, false)]
            [InlineData(ConstantsHelper.Room201, false)]
            [InlineData(ConstantsHelper.Room205, false)]
            [InlineData(ConstantsHelper.Room207, true)]
            [InlineData(ConstantsHelper.Room303, true)]
            public async Task GetAvailableRoomsForDate(int roomNumber, bool isAvailableForDate)
            {
                var availableRooms = await _sut.GetAvailableRoomsForDate(CheckInDate11Dec);

                Assert.Equal(2, availableRooms.Count());
                Assert.Equal(isAvailableForDate, availableRooms.Any(r => r.RoomNumber == roomNumber));
            }
        }

        public class CheckReservationIsPossibleTests : HotelDataRepositoryTests
        {
            [Theory]
            [InlineData(ConstantsHelper.Room101, false)]
            [InlineData(ConstantsHelper.Room207, true)]
            [InlineData(ConstantsHelper.Room303, true)]
            public async void CheckReservationIsPossible(int roomNumber, bool reservationPossibleForRoomAndDate)
            {
                await _sut.InitialiseCacheForGivenDate(CheckInDate11Dec);

                var result = _sut.CheckReservationIsPossible(ConstantsHelper.Guest1, roomNumber, CheckInDate11Dec, new BookingDetails());

                Assert.Equal(reservationPossibleForRoomAndDate, result);
            }
        }
    }
}