using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extended version of the string.Contains() method, 
        /// accepting a [StringComparison] object to perform different kind of comparisons
        /// </summary>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source?.IndexOf(value, comparisonType) >= 0;
        }
    }
}