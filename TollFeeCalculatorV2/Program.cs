namespace TollFeeCalculatorV2;
public class Program
{
	static FeeCalculator feeCalculator;
	static VehicleManager vehicleManager;
	static DateManager dateManager;

	static List<Vehicle> vehicles;

	static void Main(string[] args)
	{
		vehicles = new List<Vehicle>
		{
			new Vehicle("Volvo", VehicleTypes.Car)
		};

		dateManager = new DateManager();
		feeCalculator = new FeeCalculator();
		vehicleManager = new VehicleManager(vehicles, feeCalculator, dateManager);
		
		vehicleManager.SetNewTollPassages(10, 5, 8);
	}
}
