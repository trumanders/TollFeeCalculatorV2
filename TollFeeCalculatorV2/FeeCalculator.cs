using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class FeeCalculator : IFeeCalculator
{
	ITollRateProvider _tollRateProvider;

	const int MAX_FEE = 60;
	static readonly TimeSpan _singleChargeInterval = TimeSpan.FromHours(1);

	public FeeCalculator(ITollRateProvider tollRateProvider)
	{
		try
		{
			_tollRateProvider = tollRateProvider ?? throw new ArgumentNullException(nameof(tollRateProvider));
		}
		catch (ArgumentNullException ex)
		{
			throw new Exception("Invalid input parameter.", ex);
		}
		catch (Exception ex)
		{
			throw new Exception("Error initializing TollRateProvider", ex);
		}
	}

	public void CalculateFeeDue(List<TollPassage> tollPassages)
	{
		if (tollPassages == null)
			throw new ArgumentNullException(nameof(tollPassages), "Toll passages list cannot be null.");

		if (tollPassages.Count == 0)
			throw new ArgumentException("Toll passages list cannot be empty.", nameof(tollPassages));


		var firstFeePassage = tollPassages.FirstOrDefault(passage => passage.Fee > 0)
			?? tollPassages.First();

		var intervalStart = firstFeePassage.PassageTime;
		var highestFeePassageInInterval = firstFeePassage;

		foreach (var tollPassage in tollPassages)
		{
			if (IsPassageWithinInterval(intervalStart, tollPassage.PassageTime, _singleChargeInterval))
			{
				highestFeePassageInInterval = tollPassage.Fee > highestFeePassageInInterval.Fee
					? tollPassage : highestFeePassageInInterval;
			}
			else
			{
				highestFeePassageInInterval.IsFeeToPay = highestFeePassageInInterval.Fee > 0;
				highestFeePassageInInterval = tollPassage;
				intervalStart = tollPassage.PassageTime;
			}
		}
		highestFeePassageInInterval.IsFeeToPay = highestFeePassageInInterval.Fee > 0;
	}

	public int GetFeeByDate(DateTime date)
	{
		return _tollRateProvider.GetTollRate(date);
	}

	public int GetTotalFeeForPassages(List<TollPassage> tollPassages)
	{
		var totalFee = tollPassages
			.Where(passage => passage.IsFeeToPay)
			.GroupBy(passage => passage.PassageTime.Date)
			.Sum(group => Math.Min(group.Sum(passage => passage.Fee), MAX_FEE));

		return totalFee;
	}

	private bool IsPassageWithinInterval(DateTime start, DateTime end, TimeSpan timeSpan)
	{
		return end - start < timeSpan;
	}
}