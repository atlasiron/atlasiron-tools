#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;

namespace USP.Express.Pro.Exceptions
{
	/// <summary>
	/// The base class for any exception thrown in <see cref="Parser.Parse"/> method.
	/// </summary>
	public class ParseException: ApplicationException
	{
		public ParseException(string sMessage, int nPos)
		{
			m_sErrorMessage = sMessage;
			m_nPosition = nPos;
		}

		public ParseException(string sMessage)
		{
			m_sErrorMessage = sMessage;
		}

		private string m_sErrorMessage = "Parsing error.";
		private int m_nPosition = -1;

		/// <summary>
		/// Error Message.
		/// </summary>
		public override string Message { get { return m_sErrorMessage; } }

		/// <summary>
		/// Position of invalid character.
		/// </summary>
		public virtual int InvalidCharacterPosition { get { return m_nPosition; } }
	}


	/// <summary>
	/// The base class for any exception thrown in <see cref="ExpressionTree.Evaluate"/> method.
	/// </summary>
	public class EvaluateException: ApplicationException
	{
		public EvaluateException(string sMessage)
		{
			m_sErrorMessage = sMessage;
		}

		private string m_sErrorMessage = "Calculation error.";
		public override string Message { get { return m_sErrorMessage; } }
	}


	// === Adding Items Errors ===


	/// <summary>
	/// Identifier is not unique.
	/// </summary>
	public class DuplicateIdentifierException: ApplicationException
	{
		public DuplicateIdentifierException() {}
		private string m_sErrorMessage = "Duplicate identifier found.";
		public override string Message { get { return m_sErrorMessage; } }
	}
	 
	/// <summary>
	/// Identifier is not valid.
	/// </summary>
	public class InvalidIdentifierException: ApplicationException
	{
		public InvalidIdentifierException() {}
		private string m_sErrorMessage = "Invalid identifier name.";
		public override string Message { get { return m_sErrorMessage; } }
	}

	
	// === Parsing Errors ===


	/// <summary>
	/// Empty expression.
	/// </summary>
	public class EmptyExpressionException: ParseException
	{
		public EmptyExpressionException() : base ("Empty expression.") {}
	}

	/// <summary>
	/// Missing '"' or '#'.
	/// </summary>
	public class MissingSymbolException: ParseException
	{
		public MissingSymbolException(char cSymbol) : base ("Missing closing " + cSymbol + " symbol.") {}
	}

	/// <summary>
	/// Unbalanced '(' or ')'.
	/// </summary>
	public class UnbalancedParenthesesException: ParseException
	{
		public UnbalancedParenthesesException() : base ("Unbalanced parentheses.") {}
	}

	/// <summary>
	/// Wrong arguments number.
	/// </summary>
	public class WrongArgumentsNumberException: ParseException
	{
		public WrongArgumentsNumberException(int nPos) : base ("Wrong number of function arguments.", nPos) {}
	}

	/// <summary>
	/// Unknown variable.
	/// </summary>
	public class UnknownVariableException: ParseException
	{
		public UnknownVariableException(int nPos) : base ("Unknown variable is used.",nPos) {}
	}

	/// <summary>
	/// Unknown function.
	/// </summary>
	public class UnknownFunctionException: ParseException
	{
		public UnknownFunctionException(int nPos) : base ("Unknown function is used.",nPos) {}
	}

	/// <summary>
	/// Unexpected symbol.
	/// </summary>
	public class UnexpectedSymbolException: ParseException
	{
		public UnexpectedSymbolException(int nPos) : base ("Unexpected symbol.", nPos) {}
	}

	/// <summary>
	/// Escape character in string literal.
	/// </summary>
	public class InvalidCharacterInStringException: ParseException
	{
		public InvalidCharacterInStringException(int nPos) : base ("Invalid character in string literal.", nPos) {}
	}

	/// <summary>
	/// Invalid operator location
	/// </summary>
	public class InvalidOperatorLocationException: ParseException
	{
		public InvalidOperatorLocationException(int nPos) : base ("Invalid occurrence of operator.", nPos) {}
	}

	/// <summary>
	/// Operator is not supported in the trial version.
	/// </summary>
	public class OperatorNotSupportedInTrialVersionException: ParseException
	{
		public OperatorNotSupportedInTrialVersionException(int nPos) : base ("The operator is not supported in trial version.", nPos) {}
	}

	/// <summary>
	/// Function is not supported in trial version.
	/// </summary>
	public class FunctionNotSupportedInTrialVersionException: ParseException
	{
		public FunctionNotSupportedInTrialVersionException(int nPos) : base ("The function is not supported in trial version.", nPos) {}
	}

#if TIME_TRIAL
	public class TrialVersionExpiredException: ParseException
	{
		public TrialVersionExpiredException() : base ("The trial version has expired.") {}
	}
#endif

	/// <summary>
	/// Invalid term location.
	/// </summary>
	public class InvalidTermLocationException: ParseException
	{
		public InvalidTermLocationException(int nPos) : base ("Invalid occurrence of term.", nPos) {}
	}

	/// <summary>
	/// Invalid function location.
	/// </summary>
	public class InvalidFunctionLocationException: ParseException
	{
		public InvalidFunctionLocationException(int nPos) : base ("Invalid occurrence of function.", nPos) {}
	}

	/// <summary>
	/// Invalid parenthesis location.
	/// </summary>
	public class InvalidParenthesisLocationException: ParseException
	{
		public InvalidParenthesisLocationException(int nPos) : base ("Invalid occurrence of parenthesis.", nPos) {}
	}

	/// <summary>
	/// Invalid comma location.
	/// </summary>
	public class InvalidCommaLocationException: ParseException
	{
		public InvalidCommaLocationException(int nPos) : base ("Invalid occurrence of comma.", nPos) {}
	}

	/// <summary>
	/// Invalid expression.
	/// </summary>
	public class InvalidExpressionException: ParseException
	{
		public InvalidExpressionException(int nPos) : base ("Invalid expression.", nPos) {}
	}

	/// <summary>
	/// Invalid date expression.
	/// </summary>
	public class InvalidDateException: ParseException
	{
		public InvalidDateException (int nPos) : base ("Invalid date expression.", nPos) {}
	}

	/// <summary>
	/// Invalid Argument type.
	/// </summary>
	public class InvalidArgumentTypeException: ParseException
	{
		public InvalidArgumentTypeException(int nPos, int nIndex) : base ("Invalid argument type.", nPos)
		{
			m_nIndex = nIndex;
		}

		private int m_nIndex = -1;

		/// <summary>
		/// Invalid argument index.
		/// </summary>
		public int InvalidArgumentIndex { get { return m_nIndex; } }

	}


	// === Evaluating Errors ===


	/// <summary>
	/// Division by zero.
	/// </summary>
	public class DivisionByZeroException: EvaluateException
	{
		public DivisionByZeroException() : base ("Division by zero.") {}
	}

	/// <summary>
	/// ACOS argument is out of bounds.
	/// </summary>
	public class ACosOutOfBoundsException: EvaluateException
	{
		public ACosOutOfBoundsException() : base ("Arc cosine argument is out of bounds.") {}
	}

	/// <summary>
	/// ACOT of zero.
	/// </summary>
	public class ACotOfZeroException: EvaluateException
	{
		public ACotOfZeroException() : base ("Arc cotangent of zero.") {}
	}
	
	/// <summary>
	/// ACSC argument is out of bounds.
	/// </summary>
	public class ACscOutOfBoundsException: EvaluateException
	{
		public ACscOutOfBoundsException() : base ("Arc cosecant argument is out of bounds.") {}
	}

	/// <summary>
	/// ASEC argument is out of bounds.
	/// </summary>
	public class ASecOutOfBoundsException: EvaluateException
	{
		public ASecOutOfBoundsException() : base ("Arc secant argument is out of bounds.") {}
	}

	/// <summary>
	/// ASIN argument is out of bounds.
	/// </summary>
	public class ASinOutOfBoundsException: EvaluateException
	{
		public ASinOutOfBoundsException() : base ("Arc sine argument is out of bounds.") {}
	}

	/// <summary>
	/// Infinite cotangent.
	/// </summary>
	public class InfiniteCotException: EvaluateException
	{
		public InfiniteCotException() : base ("Infinite cotangent.") {}
	}

	/// <summary>
	/// Infinite hyperbolic cotangent.
	/// </summary>
	public class InfiniteHCotException: EvaluateException
	{
		public InfiniteHCotException() : base ("Infinite hyperbolic cotangent.") {}
	}

	/// <summary>
	/// Infinite cosecant.
	/// </summary>
	public class InfiniteCscException: EvaluateException
	{
		public InfiniteCscException() : base ("Infinite cosecant.") {}
	}

	/// <summary>
	/// Infinite hyperbolic cosecant.
	/// </summary>
	public class InfiniteHCscException: EvaluateException
	{
		public InfiniteHCscException() : base ("Infinite hyperbolic cosecant.") {}
	}

	/// <summary>
	/// Negative argument for LN.
	/// </summary>
	public class LnNegNumberException: EvaluateException
	{
		public LnNegNumberException() : base ("Negative argument of natural logarithm.") {}
	}

	/// <summary>
	/// Zero argument for LN.
	/// </summary>
	public class LnZeroException: EvaluateException
	{
		public LnZeroException() : base ("Zero argument of natural logarithm.") {}
	}

	/// <summary>
	/// Negative argument for LOG10.
	/// </summary>
	public class Log10NegNumberException: EvaluateException
	{
		public Log10NegNumberException() : base ("Negative argument of common logarithm.") {}
	}

	/// <summary>
	/// Zero argument for LN.
	/// </summary>
	public class Log10ZeroException: EvaluateException
	{
		public Log10ZeroException() : base ("Zero argument of common logarithm.") {}
	}

	/// <summary>
	/// Base is equal to 1 for LOG.
	/// </summary>
	public class LogBaseEq1Exception: EvaluateException
	{
		public LogBaseEq1Exception() : base ("Logarithm base is equal to 1.") {}
	}

	/// <summary>
	/// Negative base for LOG.
	/// </summary>
	public class LogNegBaseException: EvaluateException
	{
		public LogNegBaseException() : base ("Negative base of logarithm.") {}
	}

	/// <summary>
	/// Zero base for LOG.
	/// </summary>
	public class LogZeroBaseException: EvaluateException
	{
		public LogZeroBaseException() : base ("Zero base of logarithm.") {}
	}

	/// <summary>
	/// Negative argument for LOG.
	/// </summary>
	public class LogNegNumberException: EvaluateException
	{
		public LogNegNumberException() : base ("Negative argument of logarithm.") {}
	}

	/// <summary>
	/// Zero argument for LOG.
	/// </summary>
	public class LogZeroException: EvaluateException
	{
		public LogZeroException() : base ("Zero argument of logarithm.") {}
	}

	/// <summary>
	/// Infinite secant.
	/// </summary>
	public class InfiniteSecException: EvaluateException
	{
		public InfiniteSecException() : base ("Infinite secant.") {}
	}

	/// <summary>
	/// Infinite hyperbolic secant.
	/// </summary>
	public class InfiniteHSecException: EvaluateException
	{
		public InfiniteHSecException() : base ("Infinite hyperbolic secant.") {}
	}

	/// <summary>
	/// Negative argument for SQRT.
	/// </summary>
	public class SqrtNegNumberException: EvaluateException
	{
		public SqrtNegNumberException() : base ("Negative argument of even root.") {}
	}

	/// <summary>
	/// Infinite tangent.
	/// </summary>
	public class InfiniteTanException: EvaluateException
	{
		public InfiniteTanException() : base ("Infinite tangent.") {}
	}

	/// <summary>
	/// Infinite hyperbolic tangent.
	/// </summary>
	public class InfiniteHTanException: EvaluateException
	{
		public InfiniteHTanException() : base ("Infinite hyperbolic tangent.") {}
	}

	/// <summary>
	/// Invalid Argument type.
	/// </summary>
	public class InvalidArgumentException: EvaluateException
	{
		public InvalidArgumentException() : base ("Invalid argument type.") {}
	}

	/// <summary>
	/// Argument is out of range.
	/// </summary>
	public class ArgumentOutOfRangeException: EvaluateException
	{
		public ArgumentOutOfRangeException() : base ("Specified argument was out of the range of valid values.") {}
	}

	/// <summary>
	/// The number of variables passed to <see cref="ExpressionTree.Evaluate"/> function does not match the number of variables originally passed to <see cref="Parser.Parse"/> function.
	/// </summary>
	public class InvalidParameterCountException: EvaluateException
	{
		public InvalidParameterCountException() : base ("Invalid parameter count.") {}
	}

}
