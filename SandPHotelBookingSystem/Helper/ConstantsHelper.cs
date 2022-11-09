using HotelBookingManager.DataModels;

namespace HotelBookingManager.Helper
{
    public class ConstantsHelper
    {
        public const int Room101 = 101;
        public const int Room201 = 201;
        public const int Room205 = 205;
        public const int Room207 = 207;
        public const int Room303 = 303;
        public const string Guest1 = "Rock";
        public const string Guest2 = "Kind";
        public const string Guest3 = "Wellt";
        public const string Guest4 = "Green";
        public const string Guest5 = "Fine";
        public readonly static HotelStaff HotelBookingManager1 = new() { HotelStaffId = 25, StaffName = "Ariel Martl" };
        public readonly static HotelStaff HotelBookingManager2 = new() { HotelStaffId = 85, StaffName = "Sparkle Markle" };

        public readonly static DateTime CheckInDate10Dec = new(2022, 12, 10);
        public readonly static DateTime CheckInDate11Dec = new(2022, 12, 11);
    }
}
