using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2
{
	public class VehicleManager : IVehicleManager
	{
		private readonly List<Vehicle> _vehicles;
		private readonly IFeeCalculator _feeCalculator;
		private readonly IDateManager _dateManager;
		private readonly IVehicleDataOutput _vehicleDataOutput;

		public VehicleManager(List<Vehicle> vehicles, IFeeCalculator feeCalculator, IDateManager dateManager, IVehicleDataOutput vehicleDataOutput)
		{
			try
			{
				_vehicles = vehicles ?? throw new ArgumentNullException(nameof(vehicles));
				_feeCalculator = feeCalculator ?? throw new ArgumentNullException(nameof(feeCalculator));
				_dateManager = dateManager ?? throw new ArgumentNullException(nameof(dateManager));
				_vehicleDataOutput = vehicleDataOutput ?? throw new ArgumentNullException(nameof(vehicleDataOutput));
			}
			catch (ArgumentNullException ex)
			{
				throw new Exception("Invalid input parameter.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("Error initializing VehicleManager", ex);
			}
			
		}

		public void DisplayTollFeesForAllVehicles()
		{
			foreach (var vehicle in _vehicles)
			{
				_vehicleDataOutput.DisplayTollFees(vehicle, _feeCalculator.GetTotalFeeForPassages(vehicle.TollPassages));
			}
		}

		public void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan)
		{
			if (timeSpan <= TimeSpan.Zero || numberOfPassages < 1)
				return;

			foreach (var vehicle in _vehicles)
			{
				vehicle.TollPassages.Clear();
				var newDates = _dateManager.GetRandomDates(numberOfPassages, timeSpan);

				foreach (var date in newDates)
				{
					vehicle.TollPassages.Add(new TollPassage(date, _feeCalculator.GetFeeByDate(date)));
				}

				if (!IsTollFreeTypes(vehicle.Types))
					_feeCalculator.CalculateFeeDue(vehicle.TollPassages);
			}
		}

		public bool IsTollFreeTypes(VehicleTypes types)
		{
			return VehicleTypesManager.GetTollFreeVehicleTypes()
				.Any(tollFreeType => types.HasFlag(tollFreeType));
		}
	}
}
