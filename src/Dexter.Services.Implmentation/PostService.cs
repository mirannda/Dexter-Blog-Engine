﻿#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			PostService.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/About.ashx
// Created:		2012/11/01
// Last edit:	2012/11/02
// License:		GNU Library General Public License (LGPL)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Services.Implmentation
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Common.Logging;

	using Dexter.Data;
	using Dexter.Entities;
	using Dexter.Entities.Filters;
	using Dexter.Entities.Result;
	using Dexter.Extensions.Logging;
	using Dexter.Services.Events;

	public class PostService : IPostService
	{
		#region Fields

		private readonly ILog logger;

		private readonly IPostDataService postDataService;

		#endregion

		#region Constructors and Destructors

		public PostService(IPostDataService postDataService, ILog logger)
		{
			this.postDataService = postDataService;
			this.logger = logger;
		}

		#endregion

		#region Public Events

		public event EventHandler<GenericEventArgs<IList<TagDto>>> TagsRetrievedForPublishedPosts;

		public event EventHandler<CancelEventArgsWithOneParameter<int, IList<TagDto>>> TagsRetrievingForPublishedPosts;

		public event EventHandler<GenericEventArgs<IList<MonthDto>>> MonthsRetrievedForPublishedPosts;

		public event EventHandler<CancelEventArgsWithoutParameters<IList<MonthDto>>> MonthsRetrievingForPublishedPosts;

		public event EventHandler<GenericEventArgs<PostDto>> PostRetrievedById;

		public event EventHandler<GenericEventArgs<PostDto>> PostRetrievedBySlug;

		public event EventHandler<CancelEventArgsWithOneParameter<int, PostDto>> PostRetrievingById;

		public event EventHandler<CancelEventArgsWithOneParameter<string, PostDto>> PostRetrievingBySlug;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedByTag;

		public event EventHandler<GenericEventArgs<IPagedResult<PostDto>>> PostsRetrievedWithFilters;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingBytag;

		public event EventHandler<CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>>> PostsRetrievingWithFilters;

		#endregion

		#region Public Methods and Operators

		public IList<MonthDto> GetMonthsForPublishedPosts()
		{
			CancelEventArgsWithoutParameters<IList<MonthDto>> e = new CancelEventArgsWithoutParameters<IList<MonthDto>>(null);

			this.MonthsRetrievingForPublishedPosts.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			IList<MonthDto> data = this.postDataService.GetMonthsForPublishedPosts();

			this.MonthsRetrievedForPublishedPosts.Raise(this, new GenericEventArgs<IList<MonthDto>>(data));

			return data;
		}

		public Task<IList<MonthDto>> GetMonthsForPublishedPostsAsync()
		{
			return Task.Run(() => this.GetMonthsForPublishedPosts());
		}

		public PostDto GetPostByKey(int key)
		{
			if (key < 1)
			{
				throw new ArgumentException("The Key must be greater than 0", "key");
			}

			CancelEventArgsWithOneParameter<int, PostDto> e = new CancelEventArgsWithOneParameter<int, PostDto>(key, null);

			this.PostRetrievingById.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPostByKey' is overridden by the event 'PostRetrievingById'.");
				return e.Result;
			}

			PostDto result = this.postDataService.GetPostByKey(key);

			this.PostRetrievedById.Raise(this, new GenericEventArgs<PostDto>(result));

			return result;
		}

		public Task<PostDto> GetPostByKeyAsync(int key)
		{
			return Task.Run(() => this.GetPostByKey(key));
		}

		public PostDto GetPostBySlug(string slug)
		{
			if (slug == null)
			{
				throw new ArgumentNullException("slug");
			}

			if (slug == string.Empty)
			{
				throw new ArgumentException("The string must have a value.", "slug");
			}

			CancelEventArgsWithOneParameter<string, PostDto> e = new CancelEventArgsWithOneParameter<string, PostDto>(slug, null);

			this.PostRetrievingBySlug.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'PostRetrievingBySlug' is overridden by the event 'PostRetrievingBySlug'.");
				return e.Result;
			}

			PostDto result = this.postDataService.GetPostBySlug(slug);

			this.PostRetrievedBySlug.Raise(this, new GenericEventArgs<PostDto>(result));

			return result;
		}

		public Task<PostDto> GetPostBySlugAsync(string slug)
		{
			return Task.Run(() => this.GetPostBySlug(slug));
		}

		public IPagedResult<PostDto> GetPosts(int pageIndex, int pageSize, ItemQueryFilter filters)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTime.Now;
				filters.Status = ItemStatus.Published;
			}

			CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, ItemQueryFilter>, IPagedResult<PostDto>>(new Tuple<int, int, ItemQueryFilter>(pageIndex, pageSize, filters), null);

			this.PostsRetrievingWithFilters.Raise(this, e);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPosts' is overridden by the event 'PostsRetrievingWithFilters'.");
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.GetPosts(pageIndex, pageSize, filters);

			this.PostsRetrievedWithFilters.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		public Task<IPagedResult<PostDto>> GetPostsAsync(int pageIndex, int pageSize, ItemQueryFilter filter)
		{
			return Task.Run(() => this.GetPosts(pageIndex, pageSize, filter));
		}

		public IPagedResult<PostDto> GetPostsByTag(int pageIndex, int pageSize, string tag, ItemQueryFilter filters = null)
		{
			if (pageIndex < 1)
			{
				throw new ArgumentException("The page index must be greater than 0", "pageIndex");
			}

			if (pageSize < 1)
			{
				throw new ArgumentException("The page size must be greater than 0", "pageSize");
			}

			if (tag == null)
			{
				throw new ArgumentNullException("tag", "The string tag must contains a value");
			}

			if (tag == string.Empty)
			{
				throw new ArgumentException("The string tag must not be empty", "tag");
			}

			if (filters == null)
			{
				filters = new ItemQueryFilter();
				filters.MaxPublishAt = DateTime.Now;
				filters.Status = ItemStatus.Published;
			}

			CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>> e = new CancelEventArgsWithOneParameter<Tuple<int, int, string, ItemQueryFilter>, IPagedResult<PostDto>>(new Tuple<int, int, string, ItemQueryFilter>(pageIndex, pageSize, tag, filters), null);

			if (e.Cancel)
			{
				this.logger.DebugAsync("The result of the method 'GetPostsByTag' is overridden by the event 'PostsRetrievingBytag'.");
				return e.Result;
			}

			IPagedResult<PostDto> result = this.postDataService.GetPostsByTag(pageIndex, pageSize, tag, filters);

			this.PostsRetrievedByTag.Raise(this, new GenericEventArgs<IPagedResult<PostDto>>(result));

			return result;
		}

		public Task<IPagedResult<PostDto>> GetPostsByTagAsync(int pageIndex, int pageSize, string tag, ItemQueryFilter filters = null)
		{
			return Task.Run(() => this.GetPostsByTag(pageIndex, pageSize, tag, filters));
		}

		public void SaveOrUpdate(PostDto item)
		{
			this.postDataService.SaveOrUpdate(item);
		}

		public IList<TagDto> GetTopTagsForPublishedPosts(int maxNumberOfTags)
		{
			if (maxNumberOfTags < 1)
			{
				throw new ArgumentException("The number of tags to retrieve must be greater than 0", "numberOfTags");
			}

			CancelEventArgsWithOneParameter<int, IList<TagDto>> e = new CancelEventArgsWithOneParameter<int, IList<TagDto>>(maxNumberOfTags, null);

			TagsRetrievingForPublishedPosts.Raise(this, e);

			if (e.Cancel)
			{
				return e.Result;
			}

			var data = this.postDataService.GetTopTagsForPublishedPosts(maxNumberOfTags);

			TagsRetrievedForPublishedPosts.Raise(this, new GenericEventArgs<IList<TagDto>>(data));

			return data;
		}

		public Task<IList<TagDto>> GetTopTagsForPublishedPostsAsync(int maxNumberOfTags)
		{
			return Task.Run(() => this.GetTopTagsForPublishedPosts(maxNumberOfTags));
		}

		#endregion
	}
}