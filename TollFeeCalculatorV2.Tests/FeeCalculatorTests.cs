using FakeItEasy;
using NUnit.Framework.Internal;
using TollFeeCalculatorV2.Interfaces;
namespace TollFeeCalculatorV2.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FeeCalculatorTests
{
	private FeeCalculator _sut;
	private ITollRateProvider _tollRateProvider;

	[SetUp]
	public void Setup()
	{
		_tollRateProvider = A.Fake<ITollRateProvider>();
		_sut = new FeeCalculator(_tollRateProvider);
	}

	[Test]
	public void CalculateFeeDue_SetsCorrectFeeDues()
	{
		var tollPassagesMixed = new List<TollPassage>
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

		// Arrange
		var expectedFeeDueMixed = new List<bool>
		{
			false, false, false, false, true, true, true, true, false, true, false, true, true, false, false
		};

		// Act
		_sut.CalculateFeeDue(tollPassagesMixed);

		// Assert
		Assert.That(tollPassagesMixed.Zip(expectedFeeDueMixed, (tollPassage, expected) => tollPassage.IsFeeToPay == expected), Is.All.True);
	}

	public static IEnumerable<TestCaseData> TollPassageTestCases()
	{
		// Passages during one day - less than max fee for one day
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


		// Passages for holidays
		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 1, 1, 6, 15, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 3, 28, 8, 50, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 3, 29, 6, 45, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 3, 29, 7, 30, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 4, 1, 15, 20, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 4, 30, 17, 10, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 1, 14, 35, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 8, 17, 15, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 9, 6, 5, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 5, 15, 25, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 6, 6, 8, 20, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 12, 24, 18, 10, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 12, 25, 6, 30, 0), 0) { IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 12, 26, 7, 50, 0), 0) { IsFeeToPay = false },
		}, 0);

		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 5, 10, 7, 12, 25), 22){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 10, 8, 32, 15), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 10, 15, 35, 42), 22){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 10, 16, 40, 42), 22){ IsFeeToPay = true },
		}, 60);

		// Passages over multiple days, some exceding max fee and some that does not
		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 5, 10, 7, 15, 25), 22){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 10, 15, 31, 15), 22){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 10, 16, 41, 15), 22){ IsFeeToPay = true }, // 60 (66)

			new TollPassage(new DateTime(2024, 5, 13, 6, 40, 42), 16){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 13, 8, 20, 42), 16){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 13, 15, 10, 42), 16){ IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 13, 15, 40, 42), 22){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 13, 18, 15, 42), 9){ IsFeeToPay = true }, // 60 (63)

			new TollPassage(new DateTime(2024, 5, 14, 9, 0, 10), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 14, 10, 0, 10), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 14, 11, 0, 10), 9){ IsFeeToPay = true }, // 27

		}, 147);

		// Test single fee within interval
		yield return new TestCaseData(new List<TollPassage>
		{
			new TollPassage(new DateTime(2024, 5, 15, 9, 0, 0), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 15, 10, 0, 0), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 15, 11, 0, 0), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 15, 11, 59, 59), 9){ IsFeeToPay = false },
			new TollPassage(new DateTime(2024, 5, 15, 12, 0, 0), 9){ IsFeeToPay = true },
			new TollPassage(new DateTime(2024, 5, 15, 13, 0, 0), 9){ IsFeeToPay = true },

		}, 45);
	}

	[Test, TestCaseSource(nameof(TollPassageTestCases))]
	public void GetTotalFeeForPassages_ReturnsCorrectTotalFee(List<TollPassage> tollPassages, int expectedFee)
	{
		// Arrange
		var tollRateProvider = A.Fake<ITollRateProvider>();

		// Act
		var actualFee = _sut.GetTotalFeeForPassages(tollPassages);

		// Assert
		Assert.That(actualFee, Is.EqualTo(expectedFee));
	}
}