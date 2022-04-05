namespace Utils.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration : IComparable
    {
        public int Value { get; }

        public string DisplayName { get; set; }

        protected Enumeration(int id, string name) => (Value, DisplayName) = (id, name);

        public override string ToString() => DisplayName;

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public int CompareTo(object other) => Value.CompareTo(((Enumeration)other).Value);

        public static IEnumerable<TItem> GetAll<TItem>() where TItem : Enumeration => 
            typeof(TItem)
                .GetFields(BindingFlags.Public | 
                           BindingFlags.Static | 
                           BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<TItem>();

        public static TItem FromValue<TItem>(int value) where TItem : Enumeration 
            => Parse<TItem, int>(value, "value", item => item.Value == value);

        public static TItem FromDisplayName<TItem>(string displayName) where TItem : Enumeration 
            => Parse<TItem, string>(displayName, "display name", item => item.DisplayName == displayName);

        private static TItem Parse<TItem, TValue>(TValue value, string description, Func<TItem, bool> predicate) where TItem : Enumeration 
            => GetAll<TItem>().FirstOrDefault(predicate) ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(TItem)}");
    }
}
