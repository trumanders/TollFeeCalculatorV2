
using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2
{
	public class VehicleManager : IVehicleManager
	{
		List<IVehicle> _vehicles;
		IFeeCalculator _feeCalculator;
		IDateManager _dateManager;
		IVehicleDataOutput _vehicleDataOutput;

		public VehicleManager(List<IVehicle> vehicles, IFeeCalculator feeCalculator, IDateManager dateManager, IVehicleDataOutput vehicleDataOutput)
		{
			_vehicles = vehicles;
			_feeCalculator = feeCalculator;
			_dateManager = dateManager;
			_vehicleDataOutput = vehicleDataOutput;
		}

		public int GetNumberOfVehicles()
		{
			return _vehicles.Count;
		}

		public int GetTotalFee(int vehicleIndex)
		{
			return _feeCalculator.GetTotalFeeForPassages(_vehicles[vehicleIndex].TollPassages);
		}

		public IVehicle GetVehicle(int index)
		{
			return _vehicles[index];
		}

		public void DisplayTollFeesForAllVehicles()
		{
			Enumerable.Range(0, _vehicles.Count)
			.ToList().ForEach(DisplayTollFees);	
		}

		public void DisplayTollFees(int index)
		{
			_vehicleDataOutput.DisplayTollFees(_vehicles[index], _feeCalculator.GetTotalFeeForPassages(_vehicles[index].TollPassages));
		}

		public void GenerateNewTollPassagesForAllVehicles(int numberOfPassages, TimeSpan timeSpan)
		{
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
