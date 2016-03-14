#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;
using System.Collections;

namespace USP.Express.Pro
{
	/// <summary>
	/// A base abstract class for any operator. 
	/// </summary>
	public abstract class Operator: IIdentifier
	{
		protected Operator() {}
		//The following array sets the priority of each operator, whose array index is given above in the OperatorType definition
		//corresponding operator:													(none)	+s  -s  +   -   *   /   %   ^   <  >  ==  =  !=  <> <= >=  &&  ||  !  AND OR  NOT &  |  !&  ~   :  ?
		internal static short[] kPriorityArray = new short[] { 0, 14, 14, 10, 10, 11, 11, 11, 12, 9, 9, 8,  8, 8,  8, 9, 9,  4,  3,  13, 4,  3, 13, 7, 5, 6,  13, 2, 1 }; //zero is reserved for non-operators

		/// <summary>
		/// Returns operator type.
		/// </summary>
		internal abstract OperatorType GetOperatorType();
		/// <summary>
		/// Returns operator priority.
		/// </summary>
		internal virtual int GetPriority() { return kPriorityArray[(int) GetOperatorType()+1]; }

		/// <summary>Returns a string representation of the operator. Read-only.</summary>
		public abstract string Name{get;}
		
		/// <summary>
		/// Returns the number of operands supported by the operator.
		/// </summary>
		/// <remarks>Default implementation returns 2.</remarks>
		public virtual byte OperandsSupported { get { return 2; } }

		/// <summary>
		/// Returns a type of the return value depending on types of operands. Read-only.
		/// </summary>
		/// <param name="Types">Types of operands.</param>
		/// <returns>Type of the return value.</returns>
		/// <remarks>Default implementation returns System.Double. If you want to return value of another type you must override this method.</remarks>
		public virtual Type GetReturnType(Type[] Types)
		{
			return typeof(Double);
		}

		/// <summary>
		/// Returns a value indicating whether the operator supports the specified type of an operand.
		/// </summary>
		/// <param name="Index">Operand index.</param>
		/// <param name="Type">Operand type.</param>
		/// <returns>True, if the operator supports the specified Type of an operand with the specified Index; otherwise, false.</returns>
		/// <remarks>Default implementation returns true if operand type is System.Double. If you want to support operands of another type you must override this method.</remarks>
		protected virtual bool InputTypeSupported(Type Type, int Index)
		{ 
			return (TypeConverter.CanConvert(Type, typeof(Double)));
		}
		
		/// <summary>
		/// Returns a value indicating whether the operator supports specified operands Types.
		/// </summary>
		/// <param name="Types">Types of operands.</param>
		/// <param name="InvalidArgument">Invalid operand index.</param>
		/// <returns>True, if the operator supports specified Types of operands; otherwise, false.</returns>
		/// <remarks>Default implementation calls <see cref="InputTypeSupported"/> method iteratively for each of the input parameters. If the types of your input parameters depend on each other, you must override this method.</remarks>
		public virtual bool Validate(Type[] Types, ref int InvalidArgument)
		{
			int i;
			for (i=0; i<=Types.GetUpperBound(0); i++)
			{
				if (!InputTypeSupported(Types[i], i))
				{
					InvalidArgument = i;	// zero-based
					return false;
				}
			}
			return true;
		}
		
		/// <overloads>Returns a value calculated by the operator.</overloads>
		/// <summary>
		/// Returns a value calculated by the operator. Case-insensitive string comparison is enabled.
		/// </summary>
		/// <param name="Values">Array of input values.</param>
		/// <returns>Calculated value.</returns>
		/// <remarks>If you do not want to support IsCaseSensitive parameter you must override this method; otherwise you must override <c>Evaluate(object[] Values, bool IsCaseSensitive)</c> method.</remarks>
		internal virtual object Evaluate(object[] Values)
		{
			return Evaluate(Values, false);
		}

		/// <summary>
		/// Returns a value calculated by the operator.
		/// </summary>
		/// <param name="Values">Array of values.</param>
		/// <param name="IsCaseSensitive">Value indicating a case-sensitive or insensitive string comparison.</param>
		/// <returns>Calculated value.</returns>
		/// <remarks>If you want to support IsCaseSensitive parameter you must override this method; otherwise you must override <c>Evaluate(object[] Values)</c> method.</remarks>
		public virtual object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			return Evaluate(Values);
		}

		/// <summary>
		/// Determines if the operator returns <c>DBNull</c> when one of its input parameters is <c>DBNull</c>.		
		/// </summary>
		/// <param name="Values">Array of input values.</param>
		/// <returns><c>true</c>, if the operator returns <c>DBNull</c>, otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>
		/// Default implementation returns <c>DBNull</c> if at least one of the operator's input arguments is <c>DBNull</c>.
		/// </para>
		/// <para>
		/// You should override this method only if you would like to implement different <c>DBNull</c> processing logic.
		/// For example, the following built-in USPExpress operators override this method:
		/// <list type="bullet">
		/// <item>
		/// Logical operators: AND, OR
		/// </item>
		/// </list>
		/// </para>
		/// </remarks>
		public virtual bool IsNullable(object[] Values)
		{
			bool bResult = false;

			// assuming there are no arrays embedded
			for(int i = 0; i <= Values.GetUpperBound(0); i++)
			{
				if (Convert.IsDBNull(Values.GetValue(i)))
				{
					bResult = true;
					break;
				}
			}

			return bResult;

		}
		
	}

	// The following is a list of operator types. All operators must have an entry in this enum.
	public enum OperatorType 
	{
		noOperator = -1,					// (none)

		//	MODIFIERS
		plusModifier,					//	+ (sign)
		minusModifier,					//	- (sign)

		//	ARITHMETIC OPERATORS
		plusOperator,					//	+
		minusOperator,					//	-
		multiplyOperator,				//	*
		divideOperator,					//	/
		modulusOperator,				//	%
		powerOperator,					//	^	


		// 	BOOLEAN OPERATORS	
		isLessThanOperator,				//	<
		isGreaterThanOperator,			//	>
		isEqualToOperator,				//	==
		isBasicEqualToOperator,			//	=
		isNotEqualToOperator,			//	!=
		isBasicNotEqualToOperator,		//	<>
		isLessThanOrEqualToOperator, 	//	<=
		isGreaterThanOrEqualToOperator, //	>=
	
		//	LOGICAL OPERATORS	
		andOperator,					//	&&
		orOperator,					    //	||
		notOperator,					//	!
		andBasicOperator,				//	AND
		orBasicOperator,				//	OR
		notBasicOperator,				//	NOT
	
		//	BITWISE OPERATORS
		bitwiseAndOperator,				//	&
		bitwiseInclusiveOrOperator,		//	|
		bitwiseExclusiveOrOperator,		//	!& (same thing as '^' in C, except we are already using that for powerOperator )
		bitwiseComplement,				//	~

		lastOperator
	};
}
