namespace TollFeeCalculatorV2
{
	public class TollPassage
	{
		public DateTime PassageTime { get; private set; }
		public int Fee { get; private set; } = 0;
		public bool IsFeeToPay { get; set; }
		public TollPassage(DateTime time, int fee)
		{
			PassageTime = time;
			Fee = fee;
		}
	}
}
