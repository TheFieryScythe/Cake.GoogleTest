using System;
using System.IO;
using Cake.Core.IO;

namespace Cake.GoogleTest;

public sealed class GoogleTestRunner
{
	public GoogleTestRunner(IProcessRunner processRunner, IFileSystem fileSystem)
	{
		_processRunner = processRunner ?? throw new ArgumentNullException(nameof(processRunner));
		_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
	}

	public bool Run(FilePath executable, GoogleTestSettings settings)
	{
		if (!_fileSystem.Exist(executable))
		{
			throw new FileNotFoundException($"Google test executable not found: {executable.FullPath}", executable.FullPath);
		}

		var process = _processRunner.Start(
			executable,
			new ProcessSettings
			{
				Arguments = settings.GetArguments(),
				RedirectStandardOutput = false,
				RedirectStandardError = false,
			});
		process.WaitForExit();
		return process.GetExitCode() == 0;
	}

	private readonly IFileSystem _fileSystem;
	private readonly IProcessRunner _processRunner;
}