using System;

namespace Samples.Collections
{
    public sealed class MyKey : IEquatable<MyKey>
    {
        private readonly Int32 _value;
        private readonly String _hint;

        public MyKey(Int32 value, String hint)
        {
            _value = value;
            _hint = hint;
        }

        public override Boolean Equals(Object obj)
        {
            return Equals(obj as MyKey);
        }

        #region Equality members

        public bool Equals(MyKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value && string.Equals(_hint, other._hint);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_value * 397) ^ (_hint != null ? _hint.GetHashCode() : 0);
            }
        }

        public static bool operator ==(MyKey left, MyKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MyKey left, MyKey right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}