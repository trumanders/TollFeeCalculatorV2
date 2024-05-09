namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleManager
    {
        void AddVehicles(List<IVehicle> vehicles);
        bool IsTollFreeVehicle(int index);
        void SetNewTollPassages(int numberOfPassages);
		int GetNumberOfVehicles();
		void DisplayTollFeesForAllVehicles();
		IVehicle GetVehicle(int index);
	}
}