using System;

namespace TLMaster.UI.Interops;

public class JSHttpResponse
{
    public int Status { get; set; }
    public string StatusText { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new();
    public string? Body { get; set; }
}
