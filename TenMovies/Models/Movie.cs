using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TenMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Название фильма")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Режисер")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Год")]
        [Remote(action: "CheckYear", controller: "Home", ErrorMessage = "Год должен быть больше 1894 и не больше текущего года!")]
        public int Year { get; set; }

        public string? Poster { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения!")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Длина строки должна быть от 50 до 1000 символов")]
        [Display(Name = "Описание фильма")]
        public string Description { get; set; }

    }
}
