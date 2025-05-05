// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Model
{
    using Microsoft.CodeAnalysis.Text;

    /// <summary>
    /// Encapsulates an MSBuild "FarkleFile" item.
    /// </summary>
    /// <param name="identity">The path to the item.</param>
    /// <param name="link">An optional project-relative path where the file should be considered to have been placed.</param>
    /// <param name="text">The source of the file.</param>
    /// <remarks>
    /// Updates to these items require corresponding changes to <see href="Farkle.targets"/> and <see cref="GenerateSources"/>.
    /// </remarks>
    /// <param name="myBoolOption"></param>
    /// <param name="myStringOption"></param>
    public class FarkleItem(string identity, string? link, SourceText? text, bool? myBoolOption, string? myStringOption) : BuildItem(identity, link, text)
    {
        /// <summary>
        /// Gets the name of the MSBuild element.
        /// </summary>
        public const string ItemName = "FarkleFile";

        // TODO: Customize the file attributes here.
        public bool? MyBoolOption { get; } = myBoolOption;

        public string? MyStringOption { get; } = myStringOption;
    }
}
