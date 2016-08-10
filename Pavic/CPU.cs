using System;
using PASM;

namespace Pavic
{
	public class CPU
	{
		public static CPU cpu;
		
		public int cores
		{
			get
			{
				return engines.Length;
			}
		}
		
		Engine[] engines;
		
		public CPU (uint cores = 1)
		{
			if (cores == 0)
				throw new Exception ("Computer cannot boot without at least one CPU core.");
			
			engines = new Engine[cores];
		}
		
		public void InstantiateCores(Memory ram)
		{
			for (int i = 0; i < cores; i++)
			{
				engines[i] = new Engine ();
				engines[i].setMemory (ram);
			}
		}
		
		public static void Load (string[] pasm, int core)
		{
			cpu.engines[core].Load (pasm);
		}
		
		public static void Execute (int core)
		{
			cpu.engines[core].Execute ();
		}
		
		public static void ExecuteAtLine (int core, int line)
		{
			cpu.engines[core].Execute (line);
		}
	}
}

