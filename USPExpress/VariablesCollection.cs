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
	/// Contains a collection of variables.
	/// </summary>
	public class VariablesCollection: IdentifiersCollection
	{
		// VariableAdd event
		internal delegate void AddEventHandler( string Name );
		internal event AddEventHandler VariableAdd;

		// Changed event
		internal delegate void ChangedEventHandler();
		internal event ChangedEventHandler Changed;

		internal VariablesCollection()
		{}
		
		/// <summary>
		/// Adds a variable to the collection
		/// </summary>
		/// <param name="variable">Variable to be added</param>
		/// <returns>The index at which the variable has been added.</returns>
		public int Add( Variable variable )
		{			
			return List.Add( variable );
		}

		/// <summary>
		/// Removes a variable from the collection
		/// </summary>
		/// <param name="variable">Variable to be removed</param>
		public void Remove( Variable variable )
		{
			List.Remove( variable );
		}

		/// <summary>
		/// Get the variable at the specified index.
		/// </summary>
		public Variable this[ int index ]
		{
			get
			{
				return  ( Variable )List[ index ];
			}
		}
		
		/// <summary>
		///  Performs additional custom processes before inserting a new element into the collection. 
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsert( int index, object value )
		{			
			Variable variable = ( Variable )value;
			RaiseVariableAddEvent( variable.Name );
		}

		/// <summary>
		/// Performs additional custom processes after inserting a new element into the collection
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete( index, value );	
			
			Variable variable = ( Variable )value;			
			
			for ( int i = 0; i < variable.Aliases.Count; ++i )
			{				
				RaiseVariableAddEvent( variable.Aliases[ i ] );				
				AddToHashtable( variable.Aliases[ i ], index );
			}			
			
			variable.AliasAdd += new Variable.AliasAddEventHandler( OnAliasAdd );
			variable.AliasRemove += new Variable.AliasRemoveEventHandler( OnAliasRemove );
			
			RaiseChangedEvent();			
		}

		protected override void OnRemoveComplete(int index, object value)
		{		
			Variable variable = ( Variable )value;

			variable.AliasAdd -= new Variable.AliasAddEventHandler( OnAliasAdd );
			variable.AliasRemove -= new Variable.AliasRemoveEventHandler( OnAliasRemove );

			for ( int i = 0; i < variable.Aliases.Count; ++i )
			{				
				RemoveFromHashtable( variable.Aliases[ i ] );
			}				

			base.OnRemoveComplete( index, value );
			
			RaiseChangedEvent();
		}

		/// <summary>
		/// Updates indices in the inner hashtable of names, starting with the specified index.
		/// </summary>
		/// <param name="start">Index to start with.</param>
		/// <remarks>Called when the Variable is removed from the collection.</remarks>
		protected override void RefreshIndices( int start ) 
		{ 				
			for ( int i = start; i < Count; ++i )
			{
				Variable variable = ( ( Variable ) this[ i ] );
				
				ReplaceInHashtable( variable.Name, i - 1 );
				
				for ( int j = 0; j < variable.Aliases.Count; ++j )
				{					
					ReplaceInHashtable( variable.Aliases[ j ], i - 1 );
				}
			}
		}

		/// <summary>
		/// Handles <see cref="Variable.AliasAdd"/> event.
		/// Updates inner hashtable.
		/// </summary>
		/// <param name="variableName">Variable name.</param>
		/// <param name="alias">New alias.</param>
		private void OnAliasAdd( string variableName, string alias )
		{ 
			RaiseVariableAddEvent( alias );			
			AddToHashtable( alias, Search( variableName ) );
			RaiseChangedEvent();
		}

		/// <summary>
		/// Handles <see cref="Variable.AliasRemove"/> event.
		/// Updates inner hashtable of variables names.
		/// </summary>
		/// <param name="alias">Alias.</param>
		private void OnAliasRemove( string alias )
		{ 
			RemoveFromHashtable( alias );
			RaiseChangedEvent();
		}

		/// <summary>
		/// Raises <see cref="VariableAdd"/> event.
		/// </summary>
		/// <param name="Name">New variable name.</param>
		private void RaiseVariableAddEvent( string Name )
		{
			if ( VariableAdd != null )
			{
				VariableAdd(Name);
			}
		}

		/// <summary>
		/// Raises <see cref="Changed"/> event.
		/// </summary>
		private void RaiseChangedEvent()
		{
			if ( Changed != null )
			{
				Changed();
			}
		}
	}	
}
