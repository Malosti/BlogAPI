﻿using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context, [FromQuery] int page = 0, [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context.Posts.AsNoTracking().Include(x => x.Category).Include(x => x.Author)
                    .Select(x => new ListPostViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name}, ({x.Author.Email})"
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }
        }
        
        [HttpGet("v1/posts/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var posts = await context.Posts.AsNoTracking().Include(x => x.Author).ThenInclude(x=>x.Roles).Include(x=>x.Category).FirstOrDefaultAsync(x=> x.Id == id);

                if (posts == null)
                    return NotFound(new ResultViewModel<Post>("Conteudo não encontrado"));

                return Ok(new ResultViewModel<dynamic>(posts));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Post>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/posts/category/{category}")]
        public async Task<IActionResult> GetByCategoryAsync([FromServices] BlogDataContext context, [FromRoute] string category, [FromQuery] int page = 0, [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context.Posts.AsNoTracking().Include(x => x.Category).Include(x => x.Author).Where(x=> x.Category.Slug == category)
                    .Select(x => new ListPostViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name}, ({x.Author.Email})"
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }
        }

    }
}
