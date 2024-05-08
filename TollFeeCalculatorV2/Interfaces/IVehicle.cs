namespace TollFeeCalculatorV2.Interfaces
{
    public interface IVehicle
    {
        string Name { get; set; }
        List<TollPassage> TollPassages { get; set; }
        VehicleTypes Types { get; set; }
    }
}