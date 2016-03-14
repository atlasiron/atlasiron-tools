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
	/// A base abstract class for any function. 
	/// You must inherit from this class in order to add a user-defined function to the <see cref="Parser.Functions"/> collection.
	/// </summary>
	public abstract class Function: IIdentifier
	{
		/// <summary>Syntax</summary>
		public virtual string Syntax // are not abstract for backward compatibility
		{
			get {return "UNDEFINED";}
		}
		/// <summary>Description</summary>
		public virtual string Description		// are not abstract for backward compatibility
		{
			get {return "UNDEFINED";}
		}

		/// <summary>Group type</summary>
		public virtual Constants.GroupType Group // are not abstract for backward compatibility
		{
			get {return Constants.GroupType.Custom;}
		}	

		/// <summary>Returns the name of the function. Read-only.</summary>
		public abstract string Name{get;}

		/// <summary>
		/// Returns a value indicating whether the function supports a specified number of input parameters.
		/// </summary>
		/// <param name="Count">Number of input parameters.</param>
		/// <returns>True, if the function supports a specified number of input parameters; otherwise, false.</returns>
		/// <remarks>Default implementation returns true if number of input parameters is equal to 1. If you want to support different number of input parameters you must override this method.</remarks>
		public virtual bool MultArgsSupported(int Count)
		{ 
			return (Count == 1);
		}

		/// <summary>
		/// Returns a type of the return value. Read-only.
		/// </summary>
		/// <param name="Types">Types of input parameters.</param>
		/// <returns>Type of the return value.</returns>
		/// <remarks>Default implementation returns System.Double. If you want to return value of different type you must override this method.</remarks>
		public virtual Type GetReturnType( Type[] Types )
		{
			return typeof(Double);
		}

		/// <summary>
		/// Returns a value indicating whether the function supports the specified type of input parameter.
		/// </summary>
		/// <param name="Type">Input parameter type.</param>
		/// <param name="Index">Input parameter index.</param>
		/// <returns>True, if the function supports specified type of the parameter with the specified Index; otherwise, false.</returns>
		/// <remarks>Default implementation returns true for System.Double type. If you want to support parameters of different type you must override this method.</remarks>
		protected virtual bool InputTypeSupported(Type Type, int Index)
		{ 
			// || (ExpressionTree.DBNullHandling != DBNullHandling.None && Type.Equals(typeof(System.DBNull)))
			return (TypeConverter.CanConvert(Type, typeof(Double)));
		}

		/// <summary>
		/// Returns a value indicating whether the function supports specified types of input parameters.
		/// </summary>
		/// <param name="Types">Array of types of input parameters.</param>
		/// <param name="InvalidArgument">Invalid argument index.</param>
		/// <returns>True, if the function supports specified types; otherwise, false.</returns>
		/// <remarks>Default implementation calls <see cref="InputTypeSupported"/> method iteratively for each of the input parameters. You must override this method only if the types of your input parameters depend on each other.</remarks>
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

		/// <overloads>Returns a value calculated by the function. 
		/// When deriving from the <see cref="Function"/> class, you must override one of the overloaded versions of <c>Evaluate</c> method.
		/// </overloads>
		/// <summary>
		/// Returns a value calculated by the function. 
		/// </summary>
		/// <param name="Values">Array of input values.</param>
		/// <returns>Calculated value.</returns>
		/// <remarks>You should override this method if you do not need to account for case-sensitivity; otherwise you must override <c>Evaluate(object[] Values, bool IsCaseSensitive)</c> method.</remarks>
		public virtual object Evaluate(object[] Values)
		{
			return Evaluate(Values, false);
		}

		/// <summary>
		/// Returns a value calculated by the function. Allows to account for case-sensitivity.
		/// </summary>
		/// <param name="Values">Array of input values.</param>
		/// <param name="IsCaseSensitive">Determines if string comparisons are case-sensitive.</param>
		/// <returns>Calculated value.</returns>
		/// <remarks>If you need to account for case-sensitivity you must override this method; otherwise you should override <c>Evaluate(object[] Values)</c> method.</remarks>
		public virtual object Evaluate(object[] Values, bool IsCaseSensitive)
		{
			return Evaluate(Values);
		}

		/// <summary>
		/// Determines if the function returns <c>DBNull</c> when one of its input parameters is <c>DBNull</c>.		
		/// </summary>
		/// <param name="Values">Array of input values.</param>
		/// <returns><c>true</c>, if function returns <c>DBNull</c>, otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>
		/// Default implementation returns <c>DBNull</c> if at least one of the function's input arguments is <c>DBNull</c>.
		/// </para>
		/// <para>
		/// You should override this method only if you would like to implement different <c>DBNull</c> processing logic.
		/// For example, the following built-in USPExpress functions override this method:
		/// <list type="bullet">
		/// <item>
		/// Logical functions: AND, OR
		/// </item>
		/// <item>
		/// Aggregate functions: Count, Min, Max, Sum
		/// </item>
		/// <item>
		/// IIF funciton
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

		/// <summary>
		/// Returns true if a single input parameter of Array type has to be unfolded into an array of parameters. The conversion is performed prior to the Evaluate call.
		/// </summary>
		/// <remarks>Used for functions that accept any number of input parameters, e.g. Sum, Min, Max, etc...</remarks>
		public virtual bool UnfoldArray
		{
			get {return false;}
		}

	}
}
