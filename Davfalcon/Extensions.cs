using System;
using System.Collections.Generic;

namespace Davfalcon
{
	/// <summary>
	/// Extension methods.
	/// </summary>
	public static class Extensions
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

		/// <summary>
		/// Gets the type of an enum from its full type name.
		/// </summary>
		/// <param name="typeName">The string name of the enum type.</param>
		/// <returns>The type of the enum if it is loaded; otherwise, <c>null</c>.</returns>
		public static Type GetEnumType(this string typeName)
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var type = assembly.GetType(typeName);
				if (type == null)
					continue;
				if (type.IsEnum)
					return type;
			}
			return null;
		}

		public static EnumString[] ConvertEnumArray(this Enum[] array)
			=> Array.ConvertAll<Enum, EnumString>(array, e => e);

		public static IReadOnlyCollection<T> ToNewReadOnlyCollectionSafe<T>(this IEnumerable<T> collection)
			=> (collection == null ? new List<T>() : new List<T>(collection)).AsReadOnly();
	}
}
