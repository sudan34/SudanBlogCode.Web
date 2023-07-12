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
    public class Category : BaseEntity
    {
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        
        [Required]
        [DisplayName("Category Slug")]
        public string CategorySlug { get; set; }
        
        [ValidateNever]
        public string DefautlImageUrl { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}
