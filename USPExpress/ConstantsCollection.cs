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
	/// Contains a collection of constants.
	/// </summary>
	public class ConstantsCollection: IdentifiersCollection
	{
		// ConstantAdd event
		internal delegate void AddEventHandler( string NewName );
		internal event AddEventHandler ConstantAdd;
		
		internal ConstantsCollection()
		{}

		/// <summary>
		/// Adds a constant to the collection
		/// </summary>
		/// <param name="constant">Constant to be added</param>
		/// <returns>The index at which the constant has been added.</returns>
		public int Add( Constant constant )
		{			
			return List.Add( constant );
		}

		/// <summary>
		/// Removes a constant from the collection
		/// </summary>
		/// <param name="constant">Constant to be removed</param>
		public void Remove( Constant constant )
		{
			List.Remove( constant );
		}

		/// <summary>
		/// Get the constant at the specified index.
		/// </summary>
		public Constant this[ int index ]
		{
			get
			{
				return  ( Constant )List[ index ];
			}
		}

		/// <summary>
		///  Performs additional custom processes before inserting a new element into the collection. 
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsert(int index, object value)
		{
			Constant constant = ( Constant )value;
			RaiseConstantAddEvent( constant.Name );			
		}

		/// <summary>
		/// Raises ConstantAdd event.
		/// </summary>
		/// <param name="NewName">New constant name.</param>
		private void RaiseConstantAddEvent(string NewName)
		{
			if ( ConstantAdd != null )
			{
				ConstantAdd( NewName );
			}
		}
	}	
}

