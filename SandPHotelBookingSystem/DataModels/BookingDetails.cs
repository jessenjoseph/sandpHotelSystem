namespace HotelBookingManager.DataModels
{
    public class BookingDetails
    {
        public BookingDetails()
        {
        }

        public BookingDetails(
            int roomNumber,
            DateTime reservationDate,
            string guestLastName,
            string bookedByStaff)
        {
            RoomNumber = roomNumber;
            BookingDate = DateTime.Today;
            ReservationDate = reservationDate;
            GuestLastName = guestLastName;
            ReservedByStaff = bookedByStaff;
        }

        public BookingDetails(
            int roomNumber,
            DateTime bookingDate,
            DateTime reservationDate,
            string guestLastName,
            string reservedByStaff)
        {
            RoomNumber = roomNumber;
            BookingDate = bookingDate;
            ReservationDate = reservationDate;
            GuestLastName = guestLastName;
            ReservedByStaff = reservedByStaff;
        }

        public int RoomNumber { get; set; }
        public double RoomCost { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public string GuestLastName { get; set; }
        public string ReservedByStaff { get; set; }
    }
}