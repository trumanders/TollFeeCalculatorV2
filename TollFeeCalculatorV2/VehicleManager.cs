
using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2
{
    public class VehicleManager : IVehicleManager
	{
		List<IVehicle> _vehicles;
		IFeeCalculator _feeCalculator;
		IDateManager _dateManager;

		public VehicleManager(List<IVehicle> vehicles, IFeeCalculator feeCalculator, IDateManager dateManager)
		{
			_vehicles = vehicles;
			_feeCalculator = feeCalculator;
			_dateManager = dateManager;
		}

		public void AddVehicles(List<IVehicle> vehicles)
		{
			_vehicles.AddRange(vehicles);
		}

		public void SetNewTollPassages(int numberOfPassages)
		{
			foreach (var (vehicle, i) in _vehicles.Select((value, i) => (value, i)))
			{
				vehicle.TollPassages.Clear();
				var newDates = _dateManager.GetRandomDates(numberOfPassages);
				foreach (var date in newDates)
				{
					var fee = IsTollFreeVehicle(vehicle) ? 0 : _feeCalculator.GetFeeByDate(date);
					vehicle.TollPassages.Add(new TollPassage(date, fee));
				}

				_feeCalculator.SetFeeDue(vehicle.TollPassages);
			}
		}
		public bool IsTollFreeVehicle(IVehicle vehicle)
		{
			if (vehicle == null) return false;

			foreach (var tollFreeType in VehicleTypesManager.GetTollFreeVehicleTypes())
			{
				if ((vehicle.Types & tollFreeType) != 0)
					return true;
			}

			return false;
		}
	}
}
