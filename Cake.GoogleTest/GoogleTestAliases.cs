using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.GoogleTest;

[CakeAliasCategory("GoogleTest")]
public static class GoogleTestAliases
{
	/// <summary>
	///     Execute a Google test executable.
	/// </summary>
	/// <param name="cakeContext">The current Cake context</param>
	/// <param name="executable">Path to the Google test executable</param>
	/// <returns>Returns whether the executable ran successfully</returns>
	[CakeMethodAlias]
	public static bool GoogleTestRun(
		this ICakeContext cakeContext,
		FilePath executable)
	{
		return GoogleTestRun(cakeContext, executable, new GoogleTestSettings());
	}

	/// <summary>
	///     Execute a Google test executable with additional settings.
	/// </summary>
	/// <param name="cakeContext">The current Cake context</param>
	/// <param name="executable">Path to the Google test executable</param>
	/// <param name="settings">The Google test settings to pass to the test executable</param>
	/// <returns>Returns whether the executable ran successfully</returns>
	[CakeMethodAlias]
	public static bool GoogleTestRun(
		this ICakeContext cakeContext,
		FilePath executable,
		GoogleTestSettings settings)
	{
		return GoogleTestRun(cakeContext, cakeContext.ProcessRunner, executable, settings);
	}


	/// <summary>
	///     Execute a Google test executable with a custom process runner.
	/// </summary>
	/// <param name="cakeContext">The current Cake context</param>
	/// <param name="processRunner">A custom process runner</param>
	/// <param name="executable">Path to the Google test executable</param>
	/// <returns>Returns whether the executable ran successfully</returns>
	[CakeMethodAlias]
	public static bool GoogleTestRun(
		this ICakeContext cakeContext,
		IProcessRunner processRunner,
		FilePath executable)
	{
		return GoogleTestRun(cakeContext, processRunner, executable, new GoogleTestSettings());
	}

	/// <summary>
	///     Execute a Google test executable with a custom process runner and additional settings.
	/// </summary>
	/// <param name="cakeContext">The current Cake context</param>
	/// <param name="processRunner">A custom process runner</param>
	/// <param name="executable">Path to the Google test executable</param>
	/// <param name="settings">The Google test settings to pass to the test executable</param>
	/// <returns>Returns whether the executable ran successfully</returns>
	[CakeMethodAlias]
	public static bool GoogleTestRun(
		this ICakeContext cakeContext,
		IProcessRunner processRunner,
		FilePath executable,
		GoogleTestSettings settings)
	{
		return new GoogleTestRunner(processRunner, cakeContext.FileSystem).Run(executable, settings);
	}
}