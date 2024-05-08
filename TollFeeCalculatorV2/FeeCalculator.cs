using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class FeeCalculator : IFeeCalculator
{
	TollRateProvider _tollRateProvider;

	const int MAX_FEE = 60;
	static readonly TimeSpan _singleChargeInterval = TimeSpan.FromHours(1);

	public FeeCalculator(TollRateProvider tollRateProvider)
	{
		_tollRateProvider = tollRateProvider;
	}

	public void SetFeeDue(List<TollPassage> tollPassages)
	{
		if (tollPassages == null || tollPassages.Count == 0)
			return;

		var firstFeeIndex = tollPassages.FindIndex(passage => passage.Fee > 0) < 0
			? 0 : tollPassages.FindIndex(passage => passage.Fee > 0);

		var intervalStart = tollPassages[firstFeeIndex].PassageTime;
		var highestFeeInIntervalIndex = firstFeeIndex;

		foreach (var (tollPassage, i) in tollPassages.Select((value, index) => (value, index)))
		{
			if (IsPassageWithinInterval(intervalStart, tollPassage.PassageTime, _singleChargeInterval))
			{
				highestFeeInIntervalIndex = tollPassage.Fee > tollPassages[highestFeeInIntervalIndex].Fee
					? i : highestFeeInIntervalIndex;
			}
			else
			{
				tollPassages[highestFeeInIntervalIndex].IsFeeToPay = tollPassages[highestFeeInIntervalIndex].Fee > 0;
				highestFeeInIntervalIndex = i;
				intervalStart = tollPassage.PassageTime;
			}
		}
		tollPassages[highestFeeInIntervalIndex].IsFeeToPay = tollPassages[highestFeeInIntervalIndex].Fee > 0;
	}

	public int GetFeeByDate(DateTime date)
	{
		return _tollRateProvider.GetTollRate(date);
	}

	private bool IsPassageWithinInterval(DateTime start, DateTime end, TimeSpan timeSpan)
	{
		return end - start < timeSpan;
	}
}