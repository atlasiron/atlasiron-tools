#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;

using USP.Express.Pro.Exceptions;
using USP.Express.Pro.Constants;

namespace USP.Express.Pro.Functions
{
	// -- Built-in Functions: --

	/// <summary>
	/// Abs function.
	/// </summary>
	public class AbsFunction : Function
	{
		public AbsFunction(){}
		public override string Name { get { return "ABS"; } }
		
		public override string Syntax { get { return "ABS(number)"; } }
		public override string Description { get { return "Returns the absolute value of a number"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate( object[] Values )
		{
			double dValue = Convert.ToDouble( Values[0] );
			return Math.Abs( dValue );
		}
	}

	/// <summary>
	/// ArcCos function.
	/// </summary>
	public class ArcCosFunction: Function
	{
		public ArcCosFunction() {}
		public override string Name { get { return "ACOS"; } }

		public override string Syntax { get { return "ACOS(number)"; } }
		public override string Description { get { return "Returns the arccosine of a number, in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if ( dValue > (double) 1 || dValue < (double) (-1) )
				throw (new ACosOutOfBoundsException());

			return Math.Acos(dValue);
		}
	}

	/// <summary>
	/// ArcCot function.
	/// </summary>
	public class ArcCotFunction: Function
	{
		public ArcCotFunction() {}
		public override string Name { get { return "ACOT"; } }

		public override string Syntax { get { return "ACOT(number)"; } }
		public override string Description { get { return "Returns the arccotangent of a number in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if ( dValue.Equals((double) 0) )
				throw (new ACotOfZeroException());

			return Math.Atan((double) 1 / dValue);
		}
	}

	/// <summary>
	/// ArcCsc function.
	/// </summary>
	public class ArcCscFunction: Function
	{
		public ArcCscFunction() {}
		public override string Name { get { return "ACSC"; } }

		public override string Syntax { get { return "ACSC(number)"; } }
		public override string Description { get { return "Returns the arccosecant of a number in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if ( dValue < (double) 1 && dValue > (double) (-1) )
				throw (new ACscOutOfBoundsException());

			return Math.Asin(1 / dValue);
		}
	}

	/// <summary>
	/// ArcSec function.
	/// </summary>
	public class ArcSecFunction: Function
	{
		public ArcSecFunction() {}
		public override string Name { get { return "ASEC"; } }

		public override string Syntax { get { return "ASEC(number)"; } }
		public override string Description { get { return "Returns the arcsecant of a number in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( dValue < (double) 1 && dValue > (double) (-1) )
				throw (new ASecOutOfBoundsException());

			return Math.Acos((double) 1 / dValue);
		}
	}

	/// <summary>
	/// ArcSin function.
	/// </summary>
	public class ArcSinFunction: Function
	{
		public ArcSinFunction() {}
		public override string Name { get { return "ASIN"; } }

		public override string Syntax { get { return "ASIN(number)"; } }
		public override string Description { get { return "Returns the arcsine of a number in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( dValue > (double) 1 || dValue < (double) -1 )
				throw (new ASinOutOfBoundsException());

			return Math.Asin(dValue);
		}
	}

	/// <summary>
	/// ArcTan function.
	/// </summary>
	public class ArcTanFunction: Function
	{
		public ArcTanFunction() {}
		public override string Name { get { return "ATAN"; } }

		public override string Syntax { get { return "ATAN(number)"; } }
		public override string Description { get { return "Returns the arctangent of a number in radians."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Atan(dValue);
		}
	}

	/// <summary>
	/// Ceiling function.
	/// </summary>
	public class CeilingFunction: Function
	{
		public CeilingFunction() {}
		public override string Name { get { return "CEIL"; } }

		public override string Syntax { get { return "CEIL(number)"; } }
		public override string Description { get { return "Returns the smallest integer greater than or equal to the specified number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Ceiling(dValue);
		}
	}

	/// <summary>
	/// Cos function.
	/// </summary>
	public class CosFunction: Function
	{
		public CosFunction() {}
		public override string Name { get { return "COS"; } }

		public override string Syntax { get { return "COS(angle)"; } }
		public override string Description { get { return "Returns the cosine of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Cos(dValue);
		}
	}

	/// <summary>
	/// HyperbolicCos function.
	/// </summary>
	public class HCosFunction: Function
	{
		public HCosFunction() {}
		public override string Name { get { return "COSH"; } }

		public override string Syntax { get { return "COSH(angle)"; } }
		public override string Description { get { return "Returns the hyperbolic cosine of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Cosh(dValue);
		}
	}

	/// <summary>
	/// Cot function.
	/// </summary>
	public class CotFunction: Function
	{
		public CotFunction() {}
		public override string Name { get { return "COT"; } }

		public override string Syntax { get { return "COT(angle)"; } }
		public override string Description { get { return "Returns the cotangent of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Sin(dValue).Equals((double) 0) )
				throw (new InfiniteCotException());

			return ( (double) 1 / Math.Tan(dValue) );
		}
	}

	/// <summary>
	/// HyperbolicCot function.
	/// </summary>
	public class HCotFunction: Function
	{
		public HCotFunction() {}
		public override string Name { get { return "COTH"; } }

		public override string Syntax { get { return "COTH(angle)"; } }
		public override string Description { get { return "Returns the hyperbolic cotangent of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Tanh(dValue).Equals((double) 0))
				throw (new InfiniteHCotException());

			return (double)1 / Math.Tanh(dValue);
		}
	}

	/// <summary>
	/// Csc function.
	/// </summary>
	public class CscFunction: Function
	{
		public CscFunction() {}
		public override string Name { get { return "CSC"; } }

		public override string Syntax { get { return "CSC(angle)"; } }
		public override string Description { get { return "Returns the cosecant of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Sin(dValue).Equals((double) 0) )
				throw (new InfiniteCscException());

			return (double)1 / Math.Sin(dValue);
		}
	}

	/// <summary>
	/// HyperbolicCsc function.
	/// </summary>
	public class HCscFunction: Function
	{
		public HCscFunction() {}
		public override string Name { get { return "CSCH"; } }

		public override string Syntax { get { return "CSCH(angle)"; } }		
		public override string Description { get { return "Returns the hyperbolic cosecant of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Sinh(dValue).Equals((double) 0) )
				throw (new InfiniteHCscException());

			return (double)1 / Math.Sinh(dValue);
		}
	}

	/// <summary>
	/// Exp function.
	/// </summary>
	public class ExpFunction: Function
	{
		public ExpFunction() {}
		public override string Name { get { return "EXP"; } }

		public override string Syntax { get { return "EXP(number)"; } }
		public override string Description { get { return "Returns E (the base of natural logarithms) raised to the specified power."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Exp(dValue);
		}
	}

	/// <summary>
	/// Floor function.
	/// </summary>
	public class FloorFunction: Function
	{
		public FloorFunction() {}
		public override string Name { get { return "FLOOR"; } }

		public override string Syntax { get { return "FLOOR(number)"; } }
		public override string Description { get { return "Returns the greatest integer less than or equal to the specified number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Floor(dValue);
		}
	}

	/// <summary>
	/// Int function. ( = Floor function)
	/// </summary>
	public class IntFunction: FloorFunction
	{
		public IntFunction() {}
		public override string Name { get { return "INT"; } }

		public override string Syntax { get { return "INT(number)"; } }
	}

	/// <summary>
	/// Ln function.
	/// </summary>
	public class LnFunction: Function
	{
		public LnFunction() {}
		public override string Name { get { return "LN"; } }

		public override string Syntax { get { return "LN(number)"; } }
		public override string Description { get { return "Returns the natural logarithm of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( dValue < (double) 0 )	
				throw (new LnNegNumberException());

			if( dValue.Equals((double) 0) )	
				throw (new LnZeroException());

			return Math.Log(dValue);
		}
	}

	/// <summary>
	/// Log function.
	/// </summary>
	public class LogFunction: Function
	{
		public LogFunction() {}
		public override string Name { get { return "LOG"; } }

		public override string Syntax { get { return "LOG(number, base)"; } }
		public override string Description { get { return "Returns the logarithm of a number to the base you specify."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 2);
		}

		public override object Evaluate(object[] Values)
		{
			//Values[0] -- number
			double dNumber = Convert.ToDouble(Values[0]);
			//Values[1] -- base
			double dBase = Convert.ToDouble(Values[1]);

			if( dNumber < (double) 0 )	
				throw (new LogNegNumberException());
			if( dNumber.Equals((double) 0) )	
				throw (new LogZeroException());
			if( dBase < (double) 0 )	
				throw (new LogNegBaseException());
			if( dBase.Equals((double) 0) )	
				throw (new LogZeroBaseException());
			if( dBase.Equals((double) 1) )	
				throw (new LogBaseEq1Exception());

			return (Math.Log10( dNumber ) / Math.Log10( dBase ));
		}
	}

	/// <summary>
	/// Log10 function.
	/// </summary>
	public class Log10Function: Function
	{
		public Log10Function() {}
		public override string Name { get { return "LOG10"; } }

		public override string Syntax { get { return "LOG10(number)"; } }
		public override string Description { get { return "Returns the base-10 logarithm of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( dValue < (double) 0 )	
				throw (new Log10NegNumberException());

			if( dValue.Equals((double) 0) )	
				throw (new Log10ZeroException());

			return Math.Log10(dValue);
		}
	}

	/// <summary>
	/// Neg function.
	/// </summary>
	public class NegFunction: Function
	{
		public NegFunction() {}
		public override string Name { get { return "NEG"; } }

		public override string Syntax { get { return "NEG(number)"; } }
		public override string Description { get { return "Produces the negative of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return -1 * dValue;
		}
	}

	/// <summary>
	/// Power function.
	/// </summary>
	public class PowerFunction: Function
	{
		public PowerFunction() {}
		public override string Name { get { return "POWER"; } }

		public override string Syntax { get { return "POWER(number, power)"; } }
		public override string Description { get { return "Returns the result of a number raised to a power"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 2);
		}

		public override object Evaluate(object[] Values)
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

	/// <summary>
	/// Root function.
	/// </summary>
	public class RootFunction : Function
	{
		public RootFunction() {}
		public override string Name { get { return "ROOT"; } }

		public override string Syntax { get { return "ROOT(number, root)"; } }		
		public override string Description { get { return "Returns a root of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 2);
		}

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			int nRoot = Convert.ToInt32(Values[1]);
			
			if ((double) nRoot != Convert.ToDouble(Values[1])) // SVA: has to check if it is integer in run-time, see InputTypeSupported
				throw (new InvalidArgumentException());

			// only odd roots of negative numbers are allowed
			if( dValue < (double) 0 && nRoot % 2 == 0 )	
				throw (new SqrtNegNumberException());

			return Math.Sign(dValue) * Math.Pow(Math.Abs(dValue), (double) 1 / nRoot);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					bSupported = true;
					break;
				case 1:
					bSupported = TypeConverter.CanConvert(Type, typeof(Double)); //Int32?
					break;
			}
			return bSupported;
		}
	}

	/// <summary>
	/// Random function.
	/// </summary>
	public class RandomFunction: Function
	{
		private Random m_oRandom;
		public RandomFunction()
		{
			m_oRandom = new Random();
		}
		public override string Name { get { return "RAND"; } }

		public override string Syntax { get { return "RAND([max])"; } }
		public override string Description { get { return "Returns an evenly distributed random number between 0 and max. If max is undefined then RAND() returns random number between 0 and 1."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 0 || nCount == 1);
		}

		public override object Evaluate(object[] Values)
		{
			double dValue = 1;

			if (Values.Length > 0)
				dValue = Convert.ToDouble(Values[0]);

			return dValue * m_oRandom.NextDouble();
			
		}
	}

	/// <summary>
	/// Sec function.
	/// </summary>
	public class SecFunction: Function
	{
		public SecFunction() {}
		public override string Name { get { return "SEC"; } }

		public override string Syntax { get { return "SEC(angle)"; } }
		public override string Description { get { return "Returns the secant of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Cos(dValue).Equals((double) 0) )
				throw (new InfiniteSecException());

			return (double)1 / Math.Cos( dValue );
		}
	}

	/// <summary>
	/// HyperbolicSec function.
	/// </summary>
	public class HSecFunction: Function
	{
		public HSecFunction() {}
		public override string Name { get { return "SECH"; } }

		public override string Syntax { get { return "SECH(angle)"; } }
		public override string Description { get { return "Returns the hyperbolic secant of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Cosh(dValue).Equals((double) 0) )
				throw (new InfiniteHSecException());

			return (double)1 / Math.Cosh( dValue );
		}
	}

	/// <summary>
	/// Sin function.
	/// </summary>
	public class SinFunction : Function
	{
		public SinFunction() {}
		public override string Name { get { return "SIN"; } }

		public override string Syntax { get { return "SIN(angle)"; } }
		public override string Description { get { return "Returns the sine of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Sin(dValue);
		}
	}

	/// <summary>
	/// HyperbolicSin function.
	/// </summary>
	public class HSinFunction : Function
	{
		public HSinFunction() {}
		public override string Name { get { return "SINH"; } }

		public override string Syntax { get { return "SINH(angle)"; } }
		public override string Description { get { return "Returns the hyperbolic sine of an angle."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);
			return Math.Sinh(dValue);
		}
	}

	/// <summary>
	/// Sqrt function.
	/// </summary>
	public class SqrtFunction : Function
	{
		public SqrtFunction() {}
		public override string Name { get { return "SQRT"; } }

		public override string Syntax { get { return "SQRT(number)"; } }		
		public override string Description { get { return "Returns a square root of a number."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( dValue < (double) 0 )	
				throw (new SqrtNegNumberException());

			return Math.Sqrt(dValue);
		}
	}


	/// <summary>
	/// Sum function.
	/// </summary>
	/// <remarks>
	/// DBNull parameters are ignored.
	/// If all input parameters are of type DBNull, the result is DBNull.		
	/// </remarks>
	public class SumFunction : Function
	{
		public SumFunction() {}
		public override string Name { get { return "SUM"; } }

		public override string Syntax { get { return "SUM(number1, number2, ...)"; } }
		public override string Description { get { return "Returns the sum of the specified numbers"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Double)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// number array argument
				if (!TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 0;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values)
		{
			double dSum = 0;
			int i;					
			object oValue;
			bool bAreNull; // are all params Nulls?

			for(i = 0, bAreNull = true; i <= Values.GetUpperBound(0); i++)
			{
				oValue = Values.GetValue(i);
				if (!Convert.IsDBNull(oValue))
				{
					bAreNull = false;
					dSum += Convert.ToDouble(oValue);
				}
			}
			
			return bAreNull ? (object) System.DBNull.Value : dSum;			
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Count function.
	/// </summary>
	/// <remarks>
	/// DBNull parameters participate in total count.	
	/// </remarks>
	public class CountFunction : Function
	{
		public CountFunction() {}
		public override string Name { get { return "COUNT"; } }

		public override string Syntax { get { return "COUNT(number1, number2, ...)"; } }
		public override string Description { get { return "Returns total count of input parameters passed"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Double)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// number array argument
				if (!TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 0;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values)
		{
			return Values.Length;
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Average function.
	/// </summary>
	public class AverageFunction : Function
	{
		public AverageFunction() {}
		public override string Name { get { return "AVERAGE"; } }

		public override string Syntax { get { return "AVERAGE(number)"; } }
		public override string Description { get { return "Returns the average (arithmetic mean) of its arguments."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Double)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// number array argument
				if (!TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double)))
				{
					bIsValid = false;
					InvalidArgument = 0;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] Values)
		{
			double dAvg = 0;
			int i;

			for(i = 0; i <= Values.GetUpperBound(0); i++)
				dAvg = (dAvg * i + Convert.ToDouble(Values.GetValue(i))) / (i + 1);
			
			return dAvg;
		}

		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Tan function.
	/// </summary>
	public class TanFunction : Function
	{
		public TanFunction() {}
		public override string Name { get { return "TAN"; } }

		public override string Syntax { get { return "TAN(angle)"; } }
		public override string Description { get { return "Returns the tangent of an angle"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Cos(dValue).Equals((double) 0) )
				throw (new InfiniteTanException());

			return Math.Tan(dValue);
		}
	}

	/// <summary>
	/// HyperbolicTan function.
	/// </summary>
	public class HTanFunction : Function
	{
		public HTanFunction() {}
		public override string Name { get { return "TANH"; } }

		public override string Syntax { get { return "TANH(angle)"; } }		
		public override string Description { get { return "Returns the hyperbolic tangent of an angle"; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override object Evaluate(object[] Values)
		{
			double dValue = Convert.ToDouble(Values[0]);

			if( Math.Cosh(dValue).Equals((double) 0) )
				throw (new InfiniteHTanException());

			return Math.Tanh(dValue);
		}
	}

	/// <summary>
	/// Min function.
	/// </summary>
	/// <remarks>
	/// DBNull parameters are ignored.
	/// If all input parameters are of type DBNull, the result is DBNull.		
	/// </remarks>
	public class MinFunction : Function
	{
		public MinFunction() {}
		public override string Name { get { return "MIN"; } }

		public override string Syntax { get { return "MIN(number1, number2, ...)"; } }
		public override string Description { get { return "Returns the smallest number among its arguments."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)	// array param
			{
				// number array
				if (TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double)))
					return typeof(Double);
				else // date array
					return typeof(DateTime);
			}
			else if (TypeConverter.CanConvert(Types[0], typeof(Double)))	// number params
				return typeof(Double);
			else	// if (Types[0].Equals(typeof(DateTime))) // date params
				return typeof(DateTime);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Double)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!Types[i].Equals(typeof(DateTime)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// array argument
				if (!(TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double))
					|| Types[0].GetElementType().Equals(typeof(DateTime))))
					{
						bIsValid = false;
						InvalidArgument = 0;
					}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] aValues)
		{
			int i;
			bool bAreNull; // if all params are null
			object oValue;
			Type oType = aValues.GetValue(0).GetType(); //array type

			// the array may start with NULL
			if (oType.Equals(typeof(System.DBNull)))
			{				
				for (i = 1; i <= aValues.GetUpperBound(0); i++)
				{
					if (!Convert.IsDBNull(aValues.GetValue(i)))
					{
						oType = aValues.GetValue(i).GetType();
						break;
					}
				}
			}

			// number array
			if (TypeConverter.CanConvert(oType, typeof(Double)))
			{
				double dMin = double.MaxValue;

				for (i = 0, bAreNull = true; i <= aValues.GetUpperBound(0); i++)
				{
					oValue = aValues.GetValue(i);

					if (!Convert.IsDBNull(oValue))
					{
						bAreNull = false;
						if (Convert.ToDouble(oValue) < dMin)
							dMin = Convert.ToDouble(oValue);
					}
				}

				return bAreNull ? (object) System.DBNull.Value : dMin;
			}
			else // date array
			{
				DateTime dtMin = DateTime.MaxValue;
				for (i = 0, bAreNull = true; i <= aValues.GetUpperBound(0); i++)
				{
					oValue = aValues.GetValue(i);
					if (!Convert.IsDBNull(oValue))
					{
						bAreNull = false;
						if (DateTime.Compare(Convert.ToDateTime(oValue), dtMin) < 0)
							dtMin = Convert.ToDateTime(oValue);
					}
				}
				return bAreNull ? (object) System.DBNull.Value : dtMin;
			}
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Max function.
	/// </summary>
	/// <remarks>
	/// DBNull parameters are ignored.
	/// If all input parameters are of type DBNull, the result is DBNull.		
	/// </remarks>
	public class MaxFunction : Function
	{
		public MaxFunction() {}
		public override string Name { get { return "MAX"; } }

		public override string Syntax { get { return "MAX(number1, number2, ...)"; } }
		public override string Description { get { return "Returns the largest value among its arguments."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)	// array param
			{
				// number array
				if (TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double)))
					return typeof(Double);
				else // date array
					return typeof(DateTime);
			}
			else if (TypeConverter.CanConvert(Types[0], typeof(Double)))	// number params
				return typeof(Double);
			else	// if (Types[0].Equals(typeof(DateTime))) // date params
				return typeof(DateTime);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Double)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Double)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types[0].Equals(typeof(DateTime)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!Types[i].Equals(typeof(DateTime)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// array argument
				if (!(TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Double))
					|| Types[0].GetElementType().Equals(typeof(DateTime))))
				{
					bIsValid = false;
					InvalidArgument = 0;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}

		public override object Evaluate(object[] aValues)
		{
			int i;
			bool bAreNull; // if all params are null
			object oValue;
			Type oType = aValues.GetValue(0).GetType(); //array type

			// the array may start with NULL
			if (oType.Equals(typeof(System.DBNull)))
			{				
				for (i = 1; i <= aValues.GetUpperBound(0); i++)
				{
					if (!Convert.IsDBNull(aValues.GetValue(i)))
					{
						oType = aValues.GetValue(i).GetType();
						break;
					}
				}
			}

			// numbers
			if (TypeConverter.CanConvert(oType, typeof(Double)))
			{
				double dMax = double.MinValue; 

				for (i = 0, bAreNull = true; i <= aValues.GetUpperBound(0); i++)
				{
					oValue = aValues.GetValue(i);

					if (!Convert.IsDBNull(oValue))
					{
						bAreNull = false;
						if (Convert.ToDouble(oValue) > dMax)
							dMax = Convert.ToDouble(oValue);
					}
				}
				return bAreNull ? (object) System.DBNull.Value : dMax;
			}
			else // dates
			{
				DateTime dtMax = DateTime.MinValue;
				for (i = 0, bAreNull = true; i <= aValues.GetUpperBound(0); i++)
				{
					oValue = aValues.GetValue(i);
					if (!Convert.IsDBNull(oValue))
					{
						bAreNull = false;
						if (DateTime.Compare(Convert.ToDateTime(oValue), dtMax) > 0)
							dtMax = Convert.ToDateTime(oValue);
					}
				}
				return bAreNull ? (object) System.DBNull.Value : dtMax;
			}
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}

		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Round function.
	/// </summary>
	public class RoundFunction : Function
	{
		public RoundFunction() {}
		public override string Name { get { return "ROUND"; } }

		public override string Syntax { get { return "ROUND(number, [decimals])"; } }
		public override string Description { get { return "Returns a number with the specified precision nearest the specified value."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 1 || nCount == 2);
		}

		public override object Evaluate(object[] Values)
		{
			double dValue;
			if (Values.Length > 1)
				dValue = Math.Round(Convert.ToDouble(Values[0]), Convert.ToInt32(Values[1])); 
			else
				dValue = Math.Round(Convert.ToDouble(Values[0])); 
			return dValue;
		}
	}


	/// <summary>
	/// IIf function.
	/// </summary>
	public class IIFFunction: Function
	{
		public IIFFunction() {}
		public override string Name { get { return "IIF"; } }

		public override string Syntax { get { return "IIF(boolean_condition, expression1, expression2)"; } }
		public override string Description 
		{
			get { return "If condition evaluates to TRUE, expression1 is returned. If condition evaluates to FALSE, expression2 is returned.\nThis function accepts any type for its second and third parameters as long as they are both of the same type."; } 
		}
		public override GroupType Group { get { return GroupType.Logical; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3);
		}

		public override Type GetReturnType(Type[] Types)
		{
			return Types[1];
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					bSupported = Type.Equals(typeof(Boolean));
					break;
				case 1:
				case 2:
					bSupported = true;
					break;
			}
			return bSupported;
		}
		
		public override bool Validate(Type[] Types, ref int InvalidArgument) 
		{
			if (base.Validate(Types, ref InvalidArgument))
			{
				if (TypeConverter.CanConvert(Types[1], Types[2]) || TypeConverter.CanConvert(Types[2], Types[1])) //SVA.#3231
					return true;
				else
				{
					InvalidArgument = 2;
					return false;
				}
			}
			else
				return false;
		}

		public override object Evaluate(object[] Values)
		{
			bool bValue = Convert.ToBoolean(Values[0]);
			return ( bValue ? Values[1] : Values[2]);
		}

		public override bool IsNullable(object[] Values)
		{
			if (Convert.IsDBNull(Values[0])) // if the very first boolean parameter of IIF is DBNULL, the result is undefined, i.e. DBNULL
				return true;
			else
				return false;
		}
	}

	/// <summary>
	/// If function. ( = IIf function)
	/// </summary>
	public class IFFunction: IIFFunction
	{ 
		public IFFunction() {}
		public override string Name { get { return "IF"; } }

		public override string Syntax { get { return "IF(boolean_condition, expression1, expression2)"; } }

	}

	/// <summary>
	/// And function.
	/// </summary>
	public class AndFunction: Function
	{
		public AndFunction() {}
		public override string Name { get { return "AND"; } }

		public override string Syntax { get { return "AND(boolean, boolean, ...)"; } }
		public override string Description { get { return "Returns true if all its arguments are true; returns false if any argument is false"; } }
		public override GroupType Group { get { return GroupType.Logical; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount > 0);
		}

		public override Type GetReturnType(Type[] Types)
		{
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (TypeConverter.CanConvert(Types[0], typeof(Boolean)))
			{
				for (i=1; i<=Types.GetUpperBound(0); i++)
					if (!TypeConverter.CanConvert(Types[i], typeof(Boolean)))
					{
						bIsValid = false;
						InvalidArgument = i;
						break;
					}
			}
			else if (Types.GetUpperBound(0) == 0 && Types[0].IsArray)
			{
				// array argument
				if (!TypeConverter.CanConvert(Types[0].GetElementType(), typeof(Boolean)))
				{
					bIsValid = false;
					InvalidArgument = 0;
				}
			}
			else
			{
				bIsValid = false;
				InvalidArgument = 0;
			}

			return bIsValid;
		}		

		public override object Evaluate(object[] Values)
		{
			object oResult = Values[0];
			int i;

			for(i = 1; i <= Values.GetUpperBound(0); i++)
				oResult = Operators.AndOperator.staticEvaluate(new object[]{oResult, Values[i]});
				//bResult = bResult && Convert.ToBoolean(Values.GetValue(i));
			
			return oResult ;
		}

		public override bool IsNullable(object[] Values)
		{
			return false;
		}
	
		public override bool UnfoldArray
		{
			get {return true;}
		}
	}

	/// <summary>
	/// Or function.
	/// </summary>
	public class OrFunction: AndFunction
	{
		public OrFunction() {}
		public override string Name { get { return "OR"; } }

		public override string Syntax { get { return "OR(boolean, boolean, ...)"; } }
		public override string Description { get { return "Returns true if any argument is true; returns false if all arguments are false."; } }
		public override GroupType Group { get { return GroupType.Logical; } }

		public override object Evaluate(object[] Values)
		{
			object oResult = Values[0];
			int i;

			for(i = 1; i <= Values.GetUpperBound(0); i++)
				oResult = Operators.OrOperator.staticEvaluate(new object[]{oResult, Values[i]});
				//bResult = bResult || Convert.ToBoolean(aValues.GetValue(i));
			
			return oResult ;
		}
	}


	// === Date Functions ===


	/// <summary>
	/// Now function.
	/// </summary>
	public class NowFunction: Function
	{
		public NowFunction() {}
		public override string Name { get { return "NOW"; } }

		public override string Syntax { get { return "NOW()"; } }
		public override string Description 
		{
			get { return "Returns current date and time according to the setting of your computer's system date and time."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 0);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(DateTime);
		}

		public override object Evaluate(object[] Values) 
		{
			return DateTime.Now;
		}
	}

	/// <summary>
	/// Today function.
	/// </summary>
	public class TodayFunction: Function
	{
		public TodayFunction() {}
		public override string Name { get { return "TODAY"; } }

		public override string Syntax { get { return "TODAY()"; } }
		public override string Description 
		{
			get { return "Returns current date. Time part of the day is zero (midnight)."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 0);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(DateTime);
		}

		public override object Evaluate(object[] Values) 
		{
			#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
			#else
				return DateTime.Today;
			#endif
		}
	}

	/// <summary>
	/// Year function.
	/// </summary>
	public class YearFunction : Function
	{
		public YearFunction() {}
		public override string Name { get { return "YEAR"; } }

		public override string Syntax { get { return "YEAR(date)"; } }
		public override string Description 
		{
			get { return "Returns the year corresponding to the specified date."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Year;
#endif
		}
	}

	/// <summary>
	/// Month function.
	/// </summary>
	public class MonthFunction : Function
	{
		public MonthFunction() {}
		public override string Name { get { return "GETMONTH"; } }

		public override string Syntax { get { return "GETMONTH(date)"; } }
		public override string Description 
		{
			get { return "Returns a number between 1 and 12, inclusive, representing the month of the year, corresponding to the specified date."; }
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Month;
#endif
		}
	}

	/// <summary>
	/// Day function.
	/// </summary>
	public class DayFunction : Function
	{
		public DayFunction() {}
		public override string Name { get { return "DAY"; } }

		public override string Syntax { get { return "DAY(date)"; } }
		public override string Description 
		{
			get { return "Returns a number between 1 and 31, inclusive, representing the day of the month, corresponding to the specified date."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Day;
#endif
		}
	}

	/// <summary>
	/// Weekday function.
	/// </summary>
	public class WeekDayFunction : Function
	{
		public WeekDayFunction() {}
		public override string Name { get { return "WEEKDAY"; } }

		public override string Syntax { get { return "WEEKDAY(date)"; } }
		public override string Description 
		{
			get 
		{
			return "Returns a number representing the day of the week, corresponding to the specified date. The return value ranges from zero, indicating Sunday, to six, indicating Saturday."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.DayOfWeek;
#endif
		}
	}

	/// <summary>
	/// Hour function.
	/// </summary>
	public class HourFunction : Function
	{
		public HourFunction() {}
		public override string Name { get { return "HOUR"; } }

		public override string Syntax { get { return "HOUR(date)"; } }
		public override string Description 
		{
			get { return "Returns a number between 0 and 23, inclusive, representing the hour of the day."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Hour;
#endif
		}
	}

	/// <summary>
	/// Minute function.
	/// </summary>
	public class MinuteFunction : Function
	{
		public MinuteFunction() {}
		public override string Name { get { return "MINUTE"; } }

		public override string Syntax { get { return "MINUTE(date)"; } }
		public override string Description 
		{
			get { return "Returns a number between 0 and 59, inclusive, representing the minute of the hour."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Minute;
#endif
		}
	}

	/// <summary>
	/// Second function.
	/// </summary>
	public class SecondFunction : Function
	{
		public SecondFunction() {}
		public override string Name { get { return "SECOND"; } }

		public override string Syntax { get { return "SECOND(date)"; } }
		public override string Description 
		{
			get { return "Returns a number between 0 and 59, inclusive, representing the second of the minute."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			if (Index == 0 && Type.Equals(typeof(DateTime)))
				return true;
			else
				return false;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			DateTime dtDate = Convert.ToDateTime(Values[0]);
			return dtDate.Second;
#endif
		}
	}

	/// <summary>
	/// DateAdd function.
	/// </summary>
	public class DateAddFunction : Function
	{
		public DateAddFunction() {}
		public override string Name { get { return "DATEADD"; } }

		public override string Syntax { get { return "DATEADD(interval, number, date)"; } }
		public override string Description 
		{
			get { return "Returns a Date value to which a specified time interval has been added."; } 
		}
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3);
		}

		public override Type GetReturnType(Type[] Types)
        { 
			return typeof(DateTime);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					// DateInterval
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(USP.Express.Pro.Constants.DateInterval)) || Type.Equals(typeof(String)));
					break;
				case 1:
					bSupported = TypeConverter.CanConvert(Type, typeof(Double));
					break;
				case 2:
					bSupported = Type.Equals(typeof(DateTime));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			bool bString = Values[0].GetType().Equals(typeof(String));
			double dDateInterval = 0.0;
			string sDateInterval = "";

			if (Values[0].GetType().Equals(typeof(DateInterval)))
			{
				dDateInterval = Convert.ToDouble(Values[0]);
			}
			else if (!bString)
			{
				// check DateInterval argument
				dDateInterval = Convert.ToDouble(Convert.ToInt32(Values[0]));
				if (!dDateInterval.Equals(Convert.ToDouble(Values[0])) || !Enum.IsDefined(typeof(DateInterval), (DateInterval) dDateInterval))
					throw new InvalidArgumentException();
			}
			else
				sDateInterval = Convert.ToString(Values[0]);

			double dNumber = Convert.ToDouble(Values[1]);
			DateTime dtDate = Convert.ToDateTime(Values[2]);

			try
			{
				if (bString)
					dtDate = Microsoft.VisualBasic.DateAndTime.DateAdd(sDateInterval, dNumber, dtDate);
				else
					dtDate = Microsoft.VisualBasic.DateAndTime.DateAdd((Microsoft.VisualBasic.DateInterval) dDateInterval, dNumber, dtDate);
			}
			catch (System.ArgumentException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}
			
			return dtDate;
#endif
		}
	}

	/// <summary>
	/// DateDiff function.
	/// </summary>
	public class DateDiffFunction : Function
	{
		public DateDiffFunction() {}
		public override string Name { get { return "DATEDIFF"; } }
		
		public override string Syntax { get { return "DATEDIFF(interval, date1, date2 [, firstdayofweek [,firstweekofyear]])"; } }
		public override string Description { get { return "Returns the number of intervals between two dates."; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount >= 3 && nCount <= 5);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					// DateInterval
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(USP.Express.Pro.Constants.DateInterval)) || Type.Equals(typeof(String)));
					break;
				case 1:
				case 2:
					bSupported = Type.Equals(typeof(DateTime));
					break;
				case 3:
					// FirstDayOfWeek
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(FirstDayOfWeek)));
					break;
				case 4:
					// FirstWeekOfYear
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(FirstWeekOfYear)));
					break;
			}
			return bSupported;
		}
		
		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		public override object Evaluate(object[] Values) 
		{
			bool bString = Values[0].GetType().Equals(typeof(String));
			double dDateInterval = 0.0;
			string sDateInterval = "";

			if (Values[0].GetType().Equals(typeof(DateInterval)))
			{
				dDateInterval = Convert.ToDouble(Values[0]);
			}
			else if (!bString)
			{
				// check DateInterval argument
				dDateInterval = Convert.ToDouble(Convert.ToInt32(Values[0]));
				if (!dDateInterval.Equals(Convert.ToDouble(Values[0])) || !Enum.IsDefined(typeof(DateInterval), (DateInterval) dDateInterval))
					throw new InvalidArgumentException();
			}
			else
				sDateInterval = Convert.ToString(Values[0]);

			double dFirstDayOfWeek;
			Microsoft.VisualBasic.FirstDayOfWeek nFirstDayOfWeek;
			if (Values.GetUpperBound(0) >= 3)
			{
				if (Values[3].GetType().Equals(typeof(FirstDayOfWeek)))
				{
					dFirstDayOfWeek = Convert.ToDouble(Values[3]);
				}
				else
				{
					// check FirstDayOfWeek argument
					dFirstDayOfWeek = Convert.ToDouble(Convert.ToInt32(Values[3]));
					if (!dFirstDayOfWeek.Equals(Convert.ToDouble(Values[3])) || !Enum.IsDefined(typeof(FirstDayOfWeek), (FirstDayOfWeek) dFirstDayOfWeek))
						throw new InvalidArgumentException();
				}
				nFirstDayOfWeek = (Microsoft.VisualBasic.FirstDayOfWeek) dFirstDayOfWeek;
			}
			else
				nFirstDayOfWeek = Microsoft.VisualBasic.FirstDayOfWeek.Sunday;

			double dFirstWeekOfYear;
			Microsoft.VisualBasic.FirstWeekOfYear nFirstWeekOfYear;
			if (Values.GetUpperBound(0) == 4)
			{
				if (Values[4].GetType().Equals(typeof(FirstWeekOfYear)))
				{
					dFirstWeekOfYear = Convert.ToDouble(Values[4]);
				}
				else
				{
					// check FirstWeekOfYear argument
					dFirstWeekOfYear = Convert.ToDouble(Convert.ToInt32(Values[4]));
					if (!dFirstWeekOfYear.Equals(Convert.ToDouble(Values[4])) || !Enum.IsDefined(typeof(FirstWeekOfYear), (FirstWeekOfYear) dFirstWeekOfYear))
						throw new InvalidArgumentException();
				}
				nFirstWeekOfYear = (Microsoft.VisualBasic.FirstWeekOfYear) dFirstWeekOfYear;
			}
			else
				nFirstWeekOfYear = Microsoft.VisualBasic.FirstWeekOfYear.Jan1;

			DateTime dtDate1 = Convert.ToDateTime(Values[1]);
			DateTime dtDate2 = Convert.ToDateTime(Values[2]);
			long nInt64Result;

			try
			{
				if (bString)
					nInt64Result = Microsoft.VisualBasic.DateAndTime.DateDiff(sDateInterval, dtDate1, dtDate2, nFirstDayOfWeek, nFirstWeekOfYear);
				else
					nInt64Result = Microsoft.VisualBasic.DateAndTime.DateDiff((Microsoft.VisualBasic.DateInterval) dDateInterval, dtDate1, dtDate2, nFirstDayOfWeek, nFirstWeekOfYear);
			}
			catch (System.ArgumentException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}

			int nResult;
			nResult = Convert.ToInt32(nInt64Result);

			return nResult;
		}
	}	

	/// <summary>
	/// DatePart function.
	/// </summary>
	public class DatePartFunction : Function
	{
		public DatePartFunction() {}
		public override string Name { get { return "DATEPART"; } }

		public override string Syntax { get { return "DATEPART(interval, date [, firstdayofweek [,firstweekofyear]])"; } }
		public override string Description { get { return "Returns an Integer value containing the specified component of a given Date value."; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount >= 2 && nCount <= 4);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					// DateInterval
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(DateInterval)) || Type.Equals(typeof(String)));
					break;
				case 1:
					bSupported = Type.Equals(typeof(DateTime));
					break;
				case 2:
					// FirstDayOfWeek
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(FirstDayOfWeek)));
					break;
				case 3:
					// FirstWeekOfYear
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(FirstWeekOfYear)));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			bool bString = Values[0].GetType().Equals(typeof(String));
			double dDateInterval = 0.0;
			string sDateInterval = "";

			if (Values[0].GetType().Equals(typeof(DateInterval)))
			{
				dDateInterval = Convert.ToDouble(Values[0]);
			}
			else if (!bString)
			{
				// check DateInterval argument
				dDateInterval = Convert.ToDouble(Convert.ToInt32(Values[0]));
				if (!dDateInterval.Equals(Convert.ToDouble(Values[0])) || !Enum.IsDefined(typeof(DateInterval), (DateInterval) dDateInterval))
					throw new InvalidArgumentException();
			}
			else
				sDateInterval = Convert.ToString(Values[0]);

			double dFirstDayOfWeek;
			Microsoft.VisualBasic.FirstDayOfWeek nFirstDayOfWeek;
			if (Values.GetUpperBound(0) >= 2)
			{
				if (Values[2].GetType().Equals(typeof(FirstDayOfWeek)))
				{
					dFirstDayOfWeek = Convert.ToDouble(Values[2]);
				}
				else
				{
					// check FirstDayOfWeek argument
					dFirstDayOfWeek = Convert.ToDouble(Convert.ToInt32(Values[2]));
					if (!dFirstDayOfWeek.Equals(Convert.ToDouble(Values[2])) || !Enum.IsDefined(typeof(FirstDayOfWeek), (FirstDayOfWeek) dFirstDayOfWeek))
						throw new InvalidArgumentException();
				}
				nFirstDayOfWeek = (Microsoft.VisualBasic.FirstDayOfWeek) dFirstDayOfWeek;
			}
			else
				nFirstDayOfWeek = Microsoft.VisualBasic.FirstDayOfWeek.Sunday;

			double dFirstWeekOfYear;
			Microsoft.VisualBasic.FirstWeekOfYear nFirstWeekOfYear;
			if (Values.GetUpperBound(0) == 3)
			{
				if (Values[3].GetType().Equals(typeof(FirstWeekOfYear)))
				{
					dFirstWeekOfYear = Convert.ToDouble(Values[3]);
				}
				else
				{
					// check FirstWeekOfYear argument
					dFirstWeekOfYear = Convert.ToDouble(Convert.ToInt32(Values[3]));
					if (!dFirstWeekOfYear.Equals(Convert.ToDouble(Values[3])) || !Enum.IsDefined(typeof(FirstWeekOfYear), (FirstWeekOfYear) dFirstWeekOfYear))
						throw new InvalidArgumentException();
				}
				nFirstWeekOfYear = (Microsoft.VisualBasic.FirstWeekOfYear) dFirstWeekOfYear;
			}
			else
				nFirstWeekOfYear = Microsoft.VisualBasic.FirstWeekOfYear.Jan1;

			DateTime dtDate = Convert.ToDateTime(Values[1]);
			int nResult;

			try
			{
				if (bString)
					nResult = Microsoft.VisualBasic.DateAndTime.DatePart(sDateInterval, dtDate, nFirstDayOfWeek, nFirstWeekOfYear);
				else
					nResult = Microsoft.VisualBasic.DateAndTime.DatePart((Microsoft.VisualBasic.DateInterval) dDateInterval, dtDate, nFirstDayOfWeek, nFirstWeekOfYear);
			}
			catch (System.ArgumentException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}

			return nResult;
#endif
		}
	}

	/// <summary>
	/// Date function.
	/// </summary>
	public class DateFunction : Function
	{
		public DateFunction() {}
		public override string Name { get { return "DATE"; } }

		public override string Syntax { get { return "DATE(int_year, int_month, int_day [, int_hour, int_minute, int_second])"; } }
		public override string Description { get { return "Returns a Date value representing a specified year, month, day, hour, minute, and second."; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3 || nCount == 6);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(DateTime);
		}

		public override object Evaluate(object[] Values) 
		{
#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
#else
			//return Microsoft.VisualBasic.DateAndTime.DateSerial((int) Values[0], (int) Values[1], (int) Values[2]).Ticks;
			DateTime dtDate;
			try
			{
				int nValue0 = Convert.ToInt32(Values[0]);
				int nValue1 = Convert.ToInt32(Values[1]);
				int nValue2 = Convert.ToInt32(Values[2]);
				if (Values.GetUpperBound(0) == 2)
					dtDate = new DateTime(nValue0, nValue1, nValue2);
				else
				{
					int nValue3 = Convert.ToInt32(Values[3]);
					int nValue4 = Convert.ToInt32(Values[4]);
					int nValue5 = Convert.ToInt32(Values[5]);
					dtDate = new DateTime(nValue0, nValue1, nValue2, nValue3, nValue4, nValue5);
				}
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}
			
			return dtDate;
#endif
		}
	}

	/// <summary>
	/// Format function.
	/// </summary>
	public class FormatFunction : Function
	{
		public FormatFunction() {}
		public override string Name { get { return "FORMAT"; } }

		public override string Syntax { get { return "FORMAT(number, style)"; } }
		public override string Description { get { return "Returns a string expression formatted according to instructions contained in a format string expression."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 1 || nCount == 2);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					// number or date 
					bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(DateTime)));
					break;
				case 1:
					// format string (optional)
					bSupported = Type.Equals(typeof(String));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values) 
		{
			object oExpression = Values[0];
			string sFormat = "";
			if (Values.GetUpperBound(0) == 1)
				sFormat = Convert.ToString(Values[1]);

			string sResult = Microsoft.VisualBasic.Strings.Format(oExpression, sFormat);
			
			return sResult;
		}
	}


    /// <summary>
    /// DateDiff function.
    /// </summary>
    public class StartOfWeekFunction : Function
    {
        public StartOfWeekFunction() { }
        public override string Name { get { return "STARTOFWEEK"; } }

        public override string Syntax { get { return "STARTOFWEEK(date [, firstdayofweek])"; } }
        public override string Description { get { return "Returns the date of the first day of the week."; } }
        public override GroupType Group { get { return GroupType.DateAndTime; } }

        public override bool MultArgsSupported(int nCount)
        {
            return (nCount >= 1 && nCount <= 2);
        }

        protected override bool InputTypeSupported(Type Type, int Index)
        {
            bool bSupported = false;
            switch (Index)
            {
                case 0:
                    bSupported = Type.Equals(typeof(DateTime));
                    break;
                case 1:
                    // FirstDayOfWeek
                    bSupported = (TypeConverter.CanConvert(Type, typeof(Double)) || Type.Equals(typeof(FirstDayOfWeek)));
                    break;
            }
            return bSupported;
        }

        public override Type GetReturnType(Type[] Types)
        {
            return typeof(DateTime);
        }

        public override object Evaluate(object[] Values)
        {
            double dFirstDayOfWeek;
			Microsoft.VisualBasic.FirstDayOfWeek nFirstDayOfWeek;
			if (Values.GetUpperBound(0) >= 1)
			{
				if (Values[1].GetType().Equals(typeof(FirstDayOfWeek)))
				{
					dFirstDayOfWeek = Convert.ToDouble(Values[1]);
				}
				else
				{
					// check FirstDayOfWeek argument
					dFirstDayOfWeek = Convert.ToDouble(Convert.ToInt32(Values[1]));
					if (!dFirstDayOfWeek.Equals(Convert.ToDouble(Values[1])) || !Enum.IsDefined(typeof(FirstDayOfWeek), (FirstDayOfWeek) dFirstDayOfWeek))
						throw new InvalidArgumentException();
				}
				nFirstDayOfWeek = (Microsoft.VisualBasic.FirstDayOfWeek) dFirstDayOfWeek;
			}
			else
				nFirstDayOfWeek = Microsoft.VisualBasic.FirstDayOfWeek.Sunday;

            
            DateTime dtDate = Convert.ToDateTime(Values[0]);
            try
            {
                return Microsoft.VisualBasic.DateAndTime.DateAdd("d",-Microsoft.VisualBasic.DateAndTime.Weekday(dtDate,nFirstDayOfWeek)+1,dtDate);
            }
            catch (System.ArgumentException)
            {
                throw new Exceptions.ArgumentOutOfRangeException();
            }
        }
    }	

	// === String Functions ===


	/// <summary>
	/// Len function.
	/// </summary>
	public class LenFunction : Function
	{
		public LenFunction() {}
		public override string Name { get { return "LEN"; } }

		public override string Syntax { get { return "LEN(string)"; } }
		public override string Description { get { return "Returns the number of characters in a string."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			return (Index == 0 && (Type.Equals(typeof(String)) || Type.Equals(typeof(DBNull))));
		}
		
		public override object Evaluate(object[] Values) 
		{
            if (Values[0] == DBNull.Value)
                return 0;

			string sValue = Convert.ToString(Values[0]);
			return sValue.Length;
		}
	}

	/// <summary>
	/// Left function.
	/// </summary>
	public class LeftFunction : Function
	{
		public LeftFunction() {}
		public override string Name { get { return "LEFT"; } }

		public override string Syntax { get { return "LEFT(string, nCount)"; } }
		public override string Description { get { return "Returns the first (leftmost) nCount characters from a string."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 2);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					bSupported = Type.Equals(typeof(String));
					break;
				case 1:
					bSupported = TypeConverter.CanConvert(Type, typeof(Double));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values) 
		{
			string sValue = Convert.ToString(Values[0]);
			int nLength = Convert.ToInt32(Values[1]);

			string sResult;
			try
			{
				sResult = sValue.Substring(0, nLength);
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}

			return sResult;
		}
	}

	/// <summary>
	/// Mid function.
	/// </summary>
	public class MidFunction : Function
	{
		public MidFunction() {}
		public override string Name { get { return "MID"; } }

		public override string Syntax { get { return "MID(string, nFirst, nCount)"; } }
		public override string Description { get { return "Returns a substring of length nCount characters from a string, starting at position nFirst (zero-based)."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
					bSupported = Type.Equals(typeof(String));
					break;
				case 1:
				case 2:
					bSupported = TypeConverter.CanConvert(Type, typeof(Double));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values) 
		{
			string sValue = Convert.ToString(Values[0]);
			int nStart = Convert.ToInt32(Values[1]);
			int nLength = Convert.ToInt32(Values[2]);

			string sResult;
			try
			{
				sResult = sValue.Substring(nStart, nLength);
			}
			catch (System.ArgumentOutOfRangeException)
			{
				throw new Exceptions.ArgumentOutOfRangeException();
			}

			return sResult;
		}
	}

	/// <summary>
	/// Substitute function.
	/// </summary>
	public class SubstituteFunction : Function
	{
		public SubstituteFunction() {}
		public override string Name { get { return "SUBSTITUTE"; } }

		public override string Syntax { get { return "SUBSTITUTE(text, oldText, newText)"; } }
		public override string Description { get { return "Substitutes newText for oldText in a text string."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			return Type.Equals(typeof(String));
		}
		
		public override object Evaluate(object[] Values, bool IsCaseSensitive) 
		{
			string sValue = Convert.ToString(Values[0]);
			string sOldValue = Convert.ToString(Values[1]);	// string to be replaced
			string sNewValue = Convert.ToString(Values[2]);	// string to replace all occurrences of sOldValue

			if (IsCaseSensitive)
				return sValue.Replace(sOldValue, sNewValue);
			else // if ignore case
				return System.Text.RegularExpressions.Regex.Replace(sValue, sOldValue, sNewValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		}
	}

	/// <summary>
	/// Find function.
	/// </summary>
	public class FindFunction : Function
	{
		public FindFunction() {}
		public override string Name { get { return "FIND"; } }

		public override string Syntax { get { return "FIND(textToFind, textToSearch, startIndex)"; } }
		public override string Description { get { return "Returns the zero-based index of the first occurrence of a textToFind, within a textToSearch string. The search starts at a specified character position (zero-based)."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 3);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Int32);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			bool bSupported = false;
			switch (Index)
			{
				case 0:
				case 1:
					bSupported = Type.Equals(typeof(String));
					break;
				case 2:
					bSupported = TypeConverter.CanConvert(Type, typeof(Double));
					break;
			}
			return bSupported;
		}
		
		public override object Evaluate(object[] Values, bool IsCaseSensitive) 
		{			
			#if TRIAL
				throw (new FunctionNotSupportedInTrialVersionException(0));
			#else
				int nIndex;

				string sTextToFind = Convert.ToString(Values[0]);
				string sTextToSearch = Convert.ToString(Values[1]);
				int nStart;
				// check StartIndex argument
				try
				{
					nStart = Convert.ToInt32(Values[2]);
				}
				catch
				{
					throw new InvalidArgumentException();
				}

				if (!IsCaseSensitive)
				{
					sTextToFind = sTextToFind.ToLower();
					sTextToSearch = sTextToSearch.ToLower();
				}

				try
				{
					nIndex = sTextToSearch.IndexOf(sTextToFind, nStart);
				}
				catch (System.ArgumentOutOfRangeException)
				{
					throw new Exceptions.ArgumentOutOfRangeException();
				}
				return nIndex;
			#endif
		}
	}

	/// <summary>
	/// Lower function.
	/// </summary>
	public class LowerFunction : Function
	{
		public LowerFunction() {}
		public override string Name { get { return "LOWER"; } }

		public override string Syntax { get { return "LOWER(string)"; } }
		public override string Description { get { return "Converts all uppercase letters in a text string to lowercase."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			return (Index == 0 && Type.Equals(typeof(String)));
		}
		
		public override object Evaluate(object[] Values) 
		{
			string sValue = Convert.ToString(Values[0]);
			return sValue.ToLower();
		}
	}

	/// <summary>
	/// Upper function.
	/// </summary>
	public class UpperFunction : Function
	{
		public UpperFunction() {}
		public override string Name { get { return "UPPER"; } }

		public override string Syntax { get { return "UPPER(string)"; } }
		public override string Description { get { return "Converts all lowercase letters in a text string to uppercase."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			return (Index == 0 && Type.Equals(typeof(String)));
		}
		
		public override object Evaluate(object[] Values) 
		{
			string sValue = Convert.ToString(Values[0]);
			return sValue.ToUpper();
		}
	}

	/// <summary>
	/// Trim function.
	/// </summary>
	public class TrimFunction : Function
	{
		public TrimFunction() {}
		public override string Name { get { return "TRIM"; } }

		public override string Syntax { get { return "TRIM(string)"; } }
		public override string Description { get { return "Removes all occurrences of white space characters from the beginning and end of a string."; } }
		public override GroupType Group { get { return GroupType.Text; } }

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(String);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{
			return (Index == 0 && Type.Equals(typeof(String)));
		}
		
		public override object Evaluate(object[] Values) 
		{
			string sValue = Convert.ToString(Values[0]);
			return sValue.Trim();
		}
	}

	/// <summary>
	/// In function.
	/// </summary>
	public class InFunction : Function
	{
		public InFunction() {}
		public override string Name { get { return "IN"; } }

		public override string Syntax { get { return "IN(string_ItemToFind, string_ListItem1, string_ListItem2, ...)"; } }
		public override string Description { get { return "Looks for ItemToFind in the specified list of items. Returns true if item is found in the list, false otherwise."; } }
		public override GroupType Group { get { return GroupType.Lookup; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount >= 2);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		public override bool Validate(Type[] Types, ref int InvalidArgument)
		{
			bool bIsValid = true;
			int i;

			if (Types[0].Equals(typeof(String)))
			{
				if (Types[1].Equals(typeof(String)))
				{
					for (i=2; i<=Types.GetUpperBound(0); i++)
						if (!Types[i].Equals(typeof(String)))
						{
							bIsValid = false;
							InvalidArgument = i;
							break;
						}
				}
				else if (Types.GetUpperBound(0) == 1 && Types[1].IsArray)
				{
					// number array argument
					if (!Types[1].GetElementType().Equals(typeof(String)))
					{
						bIsValid = false;
						InvalidArgument = 1;
					}
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
			int i;
			bool bFound = false;
			
			Array aValues;

			if (Values.GetUpperBound(0) == 1 && Values[1].GetType().IsArray)	// string array param
				aValues = (Array) Values[1];
			else	// string params
			{
				aValues = new object[Values.Length - 1];
				Array.Copy(Values, 1, aValues, 0, Values.Length - 1);
			}

			string sItemToFind = Values[0].ToString();

			for(i = 0; i <= aValues.GetUpperBound(0); i++)
				if (!Convert.IsDBNull(aValues.GetValue(i))) // skip NULLs
				{
					if (String.Compare(sItemToFind, Convert.ToString(aValues.GetValue(i)), !IsCaseSensitive) == 0)
					{
						bFound = true;
						break;
					}
				}
			return bFound;
		}

		public override bool IsNullable(object[] aValues)
		{
			bool bResult = false;

			// returns NULL if the value to search is NULL
			if (Convert.IsDBNull(aValues.GetValue(0)))
			{
					bResult = true;
			}

			return bResult;

		}
	}

	/// <summary>
	/// IsDBNull function
	/// </summary>
	public class IsDBNullFunction : Function
	{
		public IsDBNullFunction() {}
		public override string Name { get { return "IsDBNull"; } }

		public override string Syntax { get { return "IsDBNull(object)"; } }
		public override string Description { get { return "Determines whether or not a given expression is of type DBNull."; } }
		public override GroupType Group { get { return GroupType.Logical; } }

		public override bool MultArgsSupported(int nCount)
		{ 
			return (nCount == 1);
		}

		public override Type GetReturnType(Type[] Types)
		{ 
			return typeof(Boolean);
		}

		protected override bool InputTypeSupported(Type Type, int Index)
		{ 
			// any type, equivalent to "return true;"
			return (Type.IsSubclassOf(typeof(object)) && !Type.IsArray);
		}

		public override object Evaluate(object[] Values)
		{
			return Convert.IsDBNull(Values[0]);
		}

		public override bool IsNullable(object[] aValues)
		{
			return false;
		}
	}

	/// <summary>
	/// IsNull function
	/// </summary>
	public class IsNullFunction : IsDBNullFunction
	{
		public override string Name { get { return "IsNull"; } }

		public override string Syntax { get { return "IsNull(object)"; } }

	}

    /// <summary>
    /// IFNULL function.
    /// </summary>
    public class IfNullFunction : Function
    {
        public IfNullFunction() { }
        public override string Name { get { return "IFNULL"; } }

        public override string Syntax { get { return "IFNULL(value, replacement)"; } }
        public override string Description { get { return "Uses an specified value as replacement if the provided value is null"; } }
        public override GroupType Group { get { return GroupType.Lookup; } }

        public override bool MultArgsSupported(int nCount)
        {
            return (nCount > 0);
        }

        public override Type GetReturnType(Type[] Types)
        {
            if (Types.Length > 1)
                return Types[1];
            if (Types.Length == 1)
                return Types[0];
            return typeof(Object);
        }

        public override bool Validate(Type[] Types, ref int InvalidArgument)
        {
            return Types.Length == 2;
        }

        public override object Evaluate(object[] Values)
        {
            if (Values[0] == null || Values[0] == DBNull.Value || String.IsNullOrEmpty(Values[0].ToString()))
                return Values[1];

            return Values[0];
        }

        public override bool IsNullable(object[] Values)
        {
            return false;
        }

        public override bool UnfoldArray
        {
            get { return false; }
        }
    }

    /// <summary>
    /// Today function.
    /// </summary>
    public class NewGuidFunction : Function
    {
        public NewGuidFunction() { }
        public override string Name { get { return "NEWGUID"; } }

        public override string Syntax { get { return "NEWGUID()"; } }
        public override string Description
        {
            get { return "Returns a new GUID."; }
        }
        public override GroupType Group { get { return GroupType.DateAndTime; } }

        public override bool MultArgsSupported(int nCount)
        {
            return (nCount == 0);
        }

        public override Type GetReturnType(Type[] Types)
        {
            return typeof(String);
        }

        public override object Evaluate(object[] Values)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
