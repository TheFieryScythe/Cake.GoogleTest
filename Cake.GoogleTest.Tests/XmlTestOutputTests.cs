using System;
using NUnit.Framework;
using Shouldly;

namespace Cake.GoogleTest.Tests;

[Category("Unit")]
internal static class an_xml_test_output
{
	[Test]
	public static void requires_a_file_path()
	{
		Should.Throw<ArgumentException>(() => _ = new XmlTestOutput(null));
	}

	[Test]
	public static void produces_a_command_line_option_containing_the_xml_file_path()
	{
		var output = new XmlTestOutput(@"anyPath\anyFile.xml");

		var result = output.ToString();

		result.ShouldBe(@"--gtest_output=xml:""anyPath/anyFile.xml""");
	}
}