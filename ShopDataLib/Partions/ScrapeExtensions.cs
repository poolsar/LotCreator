using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopDataLib
{
    public static class ScrapeExtensions
    {
        public static ScrapeStatus GetStatus(this IHaveScrapeStatus ent)
        {
            return (ScrapeStatus)ent.StatusCode;
        }

        public static void SetStatus(this IHaveScrapeStatus ent, ScrapeStatus value)
        {
            ent.StatusCode = (int)value;
        }

        //public static T IfModified<T>(this IHaveScrapeStatus ent, T oval, T nval)
        //{
        //    var modified = !oval.Equals(nval);

        //    if (modified && ent.GetStatus() == ScrapeStatus.Stable)
        //    {
        //        ent.SetStatus(ScrapeStatus.Modified);
        //        return nval;
        //    }

        //    //var entDynamic = (ent as dynamic);
        //    //entDynamic["property"] = dfsd;

        //    //entDynamic.Get


        //    return oval;
        //}


        public static void Set(this IHaveScrapeStatus ent, string propName, object value)
        {
            var type = ent.GetType();
            var prop = type.GetProperty(propName);
            var oldValue = prop.GetValue(ent);

            bool isEqual = true;

            // если свойство entity
            if (value is IHaveScrapeStatus || oldValue is IHaveScrapeStatus)
            {
                if (value != null && oldValue != null)
                {
                    // если value.id == 0 значит устанавливается новая сущность которой небыло в базе
                    // значит получатся не равны
                    isEqual = (value as IHaveScrapeStatus).Id == (oldValue as IHaveScrapeStatus).Id;
                }
                else
                {
                    isEqual = (value == null) && (oldValue == null); // если один нулл а другой нет значит не равны
                }
            }
                // если свойство не ентити
            else
            {
                isEqual = value.Equals(oldValue);
            }

            if (!isEqual)
            {
                ent.GetType().GetProperty(propName).SetValue(ent, value);
            }

            if (!isEqual && ent.GetStatus() == ScrapeStatus.Stable)
            {
                ent.SetStatus(ScrapeStatus.Modified);
            }


            if (!isEqual)
            {
                var hist = new PropertyHystory();
                hist.Entity = type.Name;
                hist.EntityRef = ent;
                hist.EntityId = ent.Id;
                hist.Property = propName;
                hist.OldValue = oldValue == null ? string.Empty : oldValue.ToString();
                hist.NewValue = value == null ? string.Empty : value.ToString();
                hist.DateUpdate = DateTime.Now;


                AddHistory(hist);
            }
        }

        private static void AddHistory(PropertyHystory history)
        {
            var historyList = GetHistory(history.Entity);

            historyList.Add(history);
        }

        private static List<PropertyHystory> GetHistory(string entity)
        {
            List<PropertyHystory> historyList = null;
            if (!History.TryGetValue(entity, out historyList))
            {
                historyList = new List<PropertyHystory>();
                History.Add(entity, historyList);
            }
            return historyList;
        }

        public static Dictionary<string, List<PropertyHystory>> History = new Dictionary<string, List<PropertyHystory>>();

        public static void SaveHistory<T>(this ShopEntities context) where T : IHaveScrapeStatus
        {
            var entityName = typeof (T).Name;
            var history = GetHistory(entityName);

            foreach (var h in history)
            {
                context.PropertyHystorySet.Add(h);
            }
            
            context.SaveChanges();

            history.Clear();
        }

        public static void SaveHistory(this ShopEntities context)
        {
            var allHistory = History.SelectMany(h => h.Value).ToList();

            foreach (var h in allHistory)
            {
                h.EntityId = h.EntityRef.Id;
                context.PropertyHystorySet.Add(h);
            }

            context.SaveChanges();

            History.Clear();
        }
    }
}