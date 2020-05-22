using System;
using System.Runtime.Serialization;

namespace BonusReportGenerator.CmdClient
{
    [Serializable]
    public class ArgumentParserException : Exception
    {
        public ArgumentParserException(string message) : base(message) { }
        protected ArgumentParserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}