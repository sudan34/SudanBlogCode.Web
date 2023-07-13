using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.Models
{
    public class BlogImages:BaseEntity
    {
        [Required]
        [DisplayName("Blog Title")]
        public string BlogTitle { get; set; }
        [DisplayName("Image Url")]

        [ValidateNever]
        public string ImageUrl { get; set;}
        [Required]
        [DisplayName("SubCategory")]
        public int SubCategoryId { get; set;}
        [ValidateNever]

        public SubCategory SubCategory { get; set;}

    }
}
