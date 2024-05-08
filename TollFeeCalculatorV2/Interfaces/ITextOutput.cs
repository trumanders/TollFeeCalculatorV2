namespace TollFeeCalculatorV2.Interfaces
{
    public interface ITextOutput
    {
        void DisplayTollFeesForVehicle(IVehicle vehicle);
        string GetVehicleTypes(IVehicle vehicle);
        public void DisplayTollFeesForAllVehicles(List<IVehicle> vehicles);
	}
}