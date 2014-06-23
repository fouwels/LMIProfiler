using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace combob
{
	static class Helpers
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
		public static async Task<string> GetFriendlyName(string soc)
		{
			return JsonConvert.DeserializeObject<SocInfo>(await HttpGet("http://api.lmiforall.org.uk/api/v1/soc/code/" + soc)).title;
		}
		public static async Task<string> HttpGet(string urlIn)
		{
			var request = (HttpWebRequest)WebRequest.Create(urlIn);
			request.Accept = "application/json";

			WebResponse response = await request.GetResponseAsync();
			string temp;

			using (Stream stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
				temp = reader.ReadToEnd();
			return temp;
		}
	}
}
