// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle
{
    using System;
    using System.IO;

    internal static class PathUtils
    {
        public static string GetRelativePath(string relativeTo, string path)
        {
            relativeTo = Path.GetFullPath(relativeTo);
            var fullFilePath = Path.GetFullPath(Path.Combine(relativeTo, path));
            var relativeUri = new Uri(relativeTo).MakeRelativeUri(new Uri(fullFilePath));
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            return relativePath;
        }

        public static string EncodePathAsFilename(string path) =>
            Uri.EscapeDataString(path)
                .Replace("_", "__")
                .Replace("%", "_p");
    }
}
