using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;
public class Program
{
	static IFeeCalculator feeCalculator;
	static IVehicleManager vehicleManager;
	static IDateManager dateManager;
	static TollRateProvider tollRateProvider;
	static IVehicleDataOutput vehicleDataOutput;

	static List<IVehicle> vehicles = new List<IVehicle>();
	static int numberOfPassages = 30;

	static void Main(string[] args)
	{
		vehicles.Add(new Vehicle("Volvo 245", VehicleTypes.Car));
		vehicles.Add(new Vehicle("Truck", VehicleTypes.Military));

		tollRateProvider = new TollRateProvider();
		dateManager = new DateManager();
		vehicleDataOutput = new VehicleDataOutput();
		feeCalculator = new FeeCalculator(tollRateProvider);
		vehicleManager = new VehicleManager(vehicles, feeCalculator, dateManager, vehicleDataOutput);

		vehicleManager.SetNewTollPassages(numberOfPassages);
		vehicleManager.DisplayTollFeesForAllVehicles();
	}
}
