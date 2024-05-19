namespace TollFeeCalculatorV2.Interfaces;
public interface ITollRateProvider
{
	int GetTollRate(DateTime date);
}
