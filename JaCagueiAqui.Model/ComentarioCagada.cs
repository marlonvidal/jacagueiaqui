using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JaCagueiAqui.Model
{
    public class ComentarioCagada
    {
        public int ID { get; set; }

        [DisplayName("Escreva seu nome")]
        [UIHint("TextBox")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Deixe aqui seu comentário")]
        [UIHint("TextArea")]
        [Required(ErrorMessage = "O comentário é obrigatório")]
        public string Comentario { get; set; }
        
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
