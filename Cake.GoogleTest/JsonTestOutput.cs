using System;
using Cake.Core.IO;

namespace Cake.GoogleTest;

public sealed class JsonTestOutput : TestOutput
{
	public FilePath OutputFile { get; }

	public JsonTestOutput(FilePath outputFile)
	{
		OutputFile = outputFile ?? throw new ArgumentNullException(nameof(outputFile));
	}

	protected override string GetTypeIdentifier()
	{
		return "json";
	}

	protected override FilePath GetOutputFile()
	{
		return OutputFile;
	}
}