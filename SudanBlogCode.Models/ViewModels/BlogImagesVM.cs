using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.Models.ViewModels
{
    public class BlogImagesVM
    {
        public BlogImages BlogImages { get; set; }   
        public IEnumerable<SelectListItem> SubCategoryList { get; set; }
        
        [ValidateNever]
        public List<IFormFile>? Files { get; set; }
    }
}
