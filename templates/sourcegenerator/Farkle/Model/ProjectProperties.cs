// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Model
{
    /// <summary>
    /// The project properties gathered from the MSBuild context.
    /// </summary>
    /// <param name="projectDir">The root directory of the project.</param>
    /// <param name="rootNamespace">The root namespace for code items.</param>
    public class ProjectProperties(string? projectDir, string? rootNamespace)
    {
        /// <summary>
        /// Gets the root directory of the project.
        /// </summary>
        public string? ProjectDir { get; } = projectDir;

        /// <summary>
        /// Gets the root namespace for code items.
        /// </summary>
        public string? RootNamespace { get; } = rootNamespace;
    }
}
