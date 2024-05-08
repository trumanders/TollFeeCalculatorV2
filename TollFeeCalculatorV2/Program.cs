namespace TollFeeCalculatorV2;
internal class Program
{
	static void Main(string[] args)
	{
		var vehicle = new Vehicle("Volvo", VehicleTypes.Car);
		vehicle.Types = VehicleTypes.Car;
		Console.WriteLine(new TollCalculator().GetTollFee(vehicle, new DateTime[] { new DateTime(2024, 5, 8, 9, 45, 0) }));
	}
}
