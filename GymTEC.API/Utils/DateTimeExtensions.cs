namespace GymTEC.API.Utils
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }
    }
}
