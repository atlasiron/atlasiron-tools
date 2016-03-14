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
	/// Stores information on variables in an expression tree.
	/// </summary>
	public class ExpressionVariablesCollection: ReadOnlyCollectionBase
	{
		internal ExpressionVariablesCollection()
		{}
		
		internal ExpressionVariablesCollection( IList list )  
		{
			InnerList.AddRange( list );
		}
		
		/// <summary>
		/// Get the variable at the specified index.
		/// </summary>
		public Variable this[ int index ]  
		{
			get  
			{
				return  ( Variable )InnerList[ index ];
			}
		}

		/// <summary>
		/// Determines the index of a specific Variable in the collection.
		/// </summary>
		/// <param name="variable">The Variable to locate in the collection.</param>
		/// <returns>The zero-based index of the Variable in the collection.</returns>
		public int IndexOf( Variable variable )  
		{
			return InnerList.IndexOf( variable );
		}

		/// <summary>
		/// Determines whether the collection contains a specific <see cref="Variable"/> object.
		/// </summary>
		/// <param name="variable">The variable to locate in the collection.</param>
		/// <returns>True if the specified Variable is found in the collection; otherwise, false.</returns>
		public bool Contains( Variable variable )  
		{
			return InnerList.Contains( variable );
		}
	}
}
