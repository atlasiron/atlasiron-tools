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
	/// Contains information about a variable and its value.
	/// </summary>
	/// <remarks>
	/// Allows one to specify a value of the variable to be used in evaluation. 
	/// See also <see cref="ExpressionTree.GetUsedVariables"/> and <see cref="ExpressionTree.Evaluate"/> methods.
	/// </remarks>
	public class VariableValue: ICloneable, IVariable
	{
		private int _index;
		private object _value;
		private Variable _variable;

		internal VariableValue( int index, Variable variable )
		{			
			_index = index;
			_value = null;
			_variable = variable;
		}
	
		/// <summary>Index of the variable in the <see cref="VariablesCollection"/> collection</summary>
		internal int Index
		{
			get
			{
				return _index;
			}
		}

		/// <summary>Value of the variable.</summary>
		public object Value
		{
			set
			{
				_value = value;
			}
			
			get
			{
				return _value;
			}
		}				
		
		/// <summary>
		/// Creates a shallow copy of the object.
		/// </summary>
		/// <returns>A shallow copy of the object.</returns>
		public object Clone()
		{			
			return new VariableValue( _index, _variable );
		}		
	
		/// <summary>Name of the variable.</summary>
		public string Name
		{
			get
			{
				return _variable.Name;
			}
		}	
	
		/// <summary>Type of the variable.</summary>
		public Type Type
		{
			get
			{
				return _variable.Type;
			}
		}

		/// <summary>Variable Aliases.</summary>
		public AliasesCollection Aliases
		{
			get
			{
				return _variable.Aliases;
			}
		}

	}	
}
