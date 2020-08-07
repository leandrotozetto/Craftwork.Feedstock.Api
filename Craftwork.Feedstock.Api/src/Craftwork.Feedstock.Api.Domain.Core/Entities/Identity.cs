using System;

namespace Craftwork.Feedstock.Api.Domain.Core.Entities
{
    public class Identifier
    {
        public readonly Guid Value;

        protected virtual object Actual => this;

        protected Identifier(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Identifier other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            //if (Value == Guid.Empty || other.Value == Guid.Empty)
            //    return false;

            return Value == other.Value;
        }

        public static bool operator ==(Identifier current, Identifier other)
        {
            if (current is null && other is null)
                return true;

            if (current is null || other is null)
                return false;

            return current.Equals(other);
        }

        public static bool operator !=(Identifier current, Identifier other)
        {
            return !(current == other);
        }

        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + Value).GetHashCode();
        }
    }
}