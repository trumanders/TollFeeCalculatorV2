using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

    public class DateManager : IDateManager
{
	int _year = DateTime.Now.Year;
	public List<DateTime> GetRandomDates(int numberOfDates)
	{
		if (_year != 2024)
		{
			throw new Exception("This program is designed to work with year 2024 only");
		}
		Random random = new Random();

		return Enumerable.Range(0, numberOfDates).Select(i =>
		{
			int month = random.Next(1, 13);
			int day = random.Next(1, DateTime.DaysInMonth(_year, month));
			int hour = random.Next(0, 24);
			int minute = random.Next(60);
			int second = random.Next(60);

			return new DateTime(_year, month, day, hour, minute, second);
		}).OrderBy(date => date).ToList();
	}
}
