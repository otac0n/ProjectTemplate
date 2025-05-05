// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Model
{
    /// <summary>
    /// Represents a fully parsed Farkle.
    /// </summary>
    /// <remarks>
    /// TODO: Replace or update this class.
    /// </remarks>
    public class Farkle
    {
        /// <summary>
        /// Gets or sets an example value derived from the file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets an example value derived from the file contents.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets an example value derived from an MSBuild item property.
        /// </summary>
        public string? Info { get; set; }
    }
}
