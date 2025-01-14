using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class TollRateProvider : ITollRateProvider
{
	private Config _config;

	public TollRateProvider(Config config)
	{
		_config = config;
	}

	public int GetTollRate(DateTime dateTime)
	{
		if (IsTollFreeDate(dateTime))
			return 0;

		TimeSpan time = dateTime.TimeOfDay;	

		foreach (var tollFee in _config.TollFees)
		{
			foreach (var interval in tollFee.TimeIntervals)
			{
				if (time >= interval.Start && time < interval.End)
				{
					return tollFee.Fee;
				}
			}
		}

		return _config.DefaultFee;
	}

	private bool IsTollFreeDate(DateTime date)
	{
		if (date.Year != 2024)
			throw new NotImplementedException();

		// All saturdays and sundays, and july are toll-free
		if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || _config.TollFreeMonths.Contains(date.Month))
			return true;

		return _config.Holidays.Contains(date.Date) || _config.Holidays.Contains(date.Date.AddDays(1));
	}
}