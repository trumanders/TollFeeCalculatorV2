
using System;
using System.Collections.Generic;
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
			_vehicles = vehicles;
			_feeCalculator = feeCalculator;
			_dateManager = dateManager;
			_vehicleDataOutput = vehicleDataOutput;
		}

		public void DisplayTollFeesForAllVehicles()
		{
			Enumerable.Range(0, _vehicles.Count).ToList().ForEach(i =>
				_vehicleDataOutput.DisplayTollFees(_vehicles[i], _feeCalculator.GetTotalFeeForPassages(_vehicles[i].TollPassages)));
		}

		public void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan)
		{
			if (timeSpan <= TimeSpan.Zero)
				return;

			for (int i = 0; i < _vehicles.Count; i++)
			{
				_vehicles[i].TollPassages.Clear();
				var newDates = _dateManager.GetRandomDates(numberOfPassages, timeSpan);
				foreach (var date in newDates)
				{
					var fee = IsTollFreeVehicle(i) ? 0 : _feeCalculator.GetFeeByDate(date);
					_vehicles[i].TollPassages.Add(new TollPassage(date, fee));
				}

				_feeCalculator.SetFeeDue(_vehicles[i].TollPassages);
			}
		}
		public bool IsTollFreeVehicle(int index)
		{
			if (_vehicles[index] == null) return false;

			foreach (var tollFreeType in VehicleTypesManager.GetTollFreeVehicleTypes())
			{
				if ((_vehicles[index].Types & tollFreeType) != 0)
					return true;
			}

			return false;
		}
	}
}
