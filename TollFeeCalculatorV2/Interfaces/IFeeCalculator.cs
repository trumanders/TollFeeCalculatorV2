namespace TollFeeCalculatorV2.Interfaces
{
    public interface IFeeCalculator
    {
        int GetFeeByDate(DateTime date);
        void SetFeeDue(List<TollPassage> tollPassages);
        int GetTotalFeeForPassages(List<TollPassage> tollPassages);

	}
}