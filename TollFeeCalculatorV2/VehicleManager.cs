using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2
{
	public class VehicleManager
	{
		List<Vehicle> _vehicles;
		FeeCalculator _feeCalculator;
		DateManager _dateManager;

		public VehicleManager(List<Vehicle> vehicles, FeeCalculator feeCalculator, DateManager dateManager)
		{
			_vehicles = vehicles;
			_feeCalculator = feeCalculator;
			_dateManager = dateManager;
		}

		public void AddVehicles(List<Vehicle> vehicles)
		{
			_vehicles.AddRange(vehicles);
		}

		public void SetNewTollPassages(int numberOfPassages, int month, int day)
		{
			foreach (var (vehicle, i) in _vehicles.Select((value, i) => (value, i)))
			{
				vehicle.TollPassages.Clear();
				var newDates = _dateManager.GetRandomDates(numberOfPassages, month, day);
				foreach (var date in newDates)
				{
					var fee = IsTollFreeVehicle(vehicle) ? 0 : _feeCalculator.GetFeeByDate(date);
					vehicle.TollPassages.Add(new TollPassage(date, fee));
				}

				_feeCalculator.SetFeeDue(vehicle.TollPassages);
			}
		}
		public bool IsTollFreeVehicle(Vehicle vehicle)
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
