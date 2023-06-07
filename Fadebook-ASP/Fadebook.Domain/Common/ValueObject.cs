using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Common
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left == null ^ right == null) return false;
            return left?.Equals(right) != false;

        }
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }
        protected abstract IEnumerable<object> GetAtomicValues();
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            var other= (ValueObject)obj;
            var instanceValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();

            while (instanceValues.MoveNext() && otherValues.MoveNext())
            {
                if (instanceValues.Current == null ^ otherValues.Current == null) return false;
                if (instanceValues.Current != null && !instanceValues.Current.Equals(otherValues.Current)) return false;
            }
            return instanceValues.MoveNext() && otherValues.MoveNext();
        }
        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

    }
}
