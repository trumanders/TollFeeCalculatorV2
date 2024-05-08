using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;
public class Program
{
	static IFeeCalculator feeCalculator;
	static IVehicleManager vehicleManager;
	static IDateManager dateManager;
	static ITextOutput textOutput;
	static TollRateProvider tollRateProvider;

	static List<IVehicle> vehicles = new List<IVehicle>();

	static void Main(string[] args)
	{
		vehicles.Add(new Vehicle("Volvo 245", VehicleTypes.Car));
		vehicles.Add(new Vehicle("Truck", VehicleTypes.Military));

		tollRateProvider = new TollRateProvider();
		dateManager = new DateManager();
		feeCalculator = new FeeCalculator(tollRateProvider);
		vehicleManager = new VehicleManager(vehicles, feeCalculator, dateManager);
		textOutput = new ConsoleTextOutput();

		vehicleManager.SetNewTollPassages(5000);
		textOutput.DisplayTollFeesForAllVehicles(vehicles);
	}
}
