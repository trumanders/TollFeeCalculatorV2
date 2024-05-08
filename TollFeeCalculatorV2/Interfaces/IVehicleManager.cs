namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleManager
    {
        void AddVehicles(List<IVehicle> vehicles);
        bool IsTollFreeVehicle(IVehicle vehicle);
        void SetNewTollPassages(int numberOfPassages);
    }
}