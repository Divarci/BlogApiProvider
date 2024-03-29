﻿using Microsoft.AspNetCore.Http;

namespace EntityLayer.Blog.DTOs.ArticleDTOs
{
    public class ArticleUpdateDTO
    {
        //Primary Key
        public int Id { get; set; }

        //Information
        public string? UpdatedDate { get; set; }

        //Check Constraint
        public byte[] RowVersion { get; set; } = null!;
        //Article Section
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? YoutubeUrl { get; set; }


        //Picture Section
        public string FileName { get; set; } = null!;
        public byte[] FileByte { get; set; } = null!;
        public string FileType { get; set; } = null!;

        //Photo Add
        public IFormFile? Photo { get; set; }

        //category relation
        public int CategoryId { get; set; }
    }
}
