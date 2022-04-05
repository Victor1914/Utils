namespace Utils.Extensions
{
    using Data;

    public static class GenericExtensions
    {
        public static TItem WithName<TItem>(this TItem enumeration, string newDisplayName)
            where TItem : Enumeration
        {
            var item = Enumeration.FromDisplayName<TItem>(enumeration.DisplayName);
            item.DisplayName = newDisplayName;

            return item;
        }
    }
}
