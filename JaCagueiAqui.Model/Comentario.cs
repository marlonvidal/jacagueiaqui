using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JaCagueiAqui.Model
{
    public class Comentario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [DisplayName("Escreva seu nome:")]
        [UIHint("TextBox")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [RegularExpression(".*@.*", ErrorMessage = "O e-mail digitado está incorreto")]
        [DisplayName("Escreva seu e-mail:")]
        [UIHint("TextBox")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O comentário é obrigatório")]
        [DisplayName("Deixe seu comentário:")]
        [UIHint("TextArea")]
        public string Descricao { get; set; }
    }
}
