using System;
using Cake.Core.IO;

namespace Cake.GoogleTest;

public sealed class XmlTestOutput : TestOutput
{
	public FilePath OutputFile { get; }

	public XmlTestOutput(FilePath outputFile)
	{
		OutputFile = outputFile ?? throw new ArgumentNullException(nameof(outputFile));
	}

	protected override string GetTypeIdentifier()
	{
		return "xml";
	}

	protected override FilePath GetOutputFile()
	{
		return OutputFile;
	}
}