using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Bu satırı ekleyin

namespace Blog.Core.Models
{
    public class Category : BaseModel
    {
        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Açıklama gereklidir.")]
        public string Description { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
