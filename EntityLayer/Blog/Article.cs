﻿using CoreLayer;

namespace EntityLayer.Blog
{
    public class Article : BaseEntity
    {
        //Article Section
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;

        //Picture Section
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;

    }
}
