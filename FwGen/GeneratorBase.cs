using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FwGen
{
	public abstract class GeneratorBase
	{
		protected List<Type> Types => new List<Type>();

		public void Add(Type t)
		{
			if (!Types.Contains(t))
				Types.Add(t);
		}

		public void Generate(string path)
		{
			if (!path.EndsWith("\\")) path += "\\";
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			GenerateClassFiles(path);
		}

		protected abstract void GenerateClassFiles(string path);
	}
}
