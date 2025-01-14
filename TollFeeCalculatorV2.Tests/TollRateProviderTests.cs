using TollFeeCalculatorV2.Interfaces;

namespace TollFeeCalculatorV2.Tests;

public class TollRateProviderTests
{
	private ITollRateProvider _sut;

	[SetUp]
	public void Setup()
	{
		_sut = new TollRateProvider(ReadAndParseConfigJSON.GetConfig());
	}

	[TestCase("2024-01-01T14:34:56", 0)]
	[TestCase("2024-01-05T14:34:56", 0)]
	[TestCase("2024-03-28T09:48:23", 0)]
	[TestCase("2024-03-29T13:15:44", 0)]
	[TestCase("2024-04-01T11:27:09", 0)]
	[TestCase("2024-04-30T10:19:21", 0)]
	[TestCase("2024-05-01T18:40:37", 0)]
	[TestCase("2024-05-08T07:54:11", 0)]
	[TestCase("2024-05-09T11:36:48", 0)]
	[TestCase("2024-06-05T10:58:02", 0)]
	[TestCase("2024-06-06T14:07:29", 0)]
	[TestCase("2024-06-21T13:07:29", 0)]
	[TestCase("2024-06-22T09:07:29", 0)]
	[TestCase("2024-12-24T12:30:55", 0)]
	[TestCase("2024-12-25T09:22:43", 0)]
	[TestCase("2024-12-26T08:15:07", 0)]
	public void GetFeeByDate_ReturnsZeroFeeForHolidays(DateTime dateTime, int expectedFee)
	{
		// Act
		var actualFee = _sut.GetTollRate(dateTime);

		// Assert
		Assert.That(actualFee, Is.EqualTo(expectedFee));
	}

	[TestCase("2024-05-10T06:00:00", 9)]
	[TestCase("2024-05-10T06:29:59", 9)]
	[TestCase("2024-05-10T08:30:00", 9)]
	[TestCase("2024-05-10T14:59:59", 9)]
	[TestCase("2024-05-10T18:00:00", 9)]
	[TestCase("2024-05-10T18:29:59", 9)]

	[TestCase("2024-05-10T06:30:00", 16)]
	[TestCase("2024-05-10T06:59:59", 16)]
	[TestCase("2024-05-10T08:00:00", 16)]
	[TestCase("2024-05-10T08:29:59", 16)]
	[TestCase("2024-05-10T15:00:00", 16)]
	[TestCase("2024-05-10T15:29:59", 16)]
	[TestCase("2024-05-10T17:00:00", 16)]
	[TestCase("2024-05-10T17:59:59", 16)]

	[TestCase("2024-05-10T07:00:00", 22)]
	[TestCase("2024-05-10T07:59:59", 22)]
	[TestCase("2024-05-10T15:30:00", 22)]
	[TestCase("2024-05-10T16:59:59", 22)]
	public void GetFeeByDate_ReturnsCorrectFeeAtRateChangeTimes(DateTime dateTime, int expectedFee)
	{
		// Act
		var actualFee = _sut.GetTollRate(dateTime);

		// Assert
		Assert.That(actualFee, Is.EqualTo(expectedFee));
	}
}
