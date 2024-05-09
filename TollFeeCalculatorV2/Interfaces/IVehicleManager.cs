namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleManager
    {
        bool IsTollFreeVehicle(IVehicle vehicle);
        void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan);
		void DisplayTollFeesForAllVehicles();
	}
}