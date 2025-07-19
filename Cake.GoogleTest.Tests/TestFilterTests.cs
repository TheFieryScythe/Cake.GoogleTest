using NUnit.Framework;
using Shouldly;

namespace Cake.GoogleTest.Tests;

[Category("Unit")]
internal static class when_a_test_filter_is_composed
{
	[Test]
	public static void the_command_line_is_empty_if_no_includes_or_excludes_are_added()
	{
		var filter = new TestFilter();

		var result = filter.ToString();

		result.ShouldBeEmpty();
	}

	[Test]
	public static void the_command_line_has_an_include_if_one_has_been_added()
	{
		var filter = new TestFilter().Including("any*Test?Pattern");

		var result = filter.ToString();

		result.ShouldBe("--gtest_filter=any*Test?Pattern");
	}

	[Test]
	public static void the_command_line_has_an_exclude_if_one_has_been_added()
	{
		var filter = new TestFilter().Excluding("any*Test?Pattern");

		var result = filter.ToString();

		result.ShouldBe("--gtest_filter=-any*Test?Pattern");
	}

	[Test]
	public static void the_command_line_has_excludes_and_includes_if_any_have_been_added()
	{
		var filter = new TestFilter().Including("any*Test?Pattern").Excluding("any?OtherTest*Pattern");

		var result = filter.ToString();

		result.ShouldBe("--gtest_filter=any*Test?Pattern:-any?OtherTest*Pattern");
	}
}