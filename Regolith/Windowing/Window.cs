using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithograph.Windowing
{
    public abstract class Window
    {
        public bool Open = true;

        public virtual void OnOpen() { }
        public abstract void Render();
    }
}
