#if URUTILS_NEWTONSOFT_JSON
using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UrUtils.Extensions
{
    public static class JsonNetExtensions
    {
        public static T ToObjectSafe<T>(this JToken token, T defaultValue = default, bool logException = true, string errorPrefix = "")
        {
            try
            {
                var value = token.Value<T>();
                return value;
            }
            catch (Exception e)
            {
                if (logException)
                    Debug.LogError($"{errorPrefix}Can't convert token to '{typeof(T).FullName}', returning default value [{defaultValue}]. Exception:\n{e}");
                return defaultValue;
            }
        }
    }
}
#endif