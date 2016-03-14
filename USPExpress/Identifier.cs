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
	/// <summary>Base interface for all identifiers</summary>
	public interface IIdentifier
	{
		/// <summary>Name of the identifier.</summary>
		string Name{get;}
	}

	/// <summary>Base interface for all typed identifiers</summary>
	public interface ITypedIdentifier:IIdentifier
	{
		/// <summary>Type of the identifier.</summary>
		Type Type{get;}
	}
}
