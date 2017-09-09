using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaCagueiAqui.Model
{
    public class Post
    {
        public int ID { get; set; }

        [DisplayName("Escreva seu nome")]
        [UIHint("TextBox")]
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Escreva o nome do local onde fez essa merda")]
        [UIHint("TextBox")]
        [StringLength(50, ErrorMessage = "A descrição deve ter no máximo 50 caracteres!")]
        public string Onde { get; set; }

        [DisplayName("Deixe seu comentário")]
        [UIHint("TextArea")]
        [Required(ErrorMessage = "Detalhe a sua experiência por favor.")]
        public string Comentario { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }

        public int Gostei { get; set; }

        public int NaoGostei { get; set; }

        public int NumeroComentarios { get; set; }
    }
}