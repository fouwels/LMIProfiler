using System.Collections.Generic;

namespace combob.Models.Responses
{
	public class Skill
	{
		public string soc { get; set; }
		public string onet { get; set; }
		public string prediction { get; set; }
	}

	public class OptimalResponse
	{
		public List<Skill> skills { get; set; }
		public List<string> optimum { get; set; }
		public string accuracy { get; set; }
	}
}