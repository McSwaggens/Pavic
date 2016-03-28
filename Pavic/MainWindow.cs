using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
namespace Pavic
{
    class MainWindow : GameWindow
    {
        Machine machine;
        public MainWindow(int Width, int Height) : base (Width, Height)
        {
            Title = "Pavic";
            glstd.GL.window = this;
            machine = new Machine(MemoryCalculator.MegaBytes(1), this);
        }

        protected override void OnLoad(EventArgs e)
        {
            //machine.Boot();
            //GL.Clear(ClearBufferMask.ColorBufferBit);
            //GL.ClearColor(Color.Black);
            //SwapBuffers();
            //return;

            GL.GenTextures(1, out texture);

        }

        int texture;

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            //renderer.Dispose();
        }

        protected override void OnClosed(EventArgs e)
        {
            Environment.Exit(0);
            machine.Shutdown();
        }
        public void Line(Vector2 pos1, Vector2 pos2, Color color, float width = 1)
        {
            RectangleF lineRectangle = new RectangleF(pos1.X, pos1.Y, pos2.X - pos1.X, pos2.Y - pos1.Y);

            GL.LineWidth(width);
            

            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Begin(PrimitiveType.Lines);

            GL.Color3(color);
            GL.Vertex2(Orth((int)pos1.X, (int)pos1.Y));

            GL.Vertex2(Orth((int)pos2.X, (int)pos2.Y));
            Vector2 vec = new Vector2(0, 0);
            GL.End();
        }

        public void Line(Vector2 pos1, Vector2 pos2, Color color1, Color color2, float width = 1)
        {
            RectangleF lineRectangle = new RectangleF(pos1.X, pos1.Y, pos2.X - pos1.X, pos2.Y - pos1.Y);

            GL.LineWidth(width);


            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Begin(PrimitiveType.Lines);

            GL.Color3(color1);
            GL.Vertex2(Orth((int)pos1.X, (int)pos1.Y));

            GL.Color3(color2);
            GL.Vertex2(Orth((int)pos2.X, (int)pos2.Y));
            Vector2 vec = new Vector2(0, 0);
            GL.End();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(this.ClientRectangle);
            GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            GL.Ortho(0, ClientRectangle.Width, ClientRectangle.Height, 0, -1, 0);

        }

        public Vector2 Orth(int x, int y)
        {
            Vector2 vector = new Vector2();
            vector.X = (float)(x * 2.0 / Width - 1.0);
            vector.Y = (float)(y * -2.0 / Height + 1.0);
            return vector;
        }

        public bool doSwap = false;
        OpenTK.Graphics.TextPrinter textPrinter = new OpenTK.Graphics.TextPrinter(OpenTK.Graphics.TextQuality.High);

        Font font = new Font("Inconsolata", 14);
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //base.OnRenderFrame(e);
            //return;
            //renderer.Clear(Color.Black);


            

            //renderer.DrawString("root@localhost~ /> sudo apt-get install cmatrix█", font, Brushes.White, new PointF(0, 0));

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);
            GL.LoadIdentity();
            //GL.Begin(PrimitiveType.Quads);
            //
            //GL.Color3(Color.Red);
            //GL.Vertex2(Orth(0, 0));
            //
            //GL.Color3(Color.Blue);
            //GL.Vertex2(Orth(Width, 0));
            //
            //GL.Color3(Color.Green);
            //GL.Vertex2(Orth(Width, Height));
            //
            //GL.Color3(Color.Pink);
            //GL.Vertex2(Orth(0, Height));
            //
            //GL.End();

            //Line(new Vector2(0, 0), new Vector2(Width, Height), Color.Black, Color.White, 5);

            textPrinter.Begin();
            textPrinter.Print("UDantu 1.0 | Danix 2.5", font, Color.White);
            OpenTK.Graphics.TextExtents ex = textPrinter.Measure(" ", font);
            int text_width = (int)ex.BoundingBox.Width;
            int text_height = (int)ex.BoundingBox.Height;
            GL.Translate(0F, 20F, 0F);
            textPrinter.Print("local@host: ~> _", font, Color.White);
            //textPrinter.Print("This is printing test to the max", font, Color.White);
            textPrinter.End();
            
            //Line(new PointF(0, 0), new PointF(Width, Height), Color.White, 2);

            SwapBuffers();
        }

        public Bitmap GrabScreenshot()
        {
            if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                throw new OpenTK.Graphics.GraphicsContextMissingException();

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }
    }
}