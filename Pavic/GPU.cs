using System;
using System.Collections.Generic;
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
		
		public static void Push (int x, int y)
		{
			gpu.vertexStack.Add (new Vertex (new Vector2f(x, y)));
		}
		
		public static void Pop ()
		{
			gpu.vertexStack.RemoveAt(gpu.vertexStack.Count);
		}
		
		public static void Burst ()
		{
			gpu.vertexStack.Clear();
		}
		
		public static void Clear ()
		{
			gpu.screen.Clear ();
		}
		
		public static void DrawQuad ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Quads);
		}
		
		public static void DrawPoint ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Points);
		}
		
		public static void DrawLine ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Lines);
		}
		
		public static void DrawStrip ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.LinesStrip);
		}
		
		public static void DrawTriangle ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.Triangles);
		}
		
		public static void DrawTriangleFan ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.TrianglesFan);
		}
		
		public static void DrawTriangleStrip ()
		{
			gpu.screen.Draw (gpu.vertexStack.ToArray(), PrimitiveType.TrianglesStrip);
		}
		
		public static void Draw ()
		{
			gpu.screen.Display ();
		}
	}
}

