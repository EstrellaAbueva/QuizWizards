using System.ComponentModel.DataAnnotations;

namespace QuizApi.Dtos.Taker
{
    public class TakerCreationDto
    {
        [Required(ErrorMessage = "Taker name is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the name of a taker is 50 characters.")]
        [RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Characters include (A-Z) (a-z) (' space -)")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Taker address is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the address of a taker is 50 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Taker email is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the email of a taker is 50 characters.")]
        [RegularExpression("[a-z._0-9]+@[a-z]+\\.[a-z]{2,3}", ErrorMessage = "Email invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Taker username is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the username of a taker is 50 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Taker password is required.")]
        [MaxLength(50, ErrorMessage = "Maximum lenghth for the password of a taker is 50 characters.")]
        public string? Password { get; set; }
    }
}
