using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.GoogleTest;

public sealed class GoogleTestSettings : ToolSettings
{
	public ColoredOutputEnum ColoredOutput = ColoredOutputEnum.Auto;
	public TestFilter Filter = null;
	public TestOutput Output = null;
	public bool OnlyDisplayFailures = false;
	public bool PrintTestTime = true;
	public bool FailFast = false;
	public uint RandomSeed = 0;
	public uint RepeatCount = 0;
	public bool RecreateEnvironmentsWhenRepeating = false;
	public bool RunDisabledTests = false;
	public bool Shuffle = false;
	public bool ThrowOnFailure = false;
	public bool BreakOnFailure = false;
	public bool CatchExceptions = true;
	public string AdditionalArguments = null;

	public ProcessArgumentBuilder GetArguments()
	{
		var builder = new ProcessArgumentBuilder();

		if (Filter != null)
		{
			builder.Append(Filter.ToString());
		}

		if (RunDisabledTests)
		{
			builder.Append("--gtest_also_run_disabled_tests");
		}

		if (FailFast)
		{
			builder.Append("--gtest_fail_fast");
		}

		if (RepeatCount > 0)
		{
			builder.Append($"--gtest_repeat={RepeatCount}");
		}

		if (RecreateEnvironmentsWhenRepeating)
		{
			builder.Append("--gtest_recreate_environments_when_repeating");
		}

		if (Shuffle)
		{
			builder.Append("--gtest_shuffle");
		}

		if (RandomSeed > 0)
		{
			builder.Append($"--gtest_random_seed={RandomSeed}");
		}

		if (ColoredOutput != ColoredOutputEnum.Auto)
		{
			builder.Append($"--gtest_color={ColoredOutput.ToString().ToLower()}");
		}

		if (OnlyDisplayFailures)
		{
			builder.Append("--gtest_brief");
		}

		if (!PrintTestTime)
		{
			builder.Append("--gtest_print_time=0");
		}

		if (Output != null)
		{
			builder.Append(Output.ToString());
		}

		if (BreakOnFailure)
		{
			builder.Append("--gtest_break_on_failure");
		}

		if (ThrowOnFailure)
		{
			builder.Append("--gtest_throw_on_failure");
		}

		if (!CatchExceptions)
		{
			builder.Append("--gtest_catch_exceptions=0");
		}

		if (!string.IsNullOrWhiteSpace(AdditionalArguments))
		{
			builder.Append(AdditionalArguments);
		}

		return builder;
	}
}