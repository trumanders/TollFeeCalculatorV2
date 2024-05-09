namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleManager
    {
        bool IsTollFreeTypes(VehicleTypes types);
        void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan);
		void DisplayTollFeesForAllVehicles();
	}
}