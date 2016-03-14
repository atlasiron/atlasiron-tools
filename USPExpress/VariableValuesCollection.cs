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
	/// Contains a collection of <see cref="VariableValue"/> objects.
	/// </summary>
	public class VariableValuesCollection: ReadOnlyCollectionBase, ICloneable
	{
		private Hashtable _hashtable;
		
		internal VariableValuesCollection( IList list )  
		{
			_hashtable = new Hashtable( list.Count );
			foreach( Object o in list )
			{
				VariableValue variableValue = (VariableValue)o;
				_hashtable.Add( variableValue.Name, variableValue );
				foreach( String alias in variableValue.Aliases )
				{
					_hashtable.Add( alias, variableValue );	
				}
				InnerList.Add( variableValue );
			}
		}

		/// <summary>
		/// Gets the variable at the specified index.
		/// </summary>
		public VariableValue this[ int index ]  
		{
			get  
			{
				return  ( VariableValue )InnerList[ index ];
			}
		}

		/// <summary>
		///Gets the variable associated with the specified name or alias.
		///If the specified name is not found, attempting to get it returns a null reference.
		/// </summary>
		public VariableValue this[ string name ]			
		{
			get
			{
				return ( VariableValue )_hashtable[ name ];
			}
		}

		/// <summary>
		/// Determines the index of a specified VariableValue in the collection.
		/// </summary>
		/// <param name="value">The VariableValue to locate in the collection.</param>
		/// <returns>The zero-based index of the VariableValue in the collection.</returns>
		public int IndexOf( VariableValue value )  
		{
			return InnerList.IndexOf( value );
		}

		/// <summary>
		/// Determines whether the collection contains a specific <see cref="VariableValue"/> object.
		/// </summary>
		/// <param name="var">The variable to locate in the collection.</param>
		/// <returns>True if the specified variable is found in the collection; otherwise, false.</returns>
		public bool Contains( VariableValue var )  
		{
			return InnerList.Contains( var );
		}	
		
		object ICloneable.Clone()
		{
			return Clone();
		}

		internal protected VariableValuesCollection Clone()
		{
			ArrayList temp = new ArrayList();
			foreach( VariableValue variableValue in this )
			{
				temp.Add( variableValue.Clone() );
			}
			return new VariableValuesCollection( temp );
		}
	}
}
