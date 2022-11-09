namespace HotelBookingManager.DataModels
{
    public class Room
    {
        public Room(int roomNumber, string floor)
        {
            RoomNumber = roomNumber;
            Floor = floor;
        }
        public int RoomNumber { get; set; }
        public string Floor { get; set; }
    }
}
