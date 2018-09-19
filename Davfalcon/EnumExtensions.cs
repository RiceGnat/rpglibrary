﻿using System;

namespace Davfalcon
{
	/// <summary>
	/// Extension methods for casting enums.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Converts a string name or value into an enum type.
		/// </summary>
		/// <typeparam name="T">The enum type to convert to.</typeparam>
		/// <param name="value">The enum name or value as a string.</param>
		/// <returns>The converted enum.</returns>
		public static T As<T>(this string value) where T : struct, Enum
		{
			if (!Enum.TryParse(value, out T result))
				throw new ArgumentException($"Could not parse {value} into Enum type {typeof(T)}");
			return result;
		}
	}
}
