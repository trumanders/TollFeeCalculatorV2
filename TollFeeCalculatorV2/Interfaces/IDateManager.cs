namespace TollFeeCalculatorV2.Interfaces
{
    public interface IDateManager
    {
        List<DateTime> GetRandomDates(int numberOfDates);
    }
}