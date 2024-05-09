using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class Program
{
	private readonly IFeeCalculator _feeCalculator;
	private readonly IVehicleManager _vehicleManager;
	private readonly IDateManager _dateManager;
	private readonly TollRateProvider _tollRateProvider;
	private readonly IVehicleDataOutput _vehicleDataOutput;
	private List<IVehicle> _vehicles;

	static int passageCount = 50;
	static TimeSpan timeSpan = new TimeSpan(5, 0, 0, 0);

	public Program()
	{
		try
		{
			InitializeVehicles();

			_tollRateProvider = new TollRateProvider();
			_dateManager = new DateManager();
			_vehicleDataOutput = new VehicleDataOutput();			
			_feeCalculator = new FeeCalculator(_tollRateProvider);
			_vehicleManager = new VehicleManager(_vehicles, _feeCalculator, _dateManager, _vehicleDataOutput);
		}
		catch (Exception ex) 
		{
			Console.WriteLine(ex.ToString());
		}
	}

	static void Main(string[] args)
	{
		var program = new Program();
		program.Run();
	}

	private void Run()
	{
		GenerateTollPassagesAndDisplayFees();
	}

	private void GenerateTollPassagesAndDisplayFees()
	{
		_vehicleManager.GenerateNewTollPassagesForAllVehicles(passageCount, timeSpan);
		_vehicleManager.DisplayTollFeesForAllVehicles();
	}

	private void InitializeVehicles()
	{
		_vehicles = new List<IVehicle>()
		{
			new Vehicle("Volvo 245", VehicleTypes.Car),
			new Vehicle("Truck", VehicleTypes.Military)
		};
	}
}
