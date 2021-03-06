﻿using BookWarm.Helpers;
using BookWarm.Models;
using BookWarm.Models.Comments;
using BookWarm.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWarm.Data.Repository
{
	public class Repository : IRepository
	{
		private AppDbContext _ctx;

		public Repository(AppDbContext ctx)
		{
			_ctx = ctx;
		}

		public void AddPost(Post post)
		{
			_ctx.Posts.Add(post);
		}

		public List<Post> GetAllPosts()
		{
			return _ctx.Posts.ToList();
		}

		public IndexViewModel GetAllPosts(int pageNumber, string category, string search)
		{
			//Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); }; --doesnt work :(
			int pageSize = 2;
			int skipAmount = pageSize * (pageNumber - 1);
			int capacity = skipAmount + pageSize;

			var query = _ctx.Posts.AsNoTracking().AsQueryable();

			if (!String.IsNullOrEmpty(category))
				query = query.Where(x => x.Category.ToLower().Equals(category.ToLower()));

			if (!String.IsNullOrEmpty(search))
				query = query.Where(x => EF.Functions.Like(x.Title + x.Body + x.Description, $"%{search}%"));

			int postsCount = query.Count();

			return new IndexViewModel
			{
				Posts = query
					.Skip(skipAmount) //paging - 2 posts for one page and then next page // multiple here is to know in which page we are now
					.Take(pageSize)
					.ToList(),
				PageNumber = pageNumber,
				PageCount = (int)Math.Ceiling((double)postsCount / pageSize),
				NextPage = postsCount > capacity,
				Pages = PageHelper.PageNumbers(pageNumber, (int)Math.Ceiling((double)postsCount / pageSize)).ToList(),
				Category = category,
				Search = search
			};
		} 

		public Post GetPost(int id)
		{
			return _ctx.Posts
				.Include(p => p.MainComments)
					.ThenInclude(p=>p.SubComments) //looks in MainComments
				.FirstOrDefault(m=> m.Id==id);
		}

		public void RemovePost(int id)
		{
			_ctx.Posts.Remove(GetPost(id));
		}

		public void UpdatePost(Post post)
		{
			_ctx.Posts.Update(post);
		}
		public async Task<bool> SaveChangesAsync()
		{
			if (await _ctx.SaveChangesAsync() > 0)
			{
				return true;
			}
			return false;
		}

		public void AddSubComment(SubComment comment)
		{
			_ctx.SubComments.Add(comment);
		}
	}
}
