using System.Collections.Generic;
using System.Linq;

namespace Cake.GoogleTest;

public sealed class TestFilter
{
	public TestFilter Including(string pattern)
	{
		_includePatterns.Add(pattern);
		return this;
	}

	public TestFilter Excluding(string pattern)
	{
		_excludePatterns.Add(pattern);
		return this;
	}

	public override string ToString()
	{
		if (!_includePatterns.Any() && !_excludePatterns.Any())
		{
			return string.Empty;
		}

		var allPatterns = _includePatterns.Concat(_excludePatterns.Select(entry => "-" + entry));
		return "--gtest_filter=" + string.Join(":", allPatterns);
	}

	private readonly List<string> _excludePatterns = [];
	private readonly List<string> _includePatterns = [];
}