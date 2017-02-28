using System;

namespace AC2C.Common.SerializationToolkit
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate)]
    public class SerializableAttribute : Attribute
    {
    }
}
