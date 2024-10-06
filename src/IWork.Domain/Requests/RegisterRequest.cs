using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Nome completo é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome completo deve ter no máximo 100 caracteres.")]
        public string CompleteName { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [StringLength(50, ErrorMessage = "Nome do usuário deve ter no máximo 50 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "CPF deve ter no máximo 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Número de Telefone é obrigatório.")]
        [StringLength(15, ErrorMessage = "Número de telefone deve ter no máximo 15 caracteres.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "A senha deve ter no mínimo 12 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória.")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O papel (role) do usuário é obrigatório.")]
        [RegularExpression("Admin|User", ErrorMessage = "Role deve ser 'Admin' ou 'User'.")]
        public string Role { get; set; }
        public bool IsActive { get; set; } = true; 
    }
}
