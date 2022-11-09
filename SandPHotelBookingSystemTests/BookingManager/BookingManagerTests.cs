using HotelBookingManager.DataManager;
using HotelBookingManager.DataModels;
using HotelBookingManager.Helper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingManager.BookingManager.Tests
{
    public class BookingManagerTests
    {
        private readonly Mock<IHotelDataRepository> _mockDataRepository;

        public readonly static DateTime CheckInDate10Dec = new(2022, 12, 10);

        private readonly BookingManager _sut;
        public BookingManagerTests()
        {
            _mockDataRepository = new Mock<IHotelDataRepository>();

            _mockDataRepository
                .Setup(l => l.SaveBooking(It.IsAny<BookingDetails>()))
                .ReturnsAsync(true);

            var availableRoomsForDate = new List<Room>()
            {
                new Room(ConstantsHelper.Room207, "2nd"),
                new Room(ConstantsHelper.Room303, "3rd")
            };

            _mockDataRepository
                .Setup(l => l.GetAvailableRoomsForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(availableRoomsForDate);

            _mockDataRepository
                .Setup(f => f.CheckReservationIsPossible(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<BookingDetails>()))
                .Returns(true);

            _sut = new BookingManager(_mockDataRepository.Object);
        }

        public class TheConstructorTests : BookingManagerTests
        {
            [Fact]
            public void ShouldThrowIfInputsMissing()
            {
                Assert.Throws<ArgumentNullException>(() => new BookingManager(null));
            }
        }

        public class IsRoomAvailableTests :BookingManagerTests
        {
            [Theory]
            [InlineData(ConstantsHelper.Room207, true)]
            [InlineData(ConstantsHelper.Room101, false)]
            public async Task IsRoomAvailable(int roomNumber, bool expectedResult)
            {
                var actualResult = await _sut.IsRoomAvailable(roomNumber, CheckInDate10Dec);
                Assert.Equal(expectedResult, actualResult);
            }
        }

        public class AddBookingFacts : BookingManagerTests
        {
            [Theory]
            [InlineData(ConstantsHelper.Guest1, ConstantsHelper.Room207, true)]
            [InlineData(ConstantsHelper.Guest3, ConstantsHelper.Room303, true)]
            [InlineData(ConstantsHelper.Guest5, ConstantsHelper.Room101, false, 0)]
            public async Task AddBookingForDate(string guestName, int roomNumber, bool reservationPossible, int expectedCallCount = 1)
            {
                _mockDataRepository
                    .Setup(f => f.CheckReservationIsPossible(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<BookingDetails>()))
                    .Returns(reservationPossible);

                await _sut.AddBooking(guestName, roomNumber, CheckInDate10Dec);

                _mockDataRepository
                    .Verify(
                        m => m.SaveBooking(
                            It.Is<BookingDetails>(d => d.RoomNumber == roomNumber && d.ReservationDate == CheckInDate10Dec && d.GuestLastName == guestName)),
                        Times.Exactly(expectedCallCount));
            }
        }
    }
}