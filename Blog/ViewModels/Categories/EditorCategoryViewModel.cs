using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categories
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 40 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é obrigatório")]
        public string Slug { get; set; }
    }
}
