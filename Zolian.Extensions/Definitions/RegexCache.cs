using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

// ReSharper disable ArrangeAttributes

namespace Zolian.Extensions.Definitions;

// ReSharper disable once PartialTypeWithSinglePart
internal static partial class RegexCache
{
    /// <summary>
    ///     A regex used to find structured message templates in a string
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Tested by string.Inject tests")]
    [GeneratedRegex(@"\{\{|\{[^{}]+\}|\}\}")]
    internal static partial Regex StringProcessingRegex { get; }
}