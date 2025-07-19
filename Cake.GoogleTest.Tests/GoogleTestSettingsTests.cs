using NUnit.Framework;
using Shouldly;

namespace Cake.GoogleTest.Tests;

[Category("Unit")]
internal static class the_google_test_settings_class
{
	[Test]
	public static void supports_using_a_test_filter()
	{
		var settings = new GoogleTestSettings
		{
			Filter = new TestFilter().Including("any*Test?Pattern").Excluding("any*OtherTest?Pattern")
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_filter=any*Test?Pattern:-any*OtherTest?Pattern");
	}

	[Test]
	public static void supports_running_disabled_tests()
	{
		var settings = new GoogleTestSettings
		{
			RunDisabledTests = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_also_run_disabled_tests");
	}

	[Test]
	public static void supports_failing_fast()
	{
		var settings = new GoogleTestSettings
		{
			FailFast = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_fail_fast");
	}

	[Test]
	public static void supports_repeating_tests()
	{
		var settings = new GoogleTestSettings
		{
			RepeatCount = 5
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_repeat=5");
	}

	[Test]
	public static void supports_recreating_environments_when_repeating()
	{
		var settings = new GoogleTestSettings
		{
			RecreateEnvironmentsWhenRepeating = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_recreate_environments_when_repeating");
	}

	[Test]
	public static void supports_shuffling_tests()
	{
		var settings = new GoogleTestSettings
		{
			Shuffle = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_shuffle");
	}

	[Test]
	public static void supports_setting_a_seed_for_shuffling_tests()
	{
		var settings = new GoogleTestSettings
		{
			RandomSeed = 22
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_random_seed=22");
	}

	[Test]
	public static void supports_controlling_coloured_command_line_output()
	{
		var settings = new GoogleTestSettings
		{
			ColoredOutput = ColoredOutputEnum.No
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_color=no");
	}

	[Test]
	public static void supports_only_showing_failures()
	{
		var settings = new GoogleTestSettings
		{
			OnlyDisplayFailures = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_brief");
	}

	[Test]
	public static void supports_controlling_whether_to_print_test_execution_time()
	{
		var settings = new GoogleTestSettings
		{
			PrintTestTime = false
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_print_time=0");
	}

	[Test]
	public static void supports_setting_the_output_file_path_and_type()
	{
		var settings = new GoogleTestSettings
		{
			Output = new XmlTestOutput(@"anyPath\anyFile.xml")
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe(@"--gtest_output=xml:""anyPath/anyFile.xml""");
	}

	[Test]
	public static void supports_causing_a_debug_break_on_failure()
	{
		var settings = new GoogleTestSettings
		{
			BreakOnFailure = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_break_on_failure");
	}

	[Test]
	public static void supports_throwing_an_exception_on_failure()
	{
		var settings = new GoogleTestSettings
		{
			ThrowOnFailure = true
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_throw_on_failure");
	}

	[Test]
	public static void supports_controlling_whether_to_catch_exceptions()
	{
		var settings = new GoogleTestSettings
		{
			CatchExceptions = false
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("--gtest_catch_exceptions=0");
	}

	[Test]
	public static void supports_additional_arguments()
	{
		var settings = new GoogleTestSettings
		{
			AdditionalArguments = "Any Additional Arguments"
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe("Any Additional Arguments");
	}

	[Test]
	public static void creates_an_empty_command_line_if_no_arguments_are_added()
	{
		var result = new GoogleTestSettings().GetArguments();

		result.Render().ShouldBeEmpty();
	}

	[Test]
	public static void supports_setting_all_available_arguments()
	{
		var settings = new GoogleTestSettings
		{
			ColoredOutput = ColoredOutputEnum.No,
			Filter = new TestFilter().Including("any*Test?Pattern").Excluding("any*OtherTest?Pattern"),
			Output = new XmlTestOutput(@"anyPath\anyFile.xml"),
			OnlyDisplayFailures = true,
			PrintTestTime = false,
			FailFast = true,
			RandomSeed = 22,
			RepeatCount = 5,
			RecreateEnvironmentsWhenRepeating = true,
			RunDisabledTests = true,
			Shuffle = true,
			ThrowOnFailure = true,
			BreakOnFailure = true,
			CatchExceptions = false,
			AdditionalArguments = "Any Additional Arguments"
		};

		var result = settings.GetArguments().Render();

		result.ShouldBe(
			"--gtest_filter=any*Test?Pattern:-any*OtherTest?Pattern " +
			"--gtest_also_run_disabled_tests " +
			"--gtest_fail_fast " +
			"--gtest_repeat=5 " +
			"--gtest_recreate_environments_when_repeating " +
			"--gtest_shuffle " +
			"--gtest_random_seed=22 " +
			"--gtest_color=no " +
			"--gtest_brief " +
			"--gtest_print_time=0 " +
			"--gtest_output=xml:\"anyPath/anyFile.xml\" " +
			"--gtest_break_on_failure " +
			"--gtest_throw_on_failure " +
			"--gtest_catch_exceptions=0 " +
			"Any Additional Arguments");
	}
}