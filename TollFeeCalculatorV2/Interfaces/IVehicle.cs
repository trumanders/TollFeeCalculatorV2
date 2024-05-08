using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculatorV2.Interfaces
{
	public interface IVehicle
	{
		string Name { get; set; }
		VehicleTypes Types { get; set; }
		List<TollPassage> TollPassages { get; set; }
	}
}
