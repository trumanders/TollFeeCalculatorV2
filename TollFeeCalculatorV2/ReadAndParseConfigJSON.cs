using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculatorV2
{
	public static class ReadAndParseConfigJSON
	{
		private static readonly string _jsonFilePath = Path.Combine(Path.GetDirectoryName(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName), "TollFeeCalculatorV2/config.json");

		public static Config GetConfig()
		{
			using StreamReader reader = new StreamReader(_jsonFilePath);
			var json = reader.ReadToEnd();
			var config = JsonConvert.DeserializeObject<Config>(json);

			return config;
		}
	}
}
