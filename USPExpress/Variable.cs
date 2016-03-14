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
	/// Interface that describes a Variable
	/// </summary>
	public interface IVariable: ITypedIdentifier
	{
		/// <summary>Variable Aliases.</summary> 
		AliasesCollection Aliases
		{ 
			get; 
		}
	}

	/// <summary>
	/// Represents Variable.
	/// </summary>
	public class Variable: IVariable
	{
		private AliasesCollection m_cAliases;		
		
		private readonly Type m_oType;		
		private readonly string m_sName;
	
		// AliasAdd event
		internal delegate void AliasAddEventHandler(string VariableName, string Alias);
		internal event AliasAddEventHandler AliasAdd;

		// AliasRemove event
		internal delegate void AliasRemoveEventHandler(string Alias);
		internal event AliasRemoveEventHandler AliasRemove;		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Variable"/> class.
		/// </summary>
		/// <param name="name">Name of the variable</param>
		/// <param name="type">Type of the variable</param>
		public Variable( string name, Type type )			
		{
			m_cAliases = new AliasesCollection();
			m_cAliases.AliasAdd += new AliasesCollection.AddEventHandler(RaiseAliasAddEvent);
			m_cAliases.AliasRemove += new AliasesCollection.RemoveEventHandler(RaiseAliasRemoveEvent);			
			m_oType = type;
			m_sName = name;
		}			

		/// <summary>
		/// Name of the variable. Read-only.
		/// </summary>
		public string Name
		{
			get
			{				
				return m_sName;
			}
		}

		/// <summary>
		/// Variable Type. Read-only.
		/// </summary>
		public Type Type
		{
			get 
			{ 
				return m_oType; 
			}	
		}

		/// <summary>
		/// Variable Aliases. 
		/// </summary>
		/// <remarks>
		/// Each variable can have one or more aliases. In expressions, aliases can be used 
		/// interchangeably with actual name of the variable.
		/// </remarks>
		public AliasesCollection Aliases
		{
			get 
			{ 
				return m_cAliases; 
			}
		}

		/// <summary>
		/// Creates a shallow copy of the current variable.
		/// </summary>
		/// <returns>A shallow copy of the current variable.</returns>
		internal Variable Clone()
		{
			return ( Variable )MemberwiseClone();
		}	

		/// <summary>
		/// Raises AliasAdd event.
		/// </summary>
		/// <param name="Alias">New alias.</param>
		private void RaiseAliasAddEvent( string Alias )
		{
			if ( AliasAdd != null )
			{
				AliasAdd( this.Name, Alias );
			}
		}

		/// <summary>
		/// Raises AliasRemove event.
		/// </summary>
		/// <param name="Alias">Removed alias</param>
		private void RaiseAliasRemoveEvent(string Alias )
		{
			if ( AliasRemove != null )
			{
				AliasRemove(Alias);
			}
		}
	

	}
	
	public class AliasesCollection: CollectionBase
	{
		// AliasAdd event
		internal delegate void AddEventHandler( string Alias );
		internal event AddEventHandler AliasAdd;

		// AliasRemove event
		internal delegate void RemoveEventHandler( string Alias );
		internal event RemoveEventHandler AliasRemove;		
		
		public AliasesCollection()
		{}

		public AliasesCollection( IList sourceList )
		{
			InnerList.AddRange( sourceList );
		}

		/// <summary>
		/// Adds an alias to the collection.
		/// </summary>
		/// <param name="value">Alias to be added</param>
		/// <returns>The index at which the alias has been added.</returns>
		public int Add( String value )
		{
			return List.Add( value );
		}
		
		/// <summary>
		/// Removes an alias from the collection
		/// </summary>
		/// <param name="value">Alias to be removed</param>
		public void Remove( String value )
		{
			List.Remove( value );
		}	

		/// <summary>Gets the alias at the specified index.</summary>
		public string this[ Int32 index ]
		{
			get
			{
				return  ( string )List[ index ];
			}
		}

		public Int32 IndexOf( string alias )
		{
			return List.IndexOf( alias );
		}

		/// <summary>
		/// Determines whether the collection contains a specific alias.
		/// </summary>
		/// <param name="alias">The alias to locate in the collection.</param>
		/// <returns>True if the specified alias is found in the collection; otherwise, false.</returns>
		public Boolean Contains( string alias )
		{
			return List.Contains( alias );
		}

		protected override void OnInsertComplete( int index, object value )
		{
			RaiseAliasAddEvent( ( string )value );			
		}

		protected override void OnRemoveComplete( int index, object value )
		{
			RaiseAliasRemoveEvent( ( string )value );
		}

		/// <summary>
		/// Raises AliasAdd event.
		/// </summary>
		/// <param name="Alias">New alias.</param>
		private void RaiseAliasAddEvent(string Alias)
		{
			if ( AliasAdd != null )
			{
				AliasAdd( Alias );
			}
		}

		/// <summary>
		/// Raises AliasRemove event.
		/// </summary>
		/// <param name="Alias">Removed alias.</param>
		private void RaiseAliasRemoveEvent( string Alias )
		{
			if ( AliasRemove != null )
			{
				AliasRemove(Alias);
			}
		}
	}	
}
