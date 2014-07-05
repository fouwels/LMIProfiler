using System.Collections.Generic;

namespace combob.Models
{
	public class DataAvailable
	{
		public bool skills { get; set; }
		public bool abilities { get; set; }
		public bool interests { get; set; }
	}

	public class OnetCode
	{
		public string code { get; set; }
		public List<string> occupations { get; set; }
		public DataAvailable data_available { get; set; }
	}

	public class Soc2Onet
	{
		public int soc { get; set; }
		public List<OnetCode> onetCodes { get; set; }
	}
}