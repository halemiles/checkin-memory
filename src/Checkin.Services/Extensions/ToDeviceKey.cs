namespace Checkin.Services.Extensions
{
    public static class DeviceKeyExtensions
    {
        public static string ToDeviceKey(this string name)
        {
            return $"device:{name.ToLower()}";
        }
    }
}