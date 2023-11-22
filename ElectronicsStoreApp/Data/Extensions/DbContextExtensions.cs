using System.Linq;

namespace ElectronicsStoreApp.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void UpdateManyToMany<T, KEY>(this ApplicationDbContext context, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, KEY> getKey) where T : class
        {
            if (currentItems != null)
            {
                context.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
                context.Set<T>().AddRange(newItems.Except(currentItems, getKey));
            }
            else
            {
                context.Set<T>().AddRange(newItems);
            }
        }

        public static IEnumerable<T> Except<T, KEY>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, KEY> getKeyFunc)
        {
            return items.GroupJoin(other, getKeyFunc, getKeyFunc, (item, templateItems) => new { item, templateItems })
             .SelectMany(t => t.templateItems.DefaultIfEmpty(), (t, tmp) => new { t, tmp })
             .Where(t => (ReferenceEquals(null, t.tmp) || t.tmp.Equals(default(T)))) 
             .Select(t => t.t.item);
        }
    }
}
