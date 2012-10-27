﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			CommentDto.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/10/27
// Last edit:	2012/10/27
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Dexter.Data.DataTransferObjects
{
	using System;
	using System.Net.Mail;

	public class CommentDto
	{
		#region Public Properties

		public string Author { get; set; }

		public string Body { get; set; }

		public virtual MailAddress Email { get; set; }

		public virtual Uri WebSite { get; set; }

		#endregion
	}
}