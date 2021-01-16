using System.Collections.Generic;
using System.Linq;

namespace Engine.Core.Extensions
{
    public static class HashCodeExtensions
    {
        public static int GetHashCodeOnProperties<T>(this T inspect) => GetHashCodeOnProperties(inspect, new string[0]);

        public static int GetHashCodeOnProperties<T>(this T inspect, IEnumerable<string> ignoreList)
        {
            return inspect.GetType().GetProperties().Where(x => !ignoreList.Contains(x.Name)).Select(o => o.GetValue(inspect)).GetListHashCode();
        }

        public static int GetHashCodeOnFields<T>(this T inspect) => GetHashCodeOnFields(inspect, new string[0]);

        public static int GetHashCodeOnFields<T>(this T inspect, IEnumerable<string> ignoreList)
        {
            return inspect.GetType().GetFields().Where(x => !ignoreList.Contains(x.Name)).Select(o => o.GetValue(inspect)).GetListHashCode();
        }

        public static int GetListHashCode<T>(this IEnumerable<T> sequence)
        {
            return sequence
                .Where(item => item != null)
                .Select(item => item.GetHashCode())
                .Aggregate((total, nextCode) => total ^ nextCode);
        }
    }
}