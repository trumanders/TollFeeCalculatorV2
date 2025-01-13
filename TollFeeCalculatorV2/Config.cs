namespace TollFeeCalculatorV2
{
	public class Config
	{
		public List<TollFee> TollFees { get; set; }
		public List<DateTime> Holidays { get; set; }

		public List<int> TollFreeMonths { get; set; }
		public int DefaultFee { get; set; }
	}

	public class TollFee
	{
		public int Fee { get; set; }
		public List<TimeInterval> TimeIntervals { get; set; }
	}

	public class TimeInterval
	{
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
	}
}
