﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2;

public class Program
{
	private readonly IVehicleManager _vehicleManager;
	private static List<IVehicle> _vehicles = new List<IVehicle>();

	static int passageCount = 50;
	static TimeSpan timeSpan = new TimeSpan(5, 0, 0, 0);

	public Program(IVehicleManager vehicleManager)
	{
		_vehicleManager = vehicleManager;
	}

	static void Main(string[] args)
	{
		InitializeVehicles();
		var host = CreateHostBuilder(args).Build();

		using (var scope = host.Services.CreateScope())
		{
			var serviceProvider = scope.ServiceProvider;

			try
			{
				var program = new Program(serviceProvider.GetRequiredService<IVehicleManager>());
				program.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
	

	private static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args).ConfigureServices((services) =>
		{
			services
			.AddSingleton<IVehicleDataOutput, VehicleDataOutput>()
			.AddSingleton<IDateManager, DateManager>()
			.AddSingleton<ITollRateProvider, TollRateProvider>()
			.AddSingleton<IFeeCalculator, FeeCalculator>(provider => new FeeCalculator(provider.GetRequiredService<ITollRateProvider>()))
			.AddSingleton<IVehicleManager, VehicleManager>(provider =>
			{
				var tollRateProvider = provider.GetRequiredService<ITollRateProvider>();
				var feeCalculator = provider.GetRequiredService<IFeeCalculator>();
				var dateManager = provider.GetRequiredService<IDateManager>();
				var vehicleDataOutput = provider.GetRequiredService<IVehicleDataOutput>();
				return new VehicleManager(
					_vehicles,
					provider.GetRequiredService<IFeeCalculator>(),
					provider.GetRequiredService<IDateManager>(),
					provider.GetRequiredService<IVehicleDataOutput>()
					);
			});
		});
	}

	private void Run()
	{
		GenerateTollPassagesAndDisplayFees();
	}

	private void GenerateTollPassagesAndDisplayFees()
	{
		_vehicleManager.GenerateNewTollPassagesForAllVehicles(passageCount, timeSpan);
		_vehicleManager.DisplayTollFeesForAllVehicles();
	}

	private static void InitializeVehicles()
	{
		_vehicles = new List<IVehicle>()
		{
			new Vehicle("Volvo 245", VehicleTypes.Car),
			new Vehicle("Truck", VehicleTypes.Military)
		};
	}
}

