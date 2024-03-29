﻿using CoreLayer.BaseEntity;

namespace EntityLayer.Blog.Entities
{
    public class Category : BaseEntity
    {
        //category section
        public string Name { get; set; } = null!;

        //article relation
        public ICollection<Article>? Articles { get; set; }
    }
}
