using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculatorV2
{
	public class DateManager
	{
		const int YEAR = 2024;
		public List<DateTime> GetRandomDates(int numberOfDates, int month, int day)
		{
			//_textIO.TextOutput("Enter number of dates to generate (max 1000) > ");
			//numberOfDates = _textIO.IntInput(1, 1000);

			//_textIO.TextOutput("Enter month (press enter or invalid for random) > ");
			//month = _textIO.IntInput(1, 12);

			//_textIO.TextOutput("Enter day (press enter or invalid for random) > ");
			//int maxDay = month == 2 ? 29 : (month == 4 || month == 6 || month == 9 || month == 11 ? 30 : 31);
			//day = _textIO.IntInput(1, maxDay);

			Random random = new Random();
			numberOfDates = numberOfDates < 1 ? 1 : numberOfDates;

			return Enumerable.Range(0, numberOfDates).Select(i =>
				new DateTime(
					YEAR,
					month > 0 ? month : random.Next(1, 13),
					day > 0 ? day : random.Next(1, 29),
					random.Next(0, 24),
					random.Next(0, 60),
					random.Next(0, 60)
				)).OrderBy(date => date).ToList();

		}
	}
}
