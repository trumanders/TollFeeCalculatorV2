using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class DateManager : IDateManager
{
	const int YEAR = 2024;

	public List<DateTime> GetRandomDates(int numberOfPassages, TimeSpan timeSpan)
	{
		TimeSpan timeSpanOfYear = new DateTime(2025, 1, 1) - new DateTime(2024, 1, 1);
		if (YEAR != 2024 || timeSpan > new DateTime(2025, 1, 1) - new DateTime(2024, 1, 1))
		{
			throw new Exception("This program is designed to work with year 2024 only");
		}

		Random random = new Random();
		
		int maxStartTimeAfterStartOfYearInSeconds = (int)timeSpanOfYear.TotalSeconds - (int)timeSpan.TotalSeconds + 1;
		int startDateAfterStartOfYearInSeconds = random.Next(1, maxStartTimeAfterStartOfYearInSeconds + 1);
		int endDateAfterStartOfYearInSeconds = startDateAfterStartOfYearInSeconds + (int)timeSpan.TotalSeconds;

		return Enumerable.Range(0, numberOfPassages).Select(i =>
		{
			int randomDateFromStartOfYearInSeconds = random.Next(startDateAfterStartOfYearInSeconds, endDateAfterStartOfYearInSeconds);
			DateTime radomDate = new DateTime(YEAR, 1, 1) + TimeSpan.FromSeconds(randomDateFromStartOfYearInSeconds);

			return radomDate;
		}).OrderBy(date => date).ToList();
	}
}
