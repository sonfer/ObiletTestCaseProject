using System.Runtime.Serialization;

namespace Obilet.Shared.Common;

/// <summary>
/// Servisten dönen metodun statüsünü tutar
/// </summary>
[Serializable]
[DataContract]
public enum ServiceResponseStatuses
{
    [EnumMember]
    Error,

    [EnumMember]
    Success,

    [EnumMember]
    Warning
}