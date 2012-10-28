﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			IUrlBuilder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/28
// Last edit:	2012/10/28
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Navigation.Contracts
{
	using System;
	using System.Web;

	using Dexter.Navigation.Helpers;

	public interface IUrlBuilder
	{
		#region Public Properties

		SiteUrl Home { get; }

		IAdminUrlBuilder Admin { get; }

		#endregion

		#region Public Methods and Operators

		SiteUrl CurrentUrl(HttpContextWrapper request);

		#endregion
	}
}