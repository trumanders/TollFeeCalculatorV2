namespace TollFeeCalculatorV2;

public class TollRateProvider
{
	private const int FEE_LOW = 9;
	private const int FEE_MEDIUM = 16;
	private const int FEE_HIGH = 22;

	/// <summary>
	/// Get the toll rate for a given DateTime based on specific intervals and
	/// whether the date is a toll free date or not.
	/// </summary>
	/// <param name="dateTime">The date to get the toll rate for.</param>
	/// <returns>The fee associated with the given DateTime.</returns>
	public int GetTollRate(DateTime dateTime)
	{
		if (IsTollFreeDate(dateTime))
			return 0;

		TimeSpan time = dateTime.TimeOfDay;

		if ((time >= new TimeSpan(6, 0, 0) && time < new TimeSpan(6, 30, 0)) ||
			(time >= new TimeSpan(8, 30, 0) && time < new TimeSpan(15, 0, 0)) ||
			(time >= new TimeSpan(18, 0, 0) && time < new TimeSpan(18, 30, 0)))
			return FEE_LOW;

		else if ((time >= new TimeSpan(6, 30, 0) && time < new TimeSpan(7, 0, 0)) ||
				 (time >= new TimeSpan(8, 0, 0) && time < new TimeSpan(8, 30, 0)) ||
				 (time >= new TimeSpan(15, 0, 0) && time < new TimeSpan(15, 30, 0)) ||
				 (time >= new TimeSpan(17, 0, 0) && time < new TimeSpan(18, 0, 0)))
			return FEE_MEDIUM;

		else if ((time >= new TimeSpan(7, 0, 0) && time < new TimeSpan(8, 0, 0)) ||
				 (time >= new TimeSpan(15, 30, 0) && time < new TimeSpan(17, 0, 0)))
			return FEE_HIGH;

		else
			return 0;
	}

	/// <summary>
	/// Checks whether a given DateTime is a toll free date.
	/// </summary>
	/// <param name="date">The DateTime to check.</param>
	/// <returns>True if the parameter date is a toll free date. Otherwise false.</returns>
	private bool IsTollFreeDate(DateTime date)
	{
		// All saturdays and sundays, and july are toll-free
		if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.Month == 7)
			return true;

		DateTime[] holidaysOnWeekdays = new DateTime[]
		{
		new DateTime(2024, 1, 1),
		new DateTime(2024, 1, 6),
		new DateTime(2024, 3, 29),
		new DateTime(2024, 4, 1),
		new DateTime(2024, 5, 1),
		new DateTime(2024, 5, 9),
		new DateTime(2024, 6, 6),
		new DateTime(2024, 6, 22),
		new DateTime(2024, 11, 2),
		new DateTime(2024, 12, 25),
		new DateTime(2024, 12, 26),
		};

		return date.Year == 2024 && (holidaysOnWeekdays.Contains(date.Date) || holidaysOnWeekdays.Contains(date.Date.AddDays(1)));
	}
}

