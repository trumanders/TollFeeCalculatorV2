namespace TollFeeCalculatorV2;

/* Using this enum with flagging to assign more than
 * one type to a vehicle	 
 */
public enum VehicleTypes
{
	Motorbike = 1,
	Tractor = 2,
	Emergency = 4,
	Diplomat = 8,
	Foreign = 16,
	Military = 32,
	Car = 64,
	Caravan = 128,
	Bus = 256,
	LargeBus = 512,
	Trailer = 1024
}
