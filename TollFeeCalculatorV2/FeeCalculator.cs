using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class FeeCalculator : IFeeCalculator
{
	TollRateProvider _tollRateProvider;

	const int MAX_FEE = 60;
	static readonly TimeSpan _singleChargeInterval = TimeSpan.FromHours(1);

	public FeeCalculator(TollRateProvider tollRateProvider)
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

	/// <summary>
	/// Set whether the fee for each toll passage is to be paid based on the passage intervals.
	/// </summary>
	/// <param name="tollPassages">The list of toll passages to set the fee dues on</param>
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

	/// <summary>
	/// Get the fee associated with the provided date
	/// </summary>
	/// <param name="date">The date and time for which to get the fee.</param>
	/// <returns>The fee amount for the provided date.</returns>
	public int GetFeeByDate(DateTime date)
	{
		return _tollRateProvider.GetTollRate(date);
	}

	/// <summary>
	/// Calculate the total fee for a list of toll passages that require payment. Ignore the amount
	/// that exceeds the maximu fee for each day.
	/// </summary>
	/// <param name="tollPassages">The collection of toll passages to sum up the fee for</param>
	/// <returns>The total fee.</returns>
	public int GetTotalFeeForPassages(List<TollPassage> tollPassages)
	{
		var totalFee = tollPassages
			.Where(passage => passage.IsFeeToPay)
			.GroupBy(passage => passage.PassageTime.Date)
			.Sum(group => Math.Min(group.Sum(passage => passage.Fee), MAX_FEE));

		return totalFee;
	}

	/// <summary>
	/// Checks if a passage is within a time interval from the first passage passage
	/// for the current interval.
	/// </summary>
	/// <param name="start">The time for the start of the current interval</param>
	/// <param name="end">The end time for the current interval</param>
	/// <param name="timeSpan">The timespan within which the passage should be to return true.</param>
	/// <returns>True if the passed in </returns>
	private bool IsPassageWithinInterval(DateTime start, DateTime end, TimeSpan timeSpan)
	{
		return end - start < timeSpan;
	}
}