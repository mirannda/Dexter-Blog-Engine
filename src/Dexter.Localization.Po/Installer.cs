﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			Installer.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/03/17
// Last edit:	2013/03/17
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Localization.Po
{
	using System;

	using Dexter.Dependency;
	using Dexter.Dependency.Installation;

	public class Installer : ILayerInstaller
	{
		#region Public Methods and Operators

		public void ApplicationStarted(IDexterContainer container)
		{
		}

		public void ServiceRegistration(IDexterContainer container)
		{
			container.Register<ILocalizationProvider, LocalizationProvider>(LifeCycle.Singleton);
		}

		public void ServiceRegistrationComplete(IDexterContainer container)
		{
		}

		#endregion
	}
}