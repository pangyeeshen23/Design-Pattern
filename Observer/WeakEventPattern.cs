using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Observer
{
    public class Button
    {
        public event EventHandler? Click;
        public void Fire()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Windows
    {
        public Windows(Button button)
        {
            button.Click += ButtonClicked;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button Clicked (Window Handler)");
        }

        ~Windows()
        {
            Console.WriteLine("Window Destroyed");
        }
    }

    public class WeakEventPattern
    {
        public void Run()
        {
            Button btn = new Button();
            Windows window = new Windows(btn);
            var weakRef = new WeakReference(window);
            btn.Fire();

            Console.WriteLine("Setting window to null");
            window = null;
            Console.WriteLine("Staring GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC Is Done");
            Console.WriteLine($"Is the window alive afterr GC ? { weakRef.IsAlive }");
        }
    }
}
