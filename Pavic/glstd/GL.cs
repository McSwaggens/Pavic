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

        public static void vertex_p32(Engine engine, byte[] bx, byte[] by)
        {
            int x = BitConverter.ToInt32(bx, 0);
            int y = BitConverter.ToInt32(by, 0);

            gl.Vertex2(window.Orth(x,y));
        }

        public static void DRAW_SQUARE(Engine engine, byte[] sbx, byte[] sby, byte[] ebx, byte[] eby)
        {
            int sx = BitConverter.ToInt32(sbx, 0), sy = BitConverter.ToInt32(sby, 0), ex = BitConverter.ToInt32(ebx, 0), ey = BitConverter.ToInt32(eby, 0);
            gl.Begin(BeginMode.Quads);
            gl.Vertex2(window.Orth(sx, sy));
            gl.Vertex2(window.Orth(ex, sy));
            gl.Vertex2(window.Orth(ex, ey));
            gl.Vertex2(window.Orth(sx, ey));
            gl.End();
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
            string str = "";
            foreach (byte b in text) str += (char)b;
        }

        public static void SWAP_BUFFER(Engine engine)
        {
            window.SwapBuffers();
        }

        public static byte[] WIDTH_32 (Engine engine)
        {
            return Converter.int32(window.Width);
        }

        public static byte[] HEIGHT_32 (Engine engine)
        {
            return Converter.int32(window.Height);
        }
    }
}
