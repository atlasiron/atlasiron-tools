#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;

using USP.Express.Pro.Exceptions;

namespace USP.Express.Pro.Operators
{
	// -- Built-in Operators: --

	/// <summary>
	/// Plus modifier.
	/// </summary>
	public class PlusModifier : Operator
	{
		public PlusModifier() {}
		internal override OperatorType GetOperatorType() { return OperatorType.plusModifier; }
		public override string Name { get { return "+"; } }
		public override byte OperandsSupported { get { return 1; } }

		internal override object Evaluate(object[] Values)
		{
			return Values[0];
		}
	}

	/// <summary>
	/// Minus modifier.
	/// </summary>
	public class MinusModifier : Operator
	{
		public MinusModifier() {}
		internal override OperatorType GetOperatorType() { return OperatorType.minusModifier; }
		public override string Name { get { return "-"; } }
		public override byte OperandsSupported { get { return 1; } }

		internal override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return -dValue;
		}
	}


	// === Arithmetic Operators ===


	/// <summary>
	/// Plus operator.
	/// </summary>
	public class PlusOperator : Operator
	{
		public PlusOperator(){}
		internal override OperatorType GetOperatorType() { return OperatorType.plusOperator; }
		public override string Name { get { return "+"; } }

		public override Type GetReturnType( Type[] Types )
		{ 
			if 
			( 
				TypeConverter.CanConvert( Types[ 0 ], typeof( double ) ) && 
				TypeConverter.CanConvert( Types[ 1 ], typeof( double ) )
			)
			{
				return typeof( double );
			}
			
			return typeof( string );
		}
		
		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool b1 = TypeConverter.CanConvert( Types[ 0 ], typeof( double ) );
			bool b2 = TypeConverter.CanConvert( Types[ 1 ], typeof( double ) );

			if ( b1 && b2 )
			{
				return true;	
			}

			b1 = Types[ 0 ].Equals( typeof( string ) );
			b2 = Types[ 1 ].Equals( typeof( string ) );

			if ( b1 && b2 )
			{
				return true;	
			}
			
			b1 = TypeConverter.CanConvert( Types[ 0 ], typeof( double ) );
			b2 = Types[ 1 ].Equals( typeof( string ) );

			if ( b1 && b2 )
			{
				return true;	
			}

			b1 = Types[ 0 ].Equals( typeof( string ) );
			b2 = TypeConverter.CanConvert( Types[ 1 ], typeof( double ) );

			if ( b1 && b2 )
			{
				return true;	
			}
			
			InvalidArgument = 0;
			return false;
		}
		
		internal override object Evaluate(object[] Values)
		{
			if ( Values[0].GetType().Equals(typeof(String)) || Values[1].GetType().Equals(typeof(String)))
				return Values[0].ToString() + Values[1].ToString(); 

			else if ( TypeConverter.CanConvert( Values[ 0 ].GetType(), typeof( double ) ) &&
				TypeConverter.CanConvert( Values[ 1 ].GetType(), typeof( double ) )	)
			{
				return Convert.ToDouble( Values[ 0 ] ) + Convert.ToDouble( Values[ 1 ] );
			}
			else
			{
				throw (new InvalidArgumentException());
			}
		}
	}

	/// <summary>
	/// Minus operator.
	/// </summary>
	public class MinusOperator : Operator
	{
		public MinusOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.minusOperator; }
		public override string Name { get { return "-"; } }

		internal override object Evaluate(object[] Values)
		{
			#if TRIAL
				throw (new OperatorNotSupportedInTrialVersionException(0));
				//return 0;
			#else
			double dValue0 = Convert.ToDouble(Values[0]);
			double dValue1 = Convert.ToDouble(Values[1]);
			return (dValue0 - dValue1);
			#endif
		}
	}

	/// <summary>
	/// Multiply operator.
	/// </summary>
	public class MultiplyOperator : Operator
	{
		public MultiplyOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.multiplyOperator; }
		public override string Name { get { return "*"; } }

		internal override object Evaluate(object[] Values)
		{
			double dValue0 = Convert.ToDouble(Values[0]);
			double dValue1 = Convert.ToDouble(Values[1]);
			return (dValue0 * dValue1);
		}

	}

	/// <summary>
	/// Divide operator.
	/// </summary>
	public class DivideOperator : Operator
	{
		public DivideOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.divideOperator; }
		public override string Name { get { return "/"; } }

		internal override object Evaluate(object[] Values)
		{
			#if TRIAL
				throw (new OperatorNotSupportedInTrialVersionException(0));
				//return 0;
			#else
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);

				if (dValue1.Equals((double) 0))
					throw (new DivisionByZeroException());

				return (dValue0 / dValue1);
			#endif
		}
	}

	/// <summary>
	/// Modulus operator.
	/// </summary>
	public class ModulusOperator : Operator
	{
		public ModulusOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.modulusOperator; }
		public override string Name { get { return "%"; } }

		internal override object Evaluate(object[] Values)
		{
			double dValue0 = Convert.ToDouble(Values[0]);
			double dValue1 = Convert.ToDouble(Values[1]);

			if (dValue1.Equals((double) 0))
				throw (new DivisionByZeroException());

			return dValue0 % dValue1;
		}
	}

	/// <summary>
	/// Power operator.
	/// </summary>
	public class PowerOperator : Operator
	{
		public PowerOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.powerOperator; }
		public override string Name { get { return "^"; } }

		internal override object Evaluate(object[] Values)
		{
			double dNumber = Convert.ToDouble(Values[0]);
			double dPower = Convert.ToDouble(Values[1]);

			if (dNumber.Equals((double) 0) && dPower < (double) 0)
				throw (new DivisionByZeroException());

			if (ExpressionTree.NegativeOddRoot && dNumber < (double) 0)
			{
				int nRoot = Convert.ToInt32(1/dPower);

				if ((double) nRoot != ((double) 1)/Convert.ToDouble(Values[1])) // to check if it is integer in run-time
					throw (new InvalidArgumentException());

				// only odd roots of negative numbers are allowed
				if( nRoot % 2 == 0 )	
					throw (new SqrtNegNumberException());

				return (-1) * Math.Pow(Math.Abs(dNumber), dPower);
			}
			else
				return Math.Pow( dNumber, dPower );
		}
	}


	// === Comparison Operators ===


	/// <summary>
	/// IsLessThan operator.
	/// </summary>
	public class IsLessThanOperator : Operator
	{
		public IsLessThanOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isLessThanOperator; }
		public override string Name { get { return "<"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				if (!Types[1].Equals(typeof(DateTime)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(String)))
			{
				if (!Types[1].Equals(typeof(String)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( Values[0].GetType().Equals(typeof(DateTime)) )
			{
				// compare dates
				DateTime dtDate0 = Convert.ToDateTime(Values[0]);
				DateTime dtDate1 = Convert.ToDateTime(Values[1]);
				return ( DateTime.Compare(dtDate0, dtDate1) < 0 );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) < 0 );
			}
			else
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( dValue0 < dValue1 );
			}
		}
	}

	/// <summary>
	/// IsGreaterThan operator.
	/// </summary>
	public class IsGreaterThanOperator : Operator
	{
		public IsGreaterThanOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isGreaterThanOperator; }
		public override string Name { get { return ">"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				if (!Types[1].Equals(typeof(DateTime)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(String)))
			{
				if (!Types[1].Equals(typeof(String)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( Values[0].GetType().Equals(typeof(DateTime)) )
			{
				// compare dates
				DateTime dtDate0 = Convert.ToDateTime(Values[0]);
				DateTime dtDate1 = Convert.ToDateTime(Values[1]);
				return ( DateTime.Compare(dtDate0, dtDate1) > 0 );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) > 0 );
			}
			else
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( dValue0 > dValue1 );
			}
		}
	}

	/// <summary>
	/// IsEqualTo operator.
	/// </summary>
	public class IsEqualToOperator : Operator
	{
		public IsEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isEqualToOperator; }
		public override string Name { get { return "=="; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				if (!Types[0].Equals(Types[1]))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( TypeConverter.CanConvert(Values[0].GetType(), typeof(Double)) && TypeConverter.CanConvert(Values[1].GetType(), typeof(Double)) )
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( dValue0.Equals(dValue1) );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) == 0 );
			}
			else
			{
				// compare other objects
				return ( Values[0].Equals(Values[1]) );
			}
		}

		public override bool IsNullable(object[] aValues)
		{
			if (ExpressionTree.ANSI_Nulls)
				return base.IsNullable(aValues);
			else
				return false;
		}
	}

	/// <summary>
	/// Equality ("=") operator.
	/// </summary>
	public class IsBasicEqualToOperator : IsEqualToOperator
	{
		public IsBasicEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isBasicEqualToOperator; }
		public override string Name { get { return "="; } }
	}

	/// <summary>
	/// Inequality ("!=") operator.
	/// </summary>
	public class IsNotEqualToOperator : Operator
	{
		public IsNotEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isNotEqualToOperator; }
		public override string Name { get { return "!="; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				if (!Types[0].Equals(Types[1]))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( TypeConverter.CanConvert(Values[0].GetType(), typeof(Double)) && TypeConverter.CanConvert(Values[1].GetType(), typeof(Double)) )
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( !dValue0.Equals(dValue1) );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) != 0 );
			}
			else
			{
				// compare other objects
				return ( !Values[0].Equals(Values[1]) );
			}
		}

		public override bool IsNullable(object[] aValues)
		{
			if (ExpressionTree.ANSI_Nulls)
				return base.IsNullable(aValues);
			else
				return false;
		}
	}

	/// <summary>
	/// Inequality ("&lt;&gt;") operator.
	/// </summary>
	public class IsBasicNotEqualToOperator : IsNotEqualToOperator
	{
		public IsBasicNotEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isBasicNotEqualToOperator; }
		public override string Name { get { return "<>"; } }
	}

	/// <summary>
	/// IsLessThanOrEqualTo operator.
	/// </summary>
	public class IsLessThanOrEqualToOperator : Operator
	{
		public IsLessThanOrEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isLessThanOrEqualToOperator; }
		public override string Name { get { return "<="; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				if (!Types[1].Equals(typeof(DateTime)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(String)))
			{
				if (!Types[1].Equals(typeof(String)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( Values[0].GetType().Equals(typeof(DateTime)) )
			{
				// compare dates
				DateTime dtDate0 = Convert.ToDateTime(Values[0]);
				DateTime dtDate1 = Convert.ToDateTime(Values[1]);
				return ( DateTime.Compare(dtDate0, dtDate1) <= 0 );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) <= 0 );
			}
			else
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( dValue0 <= dValue1 );
			}
		}
	}

	/// <summary>
	/// IsGreaterThanOrEqualTo operator.
	/// </summary>
	public class IsGreaterThanOrEqualToOperator : Operator
	{
		public IsGreaterThanOrEqualToOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.isGreaterThanOrEqualToOperator; }
		public override string Name { get { return ">="; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				if (!Types[1].Equals(typeof(DateTime)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else if (Types[0].Equals(typeof(String)))
			{
				if (!Types[1].Equals(typeof(String)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			if ( Values[0].GetType().Equals(typeof(DateTime)) )
			{
				// compare dates
				DateTime dtDate0 = Convert.ToDateTime(Values[0]);
				DateTime dtDate1 = Convert.ToDateTime(Values[1]);
				return ( DateTime.Compare(dtDate0, dtDate1) >= 0 );
			}
			else if ( Values[0].GetType().Equals(typeof(String)) )
			{
				// compare strings
				string sValue0 = Convert.ToString(Values[0]);
				string sValue1 = Convert.ToString(Values[1]);
				return ( String.Compare(sValue0, sValue1, !IsCaseSensitive) >= 0 );
			}
			else
			{
				// compare numbers
				double dValue0 = Convert.ToDouble(Values[0]);
				double dValue1 = Convert.ToDouble(Values[1]);
				return ( dValue0 >= dValue1 );
			}
		}
	}


	// === Logical Operators ===


	/// <summary>
	/// And operator.
	/// </summary>
	public class AndOperator : Operator
	{
		public AndOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.andOperator; }
		public override string Name { get { return "&&"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (Types[0].Equals(typeof(Boolean)))
			{
				if (!Types[1].Equals(typeof(Boolean)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{

			return staticEvaluate(Values);

		}

		public static object staticEvaluate(object[] Values)
		{
			object bResult;

			if (Convert.IsDBNull(Values[0]))
				if (TypeConverter.CanConvert(Values[1].GetType(), typeof(bool)))
					if (Convert.ToBoolean(Values[1]))
						bResult = System.DBNull.Value;
					else
						bResult = false;
				else
					bResult = System.DBNull.Value;
			else if (Convert.IsDBNull(Values[1]))
				if (TypeConverter.CanConvert(Values[0].GetType(), typeof(bool)))
					if (Convert.ToBoolean(Values[0]))
						bResult = System.DBNull.Value;
					else
						bResult = false;
				else
					bResult = System.DBNull.Value;
			else
				bResult = Convert.ToBoolean(Values[0]) && Convert.ToBoolean(Values[1]);

			return bResult;
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

	}

	/// <summary>
	/// AndBasic operator.
	/// </summary>
	public class AndBasicOperator : AndOperator
	{
		public AndBasicOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.andBasicOperator; }
		public override string Name { get { return "AND"; } }
	}

	/// <summary>
	/// Or operator.
	/// </summary>
	public class OrOperator : Operator
	{
		public OrOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.orOperator; }
		public override string Name { get { return "||"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (Types[0].Equals(typeof(Boolean)))
			{
				if (!Types[1].Equals(typeof(Boolean)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{
			return staticEvaluate(Values);
		}
	
		public static object staticEvaluate(object[] Values)
		{
			object bResult;

			if (Convert.IsDBNull(Values[0]))
				if (TypeConverter.CanConvert(Values[1].GetType(), typeof(bool)))
					if (Convert.ToBoolean(Values[1]))
						bResult = true;
					else
						bResult = System.DBNull.Value;
				else
					bResult = System.DBNull.Value;
			else if (Convert.IsDBNull(Values[1]))
				if (TypeConverter.CanConvert(Values[0].GetType(), typeof(bool)))
					if (Convert.ToBoolean(Values[0]))
						bResult = true;
					else
						bResult = System.DBNull.Value;
				else
					bResult = System.DBNull.Value;
			else
				bResult = Convert.ToBoolean(Values[0]) || Convert.ToBoolean(Values[1]);

			return bResult;
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

	}

	/// <summary>
	/// OrBasic operator.
	/// </summary>
	public class OrBasicOperator : OrOperator
	{
		public OrBasicOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.orBasicOperator; }
		public override string Name { get { return "OR"; } }
	}

	/// <summary>
	/// Not operator.
	/// </summary>
	public class NotOperator : Operator
	{
		public NotOperator(){}
		internal override OperatorType GetOperatorType() { return OperatorType.notOperator; }
		public override string Name { get { return "!"; } }
		public override byte OperandsSupported { get { return 1; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (!Types[0].Equals(typeof(Boolean)))
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{
			bool bValue = Convert.ToBoolean(Values[0]);
			return ( !bValue );
		}
	}

	/// <summary>
	/// NotBasic operator.
	/// </summary>
	public class NotBasicOperator : NotOperator
	{
		public NotBasicOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.notBasicOperator; }
		public override string Name { get { return "NOT"; } }
	}


	// === Bitwise Operators ===

	/// <summary>
	/// BitwiseAnd operator. ( + String Concatenation operator)
	/// </summary>
	public class BitwiseAndOperator : Operator
	{
		public BitwiseAndOperator(){}
		internal override OperatorType GetOperatorType() { return OperatorType.bitwiseAndOperator; }
		public override string Name { get { return "&"; } }

		public override Type GetReturnType( Type[] Types )
		{ 
			return typeof( Int32 );
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			if ( !TypeConverter.CanConvert(Types[ 0 ], typeof( Double ) ) )
			{
				InvalidArgument = 0;		
				return false;
			}
			
			if ( !TypeConverter.CanConvert(Types[ 1 ], typeof( Double ) ) )
			{
				InvalidArgument = 1;		
				return false;
			}

			return true;
		}

		internal override object Evaluate( object[] Values )
		{
			return Convert.ToInt32( Values[0] ) & Convert.ToInt32( Values[1] );
		}
	}
	
	/// <summary>
	/// BitwiseInclusiveOr operator.
	/// </summary>
	public class BitwiseInclusiveOrOperator : Operator
	{
		public BitwiseInclusiveOrOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.bitwiseInclusiveOrOperator; }
		public override string Name { get { return "|"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{
			long lValue0 = Convert.ToInt32(Values[0]);
			long lValue1 = Convert.ToInt32(Values[1]);

			return (lValue0 | lValue1);
		}
	}

	/// <summary>
	/// BitwiseExclusiveOr operator.
	/// </summary>
	public class BitwiseExclusiveOrOperator : Operator
	{
		public BitwiseExclusiveOrOperator() {}
		internal override OperatorType GetOperatorType() { return OperatorType.bitwiseExclusiveOrOperator; }
		public override string Name { get { return "!&"; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				if (!TypeConverter.CanConvert(Types[1], typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 1;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{
			long lValue0 = Convert.ToInt32(Values[0]);
			long lValue1 = Convert.ToInt32(Values[1]);

			return (lValue0 ^ lValue1);
		}
	}

	/// <summary>
	/// BitwiseComplement operator.
	/// </summary>
	public class BitwiseComplement : Operator
	{
		public BitwiseComplement() {}
		internal override OperatorType GetOperatorType() { return OperatorType.bitwiseComplement; }
		public override string Name { get { return "~"; } }
		public override byte OperandsSupported { get { return 1; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			bool bIsValid = true;

			if (!TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		internal override object Evaluate(object[] Values)
		{
			long lValue = Convert.ToInt32(Values[0]);

			return (~lValue);
		}

	}

}
