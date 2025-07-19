using Cake.Core.IO;

namespace Cake.GoogleTest;

public abstract class TestOutput
{
	protected abstract string GetTypeIdentifier();
	protected abstract FilePath GetOutputFile();

	public override string ToString()
	{
		var identifier = GetTypeIdentifier();
		var outputFile = GetOutputFile();

		return $"--gtest_output={identifier}:\"{outputFile}\"";
	}
}