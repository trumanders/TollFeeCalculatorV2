namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleDataOutput
    {
        void DisplayTollFees(Vehicle vehicle, int totalFeeForVehicle);
        string GetVehicleTypes(Vehicle vehicle);
	}
}