#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;

namespace USP.Express.Pro
{
	/// <summary>
	/// TypeConverter. Contains methods related to conversion between types.
	/// </summary>
	public class TypeConverter
	{
		public TypeConverter() {}

		/// <summary>
		/// Returns a value indicating whether the specified SourceType can be converted to the specified DestinationType.
		/// </summary>
		/// <param name="SourceType">Source type.</param>
		/// <param name="DestinationType">Destination type.</param>
		/// <returns>True, if the specified SourceType can be converted to the specified DestinationType; otherwise, false.</returns>
		public static bool CanConvert(Type SourceType, Type DestinationType)
		{
			// 040723.SVA why not to use Type.IsAssignableFrom() - because it does not address explicit conversions with Convert.To..
			// see ms-help://MS.VSCC.2003/MS.MSDNQTR.2003JUL.1033/cpguide/html/cpcontypeconversiontables.htm
			//		 ms-help://MS.VSCC.2003/MS.MSDNQTR.2003JUL.1033/csref/html/vclrfExplicitNumericConversionsTable.htm

			if (SourceType.Equals(DestinationType))
				return true;

			if (SourceType.Equals(typeof(System.Byte)))
			{
				return 
					(DestinationType.Equals(typeof(System.UInt16)) || 
					DestinationType.Equals(typeof(System.Int16)) ||
					DestinationType.Equals(typeof(System.UInt32)) ||
					DestinationType.Equals(typeof(System.Int32)) ||
					DestinationType.Equals(typeof(System.UInt64)) ||
					DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Single)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.SByte)))
			{
				return 
					(DestinationType.Equals(typeof(System.Int16)) ||
					DestinationType.Equals(typeof(System.Int32)) ||
					DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Single)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.Int16)))
			{
				return 
					(DestinationType.Equals(typeof(System.Int32)) ||
					DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Single)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.UInt16)))
			{
				return 
					(DestinationType.Equals(typeof(System.UInt32)) ||
					DestinationType.Equals(typeof(System.Int32)) ||
					DestinationType.Equals(typeof(System.UInt64)) ||
					DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Single)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.Char)))
			{
				return 
					(DestinationType.Equals(typeof(System.UInt16)) || 
					DestinationType.Equals(typeof(System.UInt32)) ||
					DestinationType.Equals(typeof(System.Int32)) ||
					DestinationType.Equals(typeof(System.UInt64)) ||
					DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Single)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.Int32)))
			{
				return 
					(DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.UInt32)))
			{
				return 
					(DestinationType.Equals(typeof(System.Int64)) ||
					DestinationType.Equals(typeof(System.Double)) ||
					DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.Int64)))
			{
				return 
					(DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.UInt64)))
			{
				return 
					(DestinationType.Equals(typeof(System.Decimal)));
			}
			else if (SourceType.Equals(typeof(System.Single)))
			{
				return 
					(DestinationType.Equals(typeof(System.Double)));
			}
			else
			{
				return false;
			}

		}
	}

}
