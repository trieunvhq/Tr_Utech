using System;
using System.Linq;
using Newtonsoft.Json;
using QRMSWeb.Utils;

namespace QRMSWeb.Helper
{
    public static class ObjectHelper
    {
        public static T TrimALlStringValueOfProperties<T>(T obj)
        {
            try
            {
                var stringProperties = obj.GetType().GetProperties()
                              .Where(p => p.PropertyType == typeof(string));

                foreach (var stringProperty in stringProperties)
                {
                    string currentValue = (string)stringProperty.GetValue(obj, null);
                    if (CommonUtils.IsHtmlEmpty(currentValue))
                    {
                        currentValue = string.Empty;
                        try
                        {
                            stringProperty.SetValue(obj, currentValue, null);
                        }
                        catch (Exception e) { }
                    }
                    if (!string.IsNullOrEmpty(currentValue))
                    {
                        try
                        {
                            stringProperty.SetValue(obj, currentValue?.Trim(), null);
                        }
                        catch (Exception e) { }
                    }
                }
            }
            catch (Exception e) { }
            return obj;
        }
        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }
}
