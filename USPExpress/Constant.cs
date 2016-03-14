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
	/// A base abstract class for any constant. 
	/// You must inherit from this class in order to add a user-defined constant to the Constants collection.
	/// </summary>
	public abstract class Constant: ITypedIdentifier
	{		
		/// <summary>Syntax</summary>
		public virtual string Syntax
		{
			get {return "UNDEFINED";}
		}

		/// <summary>Description</summary>
		public virtual string Description
		{
			get {return "UNDEFINED";}
		}

		/// <summary>Group type</summary>
		public virtual Constants.GroupType Group
		{
			get {return Constants.GroupType.Custom;}
		}	

		/// <summary>Returns the value of the constant. Read-only.</summary>
		public abstract object Value{get;}
	
		/// <summary>Returns the name of the constant. Read-only.</summary>
		public abstract string Name{get;}

		/// <summary>Type of the Constant. Read-only.</summary>
		public Type Type{get{return Value.GetType();}}

	}
}
