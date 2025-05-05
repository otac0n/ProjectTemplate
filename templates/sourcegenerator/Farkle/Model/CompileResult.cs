// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Model
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Encapsulates the results and diagnostics from the compilation of a resource.
    /// </summary>
    /// <typeparam name="T">The type resulting from compilation.</typeparam>
    public class CompileResult<T>
    {
        /// <summary>
        /// Gets or sets the value resulting from compilation.
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Gets the collection of diagnostics that occurred during compilation.
        /// </summary>
        public List<Diagnostic> Diagnostics { get; } = [];

        /// <summary>
        /// Processes the diagnostics in the result and keeps track of the severity.
        /// </summary>
        /// <param name="logDiagnostic">An action to invoke for any diagnostic.</param>
        /// <returns><c>true</c>, if there were fatal errors; <c>false</c>, otherwise.</returns>
        public bool ReportDiagnostics(Action<Diagnostic> logDiagnostic)
        {
            var hadFatal = false;
            foreach (var error in this.Diagnostics)
            {
                hadFatal |= error.Severity == DiagnosticSeverity.Error;
                logDiagnostic(error);
            }

            return hadFatal;
        }
    }
}
