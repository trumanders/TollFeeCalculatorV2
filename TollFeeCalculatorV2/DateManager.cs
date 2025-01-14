using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class DateManager : IDateManager
{
	private const int YEAR = 2024;
	TimeSpan _yearTimeSpan = new DateTime(YEAR + 1, 1, 1) - new DateTime(YEAR, 1, 1);
	TimeSpan _timeSpan;
	int _passageCount;

	public List<DateTime> GetRandomDates(int passageCount, TimeSpan timeSpan)
	{
		_timeSpan = timeSpan;
		_passageCount = passageCount;

		return GenerateRandomDates();
	}

	private List<DateTime> GenerateRandomDates()
	{
		Random random = new Random();

		var maxStartTimeAfterStartOfYearInSeconds = (int)_yearTimeSpan.TotalSeconds - (int)_timeSpan.TotalSeconds + 1;
		var startDateAfterStartOfYearInSeconds = random.Next(1, maxStartTimeAfterStartOfYearInSeconds + 1);
		var endDateAfterStartOfYearInSeconds = startDateAfterStartOfYearInSeconds + (int)_timeSpan.TotalSeconds;

		return Enumerable.Range(0, _passageCount).Select(i =>
		{
			var randomDateFromStartOfYearInSeconds = random.Next(startDateAfterStartOfYearInSeconds, endDateAfterStartOfYearInSeconds);
			var radomDate = new DateTime(YEAR, 1, 1) + TimeSpan.FromSeconds(randomDateFromStartOfYearInSeconds);

			return radomDate;
		}).OrderBy(date => date).ToList();
	}
}
