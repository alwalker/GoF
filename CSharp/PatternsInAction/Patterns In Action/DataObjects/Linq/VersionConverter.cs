using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;

namespace DataObjects.Linq
{
    /// <summary>
    /// Helper class to facilitate Binary timestamp conversions.
    /// </summary>
    public static class VersionConverter
    {
        /// <summary>
        /// Converts binary value to string.
        /// </summary>
        /// <param name="version">Binary version number.</param>
        /// <returns>Base64 version number.</returns>
        public static string ToString(Binary version)
        {
            if (version == null)
                return null;

            return Convert.ToBase64String(version.ToArray());
        }

        /// <summary>
        /// Converts string to binary value.
        /// </summary>
        /// <param name="version">Base64 version number.</param>
        /// <returns>Binary version number.</returns>
        public static Binary ToBinary(string version)
        {
            if (string.IsNullOrEmpty(version))
                return null;

            return new Binary(Convert.FromBase64String(version));
        }
    }
}
