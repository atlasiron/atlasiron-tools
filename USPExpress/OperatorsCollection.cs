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
	/// Collection of operators.
	/// </summary>
	public class OperatorsCollection: ReadOnlyCollectionBase
	{
		internal OperatorsCollection()
		{}
		
		internal OperatorsCollection( IList list )  
		{
			InnerList.AddRange( list );
		}
		
		/// <summary>
		/// Get the operator at the specified index.
		/// </summary>
		public Operator this[ int index ]  
		{			
			get  
			{
				return  ( Operator )InnerList[ index ];
			}
			set  
			{
				InnerList[ index ] = value;
			}
		}

		/// <summary>
		/// Determines the index of a specified Operator in the collection.
		/// </summary>
		/// <param name="oper">The Operator to locate in the collection.</param>
		/// <returns>The zero-based index of the Operator in the collection.</returns>
		public int IndexOf( Operator oper )
		{
			return InnerList.IndexOf( oper );
		}

		/// <summary>
		/// Determines whether the collection contains a specific <see cref="Operator"/> object.
		/// </summary>
		/// <param name="oper">The operator to locate in the collection.</param>
		/// <returns>True if the specified Operator is found in the collection; otherwise, false.</returns>
		public bool Contains( Operator oper )
		{
			return InnerList.Contains( oper );
		}
	}
}
