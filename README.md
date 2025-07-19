# Cake.GoogleTest

A Cake build extension for executing Google test assemblies.

## Installation

Cake.GoogleTest can be installed from the package manager console:

```csharp
Install-Package Cake.GoogleTest
```

It can also be used via a reference in your Cake build script using the `#addin` directive:

```csharp
#addin nuget:?package=Cake.GoogleTest
```

## Usage

Assuming you have built your native test assembly which uses Google test:

```csharp
var result = GoogleTestRun(GetRootedPath(executable));
if(!result)
{
    Error("A failure occurred when running tests");
}
```

This will result in the test application being executed and the results being output to the console.

If you want to write test results to a file you can specify the output by providing a settings instance:

```csharp
var result = GoogleTestRun(GetRootedPath(executable), new GoogleTestSettings
{
    Output = new XmlTestOutput(GetRootedPath(outputFile))
});
if(!result)
{
    Error("A failure occurred when running tests");
}
```

Test results can be output to Xml or Json.

If you need to use a custom process runner, as you may do when collecting coverage information, you can provide it like
so:

```csharp
var processRunner = new CustomProcessRunner();

var result = GoogleTestRun(processRunner, GetRootedPath(executable), new GoogleTestSettings
{
    Output = new XmlTestOutput(GetRootedPath(outputFile))
});
if(!result)
{
    Error("A failure occurred when running tests");
}
```

## Settings

The `GoogleTestSettings` class implements most of the settings supported by a Google test assembly. Any that are not can
be provided via the `AdditionalArguments` field.

| Field                             | Corresponding Setting                          |
|-----------------------------------|------------------------------------------------|
| ColoredOutput                     | `--gtest_color`                                | 
| Filter                            | `--gtest_filter`                               |
| Output                            | `--gtest_output`                               |
| OnlyDisplayFailures               | `--gtest_brief`                                |
| PrintTestTime                     | `--gtest_print_time`                           |
| FailFast                          | `--gtest_fail_fast`                            |
| RandomSeed                        | `--gtest_random_seed`                          |
| RepeatCount                       | `--gtest_repeat`                               |
| RecreateEnvironmentsWhenRepeating | `--gtest_recreate_environments_when_repeating` |
| RunDisabledTests                  | `--gtest_also_run_disabled_tests`              |
| Shuffle                           | `--gtest_shuffle`                              |
| ThrowOnFailure                    | `--gtest_throw_on_failure`                     |
| BreakOnFailure                    | `--gtest_break_on_failure`                     |
| CatchExceptions                   | `--gtest_catch_exceptions`                     |
| AdditionalArguments               | None                                           |