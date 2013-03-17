﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Category.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2012/10/27
// Last edit:	2013/01/20
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.Raven.Domain
{
	public class Category : EntityBase<string>
	{
		#region Public Properties

		public string[] ChildrenIds { get; set; }

		public bool IsDefault { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string ParentId { get; set; }

		public int[] PostsId { get; set; }

		#endregion
	}
}