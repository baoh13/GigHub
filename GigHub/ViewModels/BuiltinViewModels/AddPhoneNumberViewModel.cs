using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels.BuiltinViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}