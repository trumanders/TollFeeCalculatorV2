using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2
{
	public class VehicleManager : IVehicleManager
	{
		private readonly List<IVehicle> _vehicles;
		private readonly IFeeCalculator _feeCalculator;
		private readonly IDateManager _dateManager;
		private readonly IVehicleDataOutput _vehicleDataOutput;

		public VehicleManager(List<IVehicle> vehicles, IFeeCalculator feeCalculator, IDateManager dateManager, IVehicleDataOutput vehicleDataOutput)
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

		/// <summary>
		/// Display all toll fees and total fee for all vehicles
		/// </summary>
		public void DisplayTollFeesForAllVehicles()
		{
			foreach (var vehicle in _vehicles)
			{
				_vehicleDataOutput.DisplayTollFees(vehicle, _feeCalculator.GetTotalFeeForPassages(vehicle.TollPassages));
			}
		}

		/// <summary>
		/// Generate new toll passages with fees for all vehicles
		/// </summary>
		/// <param name="numberOfPassages">The number of toll passages to add to each vehicle.</param>
		/// <param name="timeSpan">The time span within which the toll passages should be generated.</param>
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
					_feeCalculator.SetFeeDue(vehicle.TollPassages);
			}
		}

		/// <summary>
		/// Check if any of the types is toll free
		/// </summary>
		/// <param name="types">The vehicle types to c</param>
		/// <returns>True if types contains a toll free type.</returns>
		public bool IsTollFreeTypes(VehicleTypes types)
		{
			foreach (var tollFreeType in VehicleTypesManager.GetTollFreeVehicleTypes())
			{
				if ((types & tollFreeType) != 0)
					return true;
			}

			return false;
		}
	}
}
