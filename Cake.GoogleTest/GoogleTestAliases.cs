using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.GoogleTest;

[CakeAliasCategory("GoogleTest")]
public static class GoogleTestAliases
{
	[CakeMethodAlias]
	public static bool GoogleTestRun(this ICakeContext cakeContext, FilePath executable)
	{
		return GoogleTestRun(cakeContext, executable, new GoogleTestSettings());
	}

	[CakeMethodAlias]
	public static bool GoogleTestRun(this ICakeContext cakeContext, FilePath executable, GoogleTestSettings settings)
	{
		return GoogleTestRun(cakeContext, cakeContext.ProcessRunner, executable, settings);
	}

	[CakeMethodAlias]
	public static bool GoogleTestRun(this ICakeContext cakeContext, IProcessRunner processRunner, FilePath executable)
	{
		return GoogleTestRun(cakeContext, processRunner, executable, new GoogleTestSettings());
	}

	[CakeMethodAlias]
	public static bool GoogleTestRun(this ICakeContext cakeContext, IProcessRunner processRunner, FilePath executable, GoogleTestSettings settings)
	{
		return new GoogleTestRunner(processRunner, cakeContext.FileSystem).Run(executable, settings);
	}
}