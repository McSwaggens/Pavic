using OpenTK.Graphics.OpenGL;
using PASM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gl = OpenTK.Graphics.OpenGL.GL;
namespace Pavic.glstd
{
    class GL
    {
        public static MainWindow window;
        public static void color(Engine engine, byte[] r, byte[] g, byte[] b)
        {
            gl.Color3(r[0], g[0], b[0]);
            //Console.WriteLine($"GL.Color3 {r[0]} {g[0]} {b[0]}");
        }

        public static void test(Engine engine, byte[] dat)
        {
            int test = Convert.ToInt32(dat);
            Console.WriteLine($"{test}");
        }

        public static void vertex_d(Engine engine, byte[] bx, byte[] by)
        {
            double x = BitConverter.ToDouble(bx, 0);
            double y = BitConverter.ToDouble(by, 0);

            gl.Vertex2(x, y);
        }

        public static void clear_color(Engine engine, byte[] r, byte[] g, byte[] b)
        {
            gl.ClearColor(System.Drawing.Color.FromArgb(255, r[0], g[0], b[0]));
        }

        public static void clear(Engine engine)
        {
            gl.Clear(ClearBufferMask.ColorBufferBit);
        }

        public static void begin(Engine engine)
        {
            gl.Begin(PrimitiveType.Quads);
        }

        public static void end(Engine engine)
        {
            gl.End();
        }

        public static void DRAW_TEXT(Engine engine, byte[] text)
        {
            byte a = 1;
            char c = Convert.ToChar(a);
        }

        public static void SWAP_BUFFER(Engine engine)
        {
            window.SwapBuffers();
        }
    }
}
