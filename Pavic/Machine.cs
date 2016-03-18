using PASM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace Pavic
{
    class Machine
    {
        public long Memory;
        public Engine engine = new Engine();
        public Thread thread;
        private MainWindow window;
        
        public Machine(long memory, MainWindow window)
        {
            this.window = window;
            Memory = memory;
            engine.setMemory((uint)memory);
            string[] code = File.ReadAllLines("/Users/" + Environment.UserName + "/Documents/Pash Projects/test/main.p");
            engine.Load(code);
            engine.ReferenceLibrary(typeof(glstd.GL));
            engine.ReferenceLibrary(typeof(glstd.Standard), typeof(stdlib.Threading));
        }

        public void Shutdown ()
        {
            thread.Abort();
            Environment.Exit(0);
        }

        public void Boot()
        {
            thread = new Thread(() =>
            {
                IGraphicsContext context = new GraphicsContext(GraphicsMode.Default, window.WindowInfo);
                context.MakeCurrent(window.WindowInfo);
                engine.Execute();
            });
            thread.Start();
        }
    }
}
