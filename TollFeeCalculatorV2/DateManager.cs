using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class DateManager : IDateManager
{
	const int YEAR = 2024;

	public List<DateTime> GetRandomDates(int passageCount, TimeSpan timeSpan)
	{
		TimeSpan yearTimeSpan = new DateTime(2025, 1, 1) - new DateTime(2024, 1, 1);
		if (timeSpan > yearTimeSpan)
		{
			throw new Exception("This program is designed to work with year 2024 only");
		}

		Random random = new Random();
		
		int maxStartTimeAfterStartOfYearInSeconds = (int)yearTimeSpan.TotalSeconds - (int)timeSpan.TotalSeconds + 1;
		int startDateAfterStartOfYearInSeconds = random.Next(1, maxStartTimeAfterStartOfYearInSeconds + 1);
		int endDateAfterStartOfYearInSeconds = startDateAfterStartOfYearInSeconds + (int)timeSpan.TotalSeconds;

		return Enumerable.Range(0, passageCount).Select(i =>
		{
			int randomDateFromStartOfYearInSeconds = random.Next(startDateAfterStartOfYearInSeconds, endDateAfterStartOfYearInSeconds);
			DateTime radomDate = new DateTime(YEAR, 1, 1) + TimeSpan.FromSeconds(randomDateFromStartOfYearInSeconds);

			return radomDate;
		}).OrderBy(date => date).ToList();
	}
}
