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
        TextRenderer renderer;
        Machine machine;
        public MainWindow(int Width, int Height) : base (Width, Height)
        {
            Title = "Pavic";
            glstd.GL.window = this;
            machine = new Machine(MemoryCalculator.MegaBytes(1), this);
        }

        protected override void OnLoad(EventArgs e)
        {
            machine.Boot();

            return;

            //renderer = new TextRenderer(Width, Height);
        }

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

        Bitmap text_bmp;
        int text_texture;

        /// <summary>
        /// Uses System.Drawing for 2d text rendering.
        /// </summary>
        public class TextRenderer : IDisposable
        {
            Bitmap bmp;
            Graphics gfx;
            int texture;
            Rectangle dirty_region;
            bool disposed;


            /// <summary>
            /// Constructs a new instance.
            /// </summary>
            /// <param name="width">The width of the backing store in pixels.</param>
            /// <param name="height">The height of the backing store in pixels.</param>
            public TextRenderer(int width, int height)
            {
                if (width <= 0)
                    throw new ArgumentOutOfRangeException("width");
                if (height <= 0)
                    throw new ArgumentOutOfRangeException("height ");
                if (OpenTK.Graphics.GraphicsContext.CurrentContext == null)
                    throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

                bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                    PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            }


            #region Public Members

            /// <summary>
            /// Clears the backing store to the specified color.
            /// </summary>
            /// <param name="color">A <see cref="System.Drawing.Color"/>.</param>
            public void Clear(Color color)
            {
                gfx.Clear(color);
                dirty_region = new Rectangle(0, 0, bmp.Width, bmp.Height);
            }

            /// <summary>
            /// Draws the specified string to the backing store.
            /// </summary>
            /// <param name="text">The <see cref="System.String"/> to draw.</param>
            /// <param name="font">The <see cref="System.Drawing.Font"/> that will be used.</param>
            /// <param name="brush">The <see cref="System.Drawing.Brush"/> that will be used.</param>
            /// <param name="point">The location of the text on the backing store, in 2d pixel coordinates.
            /// The origin (0, 0) lies at the top-left corner of the backing store.</param>
            public void DrawString(string text, Font font, Brush brush, PointF point)
            {
                gfx.DrawString(text, font, brush, point);

                SizeF size = gfx.MeasureString(text, font);
                dirty_region = Rectangle.Round(RectangleF.Union(dirty_region, new RectangleF(point, size)));
                dirty_region = Rectangle.Intersect(dirty_region, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            /// <summary>
            /// Gets a <see cref="System.Int32"/> that represents an OpenGL 2d texture handle.
            /// The texture contains a copy of the backing store. Bind this texture to TextureTarget.Texture2d
            /// in order to render the drawn text on screen.
            /// </summary>
            public int Texture
            {
                get
                {
                    UploadBitmap();
                    return texture;
                }
            }

            #endregion

            // Uploads the dirty regions of the backing store to the OpenGL texture.
            void UploadBitmap()
            {
                if (dirty_region != RectangleF.Empty)
                {
                    System.Drawing.Imaging.BitmapData data = bmp.LockBits(dirty_region,
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.BindTexture(TextureTarget.Texture2D, texture);
                    GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                        dirty_region.X, dirty_region.Y, dirty_region.Width, dirty_region.Height,
                        PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                    bmp.UnlockBits(data);

                    dirty_region = Rectangle.Empty;
                }
            }

            void Dispose(bool manual)
            {
                if (!disposed)
                {
                    if (manual)
                    {
                        bmp.Dispose();
                        gfx.Dispose();
                        if (OpenTK.Graphics.GraphicsContext.CurrentContext != null)
                            GL.DeleteTexture(texture);
                    }

                    disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~TextRenderer()
            {
                Console.WriteLine("[Warning] Resource leaked: {0}.", typeof(TextRenderer));
            }

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

        Font font = new Font("Inconsolata", 13);
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            
            return;
            //renderer.Clear(Color.Black);

            

            //renderer.DrawString("root@localhost~ /> sudo apt-get install cmatrix█", font, Brushes.White, new PointF(0, 0));

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Red);
            GL.Vertex2(Orth(0, 0));

            GL.Color3(Color.Blue);
            GL.Vertex2(Orth(Width, 0));

            GL.Color3(Color.Green);
            GL.Vertex2(Orth(Width, Height));

            GL.Color3(Color.Pink);
            GL.Vertex2(Orth(0, Height));

            GL.End();

            Line(new Vector2(0, 0), new Vector2(Width, Height), Color.Black, Color.White, 5);

            //renderer.Clear(Color.Black);
            //renderer.DrawString("Hello World", font, Brushes.White, new PointF(0, 0));
            //
            //
            //GL.Enable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, renderer.Texture);
            //GL.Begin(BeginMode.Quads);
            //
            //
            //GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1f, -1f);
            //GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1f, -1f);
            //GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1f, 1f);
            //GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1f, 1f);
            //
            //GL.End();

            //Line(new PointF(0, 0), new PointF(Width, Height), Color.White, 2);

            SwapBuffers();
        }
    }
}