using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Samples.Exceptions
{
    [Serializable]
    class ArgumentEmptyException : ArgumentException
    {
        public IEnumerable Argument { get; }

        public ArgumentEmptyException(String paramName, String message, IEnumerable argument)
            : base(paramName, message)
        {
            Argument = argument;
        }

        protected ArgumentEmptyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Argument = (IEnumerable) info.GetValue(nameof(Argument), typeof(IEnumerable));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Argument), Argument);
        }
    }
}