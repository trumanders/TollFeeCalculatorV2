using TollFeeCalculatorV2;

namespace TestDateGenerator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int numberOfPassages = 12;
			TimeSpan timeSpan = new TimeSpan(366, 0, 0, 0);

			List<DateTime> dates = new DateManager().GetRandomDates(numberOfPassages, timeSpan);

			foreach (var date in dates)
			{
				Console.WriteLine(date);		
				
			}
			Console.ReadLine();
		}
	}
}
