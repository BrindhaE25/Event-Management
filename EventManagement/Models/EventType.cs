using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventManagement.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum EventType
{
    [EnumMember(Value = "CONFERENCE")]
    CONFERENCE, 
    [EnumMember(Value = "SEMINAR")]
    SEMINAR, 
    [EnumMember(Value = "WORKSHOP")]
    WORKSHOP
}