// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AtYourService.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            Contract.Requires(collection != null);
            Contract.Requires(action != null);

            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
