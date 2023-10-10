using System;

public class SearchDto
{
    public Guid? DeviceId { get; set; }
    public string IpAddress { get; set; }
    public string AttributeKeys { get; set; }
    public bool? IsUp { get; set; }
    public string DeviceName { get; set; }
    public string SortBy { get; set; }
    public bool? SortDescending { get; set; }
}
