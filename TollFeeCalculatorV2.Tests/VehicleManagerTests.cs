using FakeItEasy;
using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2.Tests
{
	[Parallelizable(ParallelScope.Self)]
	[TestFixture]
	public class VehicleManagerTests
	{
		[TestCase(VehicleTypes.Car, false)]
		[TestCase(VehicleTypes.Tractor, true)]
		[TestCase(VehicleTypes.Emergency, true)]
		[TestCase(VehicleTypes.Diplomat, true)]
		[TestCase(VehicleTypes.Foreign, true)]
		[TestCase(VehicleTypes.Military, true)]
		[TestCase(VehicleTypes.Trailer, true)]
		[TestCase(VehicleTypes.LargeBus, true)]
		[TestCase(VehicleTypes.Car | VehicleTypes.Military, true)]
		[TestCase(VehicleTypes.Car | VehicleTypes.Emergency, true)]
		[TestCase(VehicleTypes.Car | VehicleTypes.Bus, false)]
		[TestCase(VehicleTypes.Car | VehicleTypes.LargeBus, true)]
		[TestCase(VehicleTypes.Car | VehicleTypes.Caravan, false)]
		[TestCase(VehicleTypes.Caravan | VehicleTypes.Military, true)]
		public void IsTollFreeVehicle_ReturnsCorrectBool(VehicleTypes types, bool expectedResult)
		{
			var vehicles = A.Fake<List<IVehicle>>();
			var feeCalc = A.Fake<FeeCalculator>();
			var dateMan = A.Fake<DateManager>();
			var vehicleDataOutput = A.Fake<VehicleDataOutput>();

			var sut = new VehicleManager(vehicles, feeCalc, dateMan, vehicleDataOutput);
			var actualResult = sut.IsTollFreeTypes(types);

			Assert.That(actualResult, Is.EqualTo(expectedResult));
		}
	}
}
