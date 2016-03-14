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
	/// Contains a collection of functions.
	/// </summary>
	public class FunctionsCollection: IdentifiersCollection
	{
		internal delegate void AddEventHandler(string NewName);
		internal event AddEventHandler FunctionAdd;
		
		internal FunctionsCollection()
		{}

		/// <summary>
		/// Adds a function to the collection.
		/// </summary>
		/// <param name="function">Function to be added</param>
		/// <returns>The index at which the function has been added.</returns>
		public int Add( Function function )
		{			
			return List.Add( function );
		}

		/// <summary>
		/// Removes a function from the collection.
		/// </summary>
		/// <param name="function">Function to be removed</param>
		public void Remove( Function function )
		{
			List.Remove( function );
		}

		/// <summary>
		/// Gets or sets the function at the specified index.
		/// </summary>
		public Function this[ int index ]
		{
			get
			{
				return  ( Function )List[ index ];
			}

			set  
			{
				InnerList[ index ] = value;
			}
		}

		/// <summary>
		///  Performs additional custom processes before inserting a new element into the collection. 
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsert(int index, object value)
		{
			Function function = ( Function )value;
			RaiseFunctionAddEvent( function.Name );
		}	

		/// <summary>
		/// Raises FunctionAdd event.
		/// </summary>
		/// <param name="NewName">New function name.</param>
		private void RaiseFunctionAddEvent(string NewName)
		{
			if ( FunctionAdd != null )
			{
				FunctionAdd(NewName);
			}
		}
	}
}
