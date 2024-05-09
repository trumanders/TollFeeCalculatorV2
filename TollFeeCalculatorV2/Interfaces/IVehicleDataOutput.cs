namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicleDataOutput
    {
        void DisplayTollFees(IVehicle vehicle, int totalFeeForVehicle);
        string GetVehicleTypes(IVehicle vehicle);
	}
}