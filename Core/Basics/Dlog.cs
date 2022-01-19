using System.Diagnostics;

namespace Core.Logging
{
    public static class Dlog
    {
        public static void Info(object? obj)
        {
            if (obj == null)
                Debug.WriteLine("🔹 NULL");
            else
                Debug.WriteLine("🔹 " + obj);
        }

        public static void Warning(object? obj)
        {
            if (obj == null)
                Debug.WriteLine("🔸 NULL");
            else
                Debug.WriteLine("🔸 " + obj);
        }

        public static void Error(object? obj)
        {
            if (obj == null)
                Debug.WriteLine("⛔ NULL");
            else
                Debug.WriteLine("⛔️ " + obj);
        }
    }
}