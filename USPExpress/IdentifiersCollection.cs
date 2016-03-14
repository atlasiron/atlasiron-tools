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
	/// Contains a collection of identifiers. 
	/// </summary>
	/// <remarks>This is a base collection class for specific collections in USPExpress</remarks> 
	public class IdentifiersCollection: CollectionBase
	{
		private Hashtable _hashtable;
		
		internal protected IdentifiersCollection()
		{
			_hashtable = new Hashtable( CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant );		
		}

		/// <summary>
		/// Returns a zero-based index of the identifier with the specified name.
		/// </summary>
		/// <param name="name">Identifier name.</param>
		/// <returns>The zero-based index of the identifier in the collection. -1, if no identifier with that name found in the collection.</returns>
		public int Search( string name )
		{
			Object index = _hashtable[ name ];
			return index == null ? -1 : ( int )index;
		}

		/// <summary>
		/// Determines the index of a specified Identifier in the collection.
		/// </summary>
		/// <param name="identifier">The Identifier to locate in the collection.</param>
		/// <returns>The zero-based index of the Identifier in the collection.</returns>
		public int IndexOf( IIdentifier identifier )  
		{
			return InnerList.IndexOf( identifier );
		}

		/// <summary>
		/// Determines whether the collection contains a specific object.
		/// </summary>
		/// <param name="identifier">The identifier to locate in the collection.</param>
		/// <returns>True if the specified identifier is found in the collection; otherwise, false.</returns>
		public bool Contains( IIdentifier identifier )  
		{
			return InnerList.Contains( identifier );
		}	

		protected void AddToHashtable( string name, int index )
		{
			_hashtable.Add( name, index );
		}

		protected void RemoveFromHashtable( string name )
		{
			_hashtable.Remove( name );
		}		

		protected void ReplaceInHashtable( string name, int index )
		{
			_hashtable[ name ] = index;
		}

		/// <summary>
		/// Performs additional custom processes after inserting a new element into the collection
		/// </summary>
		/// <param name="index">The zero-based index at which to insert value.</param>
		/// <param name="value">The new value of the element at index.</param>
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete ( index, value );
			AddToHashtable( (( IIdentifier )value ).Name, index );
		}

		protected override void OnRemoveComplete( int index, object value )
		{
			base.OnRemoveComplete ( index, value );
			RemoveFromHashtable( ( ( IIdentifier )value ).Name );

			RefreshIndices( index );			
		}

		protected override void OnClearComplete()
		{
			_hashtable.Clear();
		}

		protected virtual void RefreshIndices( int start ) 
		{
			for ( int i = start; i < Count; ++i )
			{
				IIdentifier identifier = ( IIdentifier )List[ i ];
				_hashtable[ identifier.Name ] = i - 1;
			}
		}

		protected IIdentifier this[ string name ]
		{
			get
			{
				int index = Search( name );
				return index == -1 ? null : ( IIdentifier )List[ index ];
			}
		}
	}
}
