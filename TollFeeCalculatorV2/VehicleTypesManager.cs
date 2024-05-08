namespace TollFeeCalculatorV2;

public class VehicleTypesManager
{
	public static List<VehicleTypes> GetTollFreeVehicleTypes()
	{
		return new List<VehicleTypes>()
		{
			VehicleTypes.Motorbike,
			VehicleTypes.Tractor,
			VehicleTypes.Emergency,
			VehicleTypes.Diplomat,
			VehicleTypes.Foreign,
			VehicleTypes.Military,
			VehicleTypes.Trailer,
			VehicleTypes.LargeBus
		};
	}
}
