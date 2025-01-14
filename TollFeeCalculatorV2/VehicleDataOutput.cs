using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;
public class VehicleDataOutput : IVehicleDataOutput
{
	private const string PassageTimeHeader = "PASSAGE TIME";
	private const string FeeHeader = "FEE";
	private const string PayHeader = "PAY";
	private const string TotalFeeHeader = "TOTAL FEE";
	private const string Separator = "=======================================";

	public void DisplayTollFees(Vehicle vehicle, int totalFee)
	{
		WriteVehicleHeader(vehicle);
		WritePassageTimes(vehicle);
		WriteTotalFee(totalFee);
		WriteSeparator();
	}

	public string GetVehicleTypes(Vehicle vehicle)
	{
		return string.Join(", ",
			Enum.GetValues(typeof(VehicleTypes))
			.Cast<VehicleTypes>()
			.Where(type => (vehicle.Types & type) == type && type != 0)
			.Select(s => s.ToString())
		);
	}

	private void WriteVehicleHeader(Vehicle vehicle)
	{
		Console.WriteLine($"\n{vehicle.Name} ({GetVehicleTypes(vehicle)})");
	}

	private void WritePassageTimes(Vehicle vehicle)
	{
		Console.WriteLine($"{PassageTimeHeader,-23}{FeeHeader,-5}{PayHeader,5}");

		foreach (var passageTime in vehicle.TollPassages)
		{
			Console.WriteLine($"{passageTime.PassageTime,-22}{passageTime.Fee,4}{(passageTime.IsFeeToPay ? "YES" : "NO"),7}");
		}
	}

	private void WriteTotalFee(int totalFee)
	{
		 
		Console.WriteLine($"{TotalFeeHeader}{totalFee, 17}");
	}

	private void WriteSeparator()
	{
		Console.WriteLine(Separator);
	}
}
