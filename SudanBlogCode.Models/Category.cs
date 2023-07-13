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
        [Required(ErrorMessage ="Category Name is required")]
        [DisplayName("Category Name")]
        [RegularExpression("^[a-zA-Z0-9]\\S+$", ErrorMessage ="Catergory name should be alphanumeric")]
        public string CategoryName { get; set; }
        
        [Required(ErrorMessage = "Category Slug is required")]
        [DisplayName("Category Slug")]
        public string CategorySlug { get; set; }
        
        [ValidateNever]
        [DisplayName("Defautl Image Url")]
        public string DefautlImageUrl { get; set; }
        
        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Status is required")]
        [DisplayName("Status")]
        public bool IsActive { get; set; }
    }
}
