﻿using SudanBlogCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.DataAccess.Repository.IRepository
{
    public interface IBlogImagesRepository : IRepository<BlogImages>
    {
       void Update(BlogImages obj);
    }
}