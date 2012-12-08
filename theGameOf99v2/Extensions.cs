using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using theGameOf99v2.Interfaces;

namespace theGameOf99v2
{
    public static class Extensions
    {
        public static T GetItem<T>(this Control.ControlCollection collection, int id)
            where T: ISelectable
        {
            return collection.OfType<T>().First(x => x.Id == id);
        }

        public static IEnumerable<T> GetItems<T>(this Control.ControlCollection collection, int firstId, int lastId)
             where T: ISelectable
        {
            return
                collection.OfType<T>()
                          .Where(x => x.Id >= firstId && x.Id < lastId);
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            var count = 0;
            foreach (var item in collection)
            {
                action(item, count++);
            }
        }
    }
}