using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Windowing
{
    public static class WindowManager
    {
        public static List<Window> OpenWindows = new();
        static List<Window> DeferOpen = new();

        public static T? Open<T>() where T : Window, new()
        {
            if (OpenWindows.Any(e => e.GetType() == typeof(T)))
            {
                // Throw away the window created
                return null;
            }
            var wnd = new T();
            wnd.Open = true;
            DeferOpen.Add(wnd);
            wnd.OnOpen();
            return wnd;
        }

        public static void RenderAll()
        {
            OpenWindows = OpenWindows.Concat(DeferOpen).ToList();
            DeferOpen.Clear();

            foreach (var window in OpenWindows)
            {
                window.Render();
            }

            OpenWindows = OpenWindows.Where(e => e.Open).ToList();
        }
    }
}
