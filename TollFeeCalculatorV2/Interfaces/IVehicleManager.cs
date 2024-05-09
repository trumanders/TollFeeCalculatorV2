namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleManager
    {
        bool IsTollFreeVehicle(int index);
        void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan);
		int GetNumberOfVehicles();
		void DisplayTollFeesForAllVehicles();
		IVehicle GetVehicle(int index);
	}
}