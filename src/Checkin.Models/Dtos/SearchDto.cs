using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class SearchDto
{
    public Guid? DeviceId { get; set; }
    public string IpAddress { get; set; }
    public IEnumerable<string> AttributeKeys { get; set; }
    public bool? IsUp { get; set; }
    public string DeviceName { get; set; }
    public string SortBy { get; set; }
    public bool? SortDescending { get; set; }
}
