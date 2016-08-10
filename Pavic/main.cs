using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.IO;
using PASM;

namespace Pavic
{
	public class main
	{
		static void Main (string[] args)
		{
			//Load the default bootStrap
			string[] bootStrap = File.ReadAllLines("/home/daniel/Documents/pavic.p");
			
			foreach (string str in bootStrap)
				Console.WriteLine (str);
			
			//Initialize the ram with 1 MB of RAM
			Memory ram = new Memory (1024*1024);
			
			
			//Initialize the CPU with 4 cores
			CPU.cpu = new CPU (4);
			CPU.cpu.InstantiateCores (ram); //Initialize each core with a reference to the ram
			CPU.Load (bootStrap, 0); //Load the bootStrap code into core 1
			
			
			//Initialize the GPU
			//GPU.gpu = new GPU (800, 600);
			//GPU.gpu.HandleEvents ();
			
			
			//Execute the bootStrap
			CPU.Execute (0);
		}
		
		public static void OnClosed (object sender, EventArgs e)
		{
			GPU.gpu.GetScreen().Close();
		}
	}
}

