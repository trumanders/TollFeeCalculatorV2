using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class DateManager : IDateManager
{
	const int YEAR = 2024;
	TimeSpan _yearTimeSpan = new DateTime(YEAR + 1, 1, 1) - new DateTime(YEAR, 1, 1);
	TimeSpan _timeSpan;
	int _passageCount;

	/// <summary>
	/// Generate a list of random DateTime based on the provided TimeSpan and passageCount within the year 2024.
	/// </summary>
	/// <param name="passageCount">The number of passages to generate dates for</param>
	/// <param name="timeSpan">The time span within which the dates should be randomized.</param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public List<DateTime> GetRandomDates(int passageCount, TimeSpan timeSpan)
	{
		_timeSpan = timeSpan;
		_passageCount = passageCount;
		if (timeSpan > _yearTimeSpan)
		{
			throw new Exception("This program is designed to work with year 2024 only");
		}


		return GenerateRandomDates();
	}

	private List<DateTime> GenerateRandomDates()
	{
		Random random = new Random();

		int maxStartTimeAfterStartOfYearInSeconds = (int)_yearTimeSpan.TotalSeconds - (int)_timeSpan.TotalSeconds + 1;
		int startDateAfterStartOfYearInSeconds = random.Next(1, maxStartTimeAfterStartOfYearInSeconds + 1);
		int endDateAfterStartOfYearInSeconds = startDateAfterStartOfYearInSeconds + (int)_timeSpan.TotalSeconds;

		return Enumerable.Range(0, _passageCount).Select(i =>
		{
			int randomDateFromStartOfYearInSeconds = random.Next(startDateAfterStartOfYearInSeconds, endDateAfterStartOfYearInSeconds);
			DateTime radomDate = new DateTime(YEAR, 1, 1) + TimeSpan.FromSeconds(randomDateFromStartOfYearInSeconds);

			return radomDate;
		}).OrderBy(date => date).ToList();
	}
}
