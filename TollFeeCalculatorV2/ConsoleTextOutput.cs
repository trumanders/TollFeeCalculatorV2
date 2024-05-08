using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

    public class ConsoleTextOutput : ITextOutput
{
	public void DisplayTollFeesForAllVehicles(List<IVehicle> vehicles)
	{
		vehicles.ForEach(DisplayTollFeesForVehicle);
	}

	public void DisplayTollFeesForVehicle(IVehicle vehicle)
	{
		Console.WriteLine($"{vehicle.Name} ({GetVehicleTypes(vehicle)})\n");
		Console.WriteLine($"{"PASSAGE TIME",-21}{"FEE", -5}{"PAY"}");
		foreach (var passageTime in vehicle.TollPassages)
		{
			Console.WriteLine($"{passageTime.PassageTime, -22}{passageTime.Fee, 2}{(passageTime.IsFeeToPay ? "YES" : "NO"), 5}");
		}
		Console.WriteLine("=======================================");
	}

	public string GetVehicleTypes(IVehicle vehicle)
	{
		return string.Join(", ",
			Enum.GetValues(typeof(VehicleTypes))
			.Cast<VehicleTypes>()
			.Where(type => (vehicle.Types & type) == type && type != 0)
			.Select(s => s.ToString())
		);
	}
}
