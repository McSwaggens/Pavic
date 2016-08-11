using System;
using System.Collections.Generic;
using PASM;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pavic
{
	public class GPU
	{
		private RenderWindow screen;
		
		public List<Vertex> vertexStack = new List<Vertex>();
		
		public static GPU gpu;
		
		public void HandleEvents ()
		{
			screen.DispatchEvents();
		}
		
		public RenderWindow GetScreen ()
		{
			return screen;
		}
		
		public GPU (uint initWidth, uint initHeight)
		{
			screen = new RenderWindow (new VideoMode(initWidth, initHeight), "Pavic");
			
			screen.Clear(Color.Black);
			screen.Display();
			screen.Closed += main.OnClosed;
		}
		
		public static void Push (Engine engine, byte[] x, byte[] y)
		{
			gpu.vertexStack.Add (new Vertex (new Vector2f(x[0], y[0])));
		}
		
		public static void PushC (Engine engine, byte[] r, byte[] g, byte[] b)
		{
			Vertex v = gpu.vertexStack[gpu.vertexStack.Count-1];
			v.Color = new Color (r[0], g[0], b[0], 255);
			gpu.vertexStack[gpu.vertexStack.Count-1] = v;
		}
		
		public static void PushC_A (Engine engine, byte[] r, byte[] g, byte[] b, byte[] a)
		{
			Vertex v = gpu.vertexStack[gpu.vertexStack.Count-1];
			v.Color = new Color (r[0], g[0], b[0], a[0]);
			gpu.vertexStack[gpu.vertexStack.Count-1] = v;
		}
		
		public static void Pop (Engine engine)
		{
			gpu.vertexStack.RemoveAt(gpu.vertexStack.Count);
		}
		
		public static void Burst (Engine engine)
		{
			gpu.vertexStack.Clear();
		}
		
		public static void Clear (Engine engine)
		{
			gpu.screen.Clear ();
		}
		
		public static void DrawQuad (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Quads);
		}
		
		public static void DrawPoint (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Points);
		}
		
		public static void DrawLine (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Lines);
		}
		
		public static void DrawStrip (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.LinesStrip);
		}
		
		public static void DrawTriangle (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Triangles);
		}
		
		public static void DrawTriangleFan (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.TrianglesFan);
		}
		
		public static void DrawTriangleStrip (Engine engine)
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.TrianglesStrip);
		}
		
		public static void Draw (Engine engine)
		{
			gpu.screen.Display ();
		}
	}
}

