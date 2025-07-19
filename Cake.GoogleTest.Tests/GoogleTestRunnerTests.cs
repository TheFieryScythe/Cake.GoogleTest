using System;
using System.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Testing;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Cake.GoogleTest.Tests;

[Category("Unit")]
internal static class a_google_test_runner
{
	[Test]
	public static void fails_if_the_executable_is_missing()
	{
		var runner = new GoogleTestRunnerBuilder().Build();

		Should.Throw<FileNotFoundException>(() => runner.Run("missing.exe", new GoogleTestSettings()));
	}

	[Test]
	public static void requires_a_file_system()
	{
		var environment = new FakeEnvironment(PlatformFamily.Unknown)
		{
			WorkingDirectory = "/anyDirectory"
		};
		var fakeFileSystem = new FakeFileSystem(environment);

		Should.Throw<ArgumentNullException>(() =>
		{
			_ = new GoogleTestRunner(
				new ProcessRunner(fakeFileSystem, environment, new NullLog(), null, null),
				null);
		});
	}

	[Test]
	public static void requires_a_process_runner()
	{
		var environment = new FakeEnvironment(PlatformFamily.Unknown)
		{
			WorkingDirectory = "/anyDirectory"
		};
		var fakeFileSystem = new FakeFileSystem(environment);

		Should.Throw<ArgumentNullException>(() => _ = new GoogleTestRunner(null, fakeFileSystem));
	}

	[Test]
	public static void returns_false_if_the_run_was_not_successful()
	{
		var runner = new GoogleTestRunnerBuilder()
			.WithTestProcess("anyTest.exe", false)
			.Build();

		var result = runner.Run("anyTest.exe", new GoogleTestSettings());

		result.ShouldBeFalse();
	}

	[Test]
	public static void returns_true_if_the_run_was_successful()
	{
		var runner = new GoogleTestRunnerBuilder()
			.WithTestProcess("anyTest.exe", true)
			.Build();

		var result = runner.Run("anyTest.exe", new GoogleTestSettings());

		result.ShouldBeTrue();
	}

	private sealed class GoogleTestRunnerBuilder
	{
		public GoogleTestRunnerBuilder()
		{
			_processRunner = Substitute.For<IProcessRunner>();
			_fileSystem = new FakeFileSystem(
				new FakeEnvironment(PlatformFamily.Unknown)
				{
					WorkingDirectory = "/anyDirectory"
				});
		}

		public GoogleTestRunnerBuilder WithTestProcess(string fileName, bool passes)
		{
			_fileSystem.CreateFile(fileName);

			var process = Substitute.For<IProcess>();
			var returnValue = passes
				? 0
				: -1;
			process.GetExitCode().Returns(returnValue);

			_processRunner
				.Start(Arg.Is<FilePath>(value => value.FullPath == fileName), Arg.Any<ProcessSettings>())
				.Returns(process);
			return this;
		}

		public GoogleTestRunner Build()
		{
			return new GoogleTestRunner(_processRunner, _fileSystem);
		}

		private readonly FakeFileSystem _fileSystem;
		private readonly IProcessRunner _processRunner;
	}
}