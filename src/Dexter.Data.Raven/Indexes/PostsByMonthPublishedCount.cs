﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostsByMonthPublishedCount.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/02
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Data.Raven.Indexes
{
	using System.Linq;

	using Dexter.Data.Raven.Domain;
	using Dexter.Entities;

	using global::Raven.Abstractions.Indexing;

	using global::Raven.Client.Indexes;

	public class PostsByMonthPublishedCount : AbstractIndexCreationTask<Post, MonthDto>
	{
		#region Constructors and Destructors

		public PostsByMonthPublishedCount()
		{
			this.Map = posts => posts.Where(x => x.Status == ItemStatus.Published).Select(post => new
				                                                                                      {
					                                                                                      post.PublishAt.Year, 
					                                                                                      post.PublishAt.Month, 
					                                                                                      Count = 1
				                                                                                      });

			this.Reduce = results => results.GroupBy(result => new
				                                                   {
					                                                   result.Year, 
					                                                   result.Month, 
				                                                   }).Select(g => new
					                                                                  {
						                                                                  g.Key.Year, 
						                                                                  g.Key.Month, 
						                                                                  Count = g.Sum(x => x.Count)
					                                                                  });

			this.Sort(x => x.Month, SortOptions.Int);
			this.Sort(x => x.Year, SortOptions.Int);
		}

		#endregion
	}
}