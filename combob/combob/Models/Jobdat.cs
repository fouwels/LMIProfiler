using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace combob.Models
{
		public class Skill
		{
			public string name { get; set; }
			public double value { get; set; }
		}

		public class Scale
		{
			public string id { get; set; }
			public List<Skill> skills { get; set; }
		}

		public class Jobdat
		{
			public string onetcode { get; set; }
			public string friendlyName { get; set; }

			public List<Skill> skillsClean { get; set; }
			public List<double> skillsPreppedForSending { get; set; } 
			public int acceptionValue { get; set; }
			public List<Scale> scales { get; set; }
		}
}
