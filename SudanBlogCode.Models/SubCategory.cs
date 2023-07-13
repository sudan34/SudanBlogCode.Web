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
    public class SubCategory : BaseEntity
    {
        [Required(ErrorMessage = "Sub category name is required.")]
        [DisplayName("Sub Category Name")]
        public string SubCategoryName { get; set; }
        [Required(ErrorMessage = "Slug is required.")]
        public string Slug { get; set; }
        [Required]
        [DisplayName("Status")]
        public bool IsActive { get; set; }
        [DisplayName("Category")]
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
    }
}
