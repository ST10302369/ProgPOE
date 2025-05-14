using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ProgPOE.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static bool IsAuthenticated(this HttpContext context)
        {
            return context.Session.GetInt32("UserId").HasValue;
        }

        public static bool IsFarmer(this HttpContext context)
        {
            return context.Session.GetString("Role") == "Farmer";
        }

        public static bool IsEmployee(this HttpContext context)
        {
            return context.Session.GetString("Role") == "Employee";
        }

        public static int GetUserId(this HttpContext context)
        {
            return context.Session.GetInt32("UserId") ?? 0;
        }

        public static int? GetFarmerId(this HttpContext context)
        {
            return context.Session.GetInt32("FarmerId");
        }
    }
}