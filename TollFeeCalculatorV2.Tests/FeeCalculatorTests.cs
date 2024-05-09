using NUnit.Framework;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework.Internal;
namespace TollFeeCalculatorV2.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FeeCalculatorTests
{
	[Test]
	public void SetFeeDue_SetsCorrectFeeDues()
	{
		var tollPassages = new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 5, 7, 3, 47, 25), 0),
			new TollPassage(new DateTime(2024, 5, 7, 4, 22, 15), 0),
			new TollPassage(new DateTime(2024, 5, 7, 5, 13, 42), 0),
			new TollPassage(new DateTime(2024, 5, 7, 6, 5, 33), 9),
			new TollPassage(new DateTime(2024, 5, 7, 6, 55, 14), 16),
			new TollPassage(new DateTime(2024, 5, 7, 8, 2, 58), 16),
			new TollPassage(new DateTime(2024, 5, 7, 9, 16, 3), 9),
			new TollPassage(new DateTime(2024, 5, 7, 10, 45, 20), 9),
			new TollPassage(new DateTime(2024, 5, 7, 11, 32, 55), 9),
			new TollPassage(new DateTime(2024, 5, 7, 12, 28, 44), 9),
			new TollPassage(new DateTime(2024, 5, 7, 13, 15, 9), 9),
			new TollPassage(new DateTime(2024, 5, 7, 15, 59, 23), 22),
			new TollPassage(new DateTime(2024, 5, 7, 17, 22, 8), 16),
			new TollPassage(new DateTime(2024, 5, 7, 19, 45, 56), 0),
			new TollPassage(new DateTime(2024, 5, 7, 23, 30, 11), 0)
		};

		var expectedFeeDue = new List<bool>
		{
			false, false, false, false, true, true, true, true, false, true, false, true, true, false, false
		};

		var tollRateProvider = A.Fake<TollRateProvider>();
		var sut = new FeeCalculator(tollRateProvider);
		sut.SetFeeDue(tollPassages);

		Assert.That(tollPassages.Zip(expectedFeeDue, (tollPassage, expected) => tollPassage.IsFeeToPay == expected), Is.All.True);
	}

	public static IEnumerable<TestCaseData> TollPassageTestCases()
	{
		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 6, 17, 1, 15, 20), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 2, 30, 10), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 3, 40, 35), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 4, 50, 45), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 5, 20, 15), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 6, 25, 5), 9) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 7, 15, 25), 22) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 6, 17, 7, 17, 55), 22) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 9, 45, 30), 9) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 6, 17, 10, 10, 20), 9) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 10, 33, 10), 9) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 10, 44, 40), 9) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 17, 14, 30, 5), 9) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 6, 17, 14, 55, 50), 9) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 6, 17, 18, 35, 50), 0) { IsFeeToPay = false }
		}, 49);

		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 1, 1, 6, 15, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 2, 3, 8, 50, 0), 9) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 3, 28, 6, 45, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 3, 29, 7, 30, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 4, 1, 15, 20, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 4, 30, 17, 10, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 13, 14, 35, 0), 22) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 6, 19, 17, 15, 0), 16) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 7, 21, 6, 5, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 8, 16, 15, 25, 0), 22) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 9, 23, 8, 20, 0), 16) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 10, 7, 18, 10, 0), 9) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 11, 15, 6, 30, 0), 16) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 12, 5, 7, 50, 0), 22) { IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 12, 30, 17, 45, 0), 16) { IsFeeToPay = true }
		}, 60);
	}

	[Test, TestCaseSource(nameof(TollPassageTestCases))]
	public void GetTotalFeeForPassages_ReturnsCorrectTotalFee(List<TollPassage> tollPassages, int expectedFee)
	{
		var tollRateProvider = A.Fake<TollRateProvider>();
		var sut = new FeeCalculator(tollRateProvider);
		var actualFee = sut.GetTotalFeeForPassages(tollPassages);

		Assert.That(actualFee, Is.EqualTo(expectedFee));
	}

	[TestCase("2024-01-01T02:34:56", 16)]
	[TestCase("2024-03-28T05:48:23", 16)]
	[TestCase("2024-03-29T13:15:44", 16)]
	[TestCase("2024-04-01T06:27:09", 16)]
	[TestCase("2024-04-30T03:19:21", 16)]
	[TestCase("2024-05-01T18:40:37", 16)]
	[TestCase("2024-05-08T07:54:11", 16)]
	[TestCase("2024-05-09T20:36:48", 16)]
	[TestCase("2024-06-05T22:58:02", 16)]
	[TestCase("2024-06-06T04:07:29", 16)]
	[TestCase("2024-12-24T12:30:55", 16)]
	[TestCase("2024-12-25T09:22:43", 16)]
	[TestCase("2024-12-26T00:15:07", 16)]
	public void GetFeeByDate_ReturnsCorrectFee(DateTime dateTime, int expectedFee)
	{
		var tollRateProvider = A.Fake<TollRateProvider>();
		var sut = new FeeCalculator(tollRateProvider);
		var actualFee = sut.GetFeeByDate(dateTime);
		Assert.That(actualFee, Is.EqualTo(expectedFee));
	}
}
