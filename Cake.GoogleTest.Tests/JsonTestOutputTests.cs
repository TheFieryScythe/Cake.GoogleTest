using System;
using NUnit.Framework;
using Shouldly;

namespace Cake.GoogleTest.Tests;

[Category("Unit")]
internal static class a_json_test_output
{
	[Test]
	public static void requires_a_file_path()
	{
		Should.Throw<ArgumentException>(() => _ = new JsonTestOutput(null));
	}

	[Test]
	public static void produces_a_command_line_option_containing_the_json_file_path()
	{
		var output = new JsonTestOutput(@"anyPath\anyFile.json");

		var result = output.ToString();

		result.ShouldBe(@"--gtest_output=json:""anyPath/anyFile.json""");
	}
}