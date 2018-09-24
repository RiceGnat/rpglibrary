﻿using System;
using System.Runtime.Serialization;

namespace Davfalcon
{
	/// <summary>
	/// Class to serialize enums as strings.
	/// </summary>
	[Serializable]
	public sealed class EnumString
	{
		private readonly string str;
		private readonly string type;

		[NonSerialized]
		private Type t;

		/// <summary>
		/// Create a new <see cref="EnumString"/> from the specified enum value.
		/// </summary>
		/// <param name="e">The enum to convert.</param>
		public EnumString(Enum e)
		{
			str = e.ToString();
			t = e.GetType();
			type = t.ToString();
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			t = type.GetEnumType();
		}

		/// <summary>
		/// Determines whether the specified object is 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
			=> str.Equals(obj.ToString()) &&
			!(obj is Enum && !type.Equals(obj.GetType().ToString())) &&
			!(obj is EnumString && !type.Equals((obj as EnumString).type));

		public override string ToString()
			=> str;

		public override int GetHashCode()
			=> (type + str).GetHashCode();

		public static implicit operator EnumString(Enum e)
			=> new EnumString(e);

		public static implicit operator Enum(EnumString es)
			=> (Enum)Enum.Parse(es.t, es.str);

		public static implicit operator string(EnumString es)
			=> es.ToString();

		public static bool operator ==(EnumString a, EnumString b)
			=> a.Equals(b);

		public static bool operator !=(EnumString a, EnumString b)
			=> !a.Equals(b);
	}
}