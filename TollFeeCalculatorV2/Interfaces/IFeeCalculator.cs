namespace TollFeeCalculatorV2.Interfaces
{
    public interface IFeeCalculator
    {
        int GetFeeByDate(DateTime date);
        void CalculateFeeDue(List<TollPassage> tollPassages);
        int GetTotalFeeForPassages(List<TollPassage> tollPassages);

	}
}