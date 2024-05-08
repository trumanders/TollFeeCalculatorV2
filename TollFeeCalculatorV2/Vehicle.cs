using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2;

public class Vehicle : IVehicle
{
	public string Name { get; set; }
	public VehicleTypes Types { get; set; }
	public List<TollPassage> TollPassages { get; set; } = new List<TollPassage>();
	public Vehicle(string name, VehicleTypes types)
	{
		Name = name;
		Types = types;
	}
}