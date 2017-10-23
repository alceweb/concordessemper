using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcordesSemper.Models
{
    public class MainModels
    {
    }

    public class Case
    {
        [Key]
        public int Casa_Id { get; set; }
        [Display(Name = ("Nome casa"))]
        public string Nome { get; set; }

        public virtual ICollection<Punteggi> Punteggis { get; set; }
        public virtual ICollection<Alunni> Alunnis { get; set; }
    }


    public class Punteggi
    {
        [Key]
        public int Punteggio_Id { get; set; }
        public int Casa_Id { get; set; }
        public virtual Case Nome { get; set; }
        [Display(Name = ("Data assegnazione"))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [Display(Name = ("Punti"))]
        public int Punti { get; set; }
        public string Motivazione { get; set; }
        public int Comportamento { get; set; }
        public int Dimenticanze { get; set; }
        public int Varie { get; set; }
        public int GrandiG { get; set; }
        public int OeP { get; set; }
        public string Insegnante { get; set; }

    }

    public class Alunni
    {
        [Key]
        public int Alunno_Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Casa_Id { get; set; }
        public virtual Case Casa { get; set; }
    }

    public class Incarichi {
        [Key]
        public int Incarico_Id { get; set; }
        public string Incarico { get; set; }
    }

    public class Quesiti
    {
        [Key]
        public int Quesito_Id { get; set; }
        public string Prof { get; set; }
        [Required]
        [Display(Name ="Valore")]
        public int Valore { get; set; }
        [Required]
        [Display(Name ="Prima domanda")]
        public string Domanda_1 { get; set; }
        [Required]
        [Display(Name = "Prima risposta")]
        public string Risposta_1 { get; set; }
        [Required]
        [Display(Name = "Seconda domanda")]
        public string Domanda_2 { get; set; }
        [Required]
        [Display(Name = "Seconda risposta")]
        public string Risposta_2 { get; set; }
        [Required]
        [Display(Name = "Terza domanda")]
        public string Domanda_3 { get; set; }
        [Required]
        [Display(Name = "Terza risposta")]
        public string Risposta_3 { get; set; }
        [Required]
        [Display(Name = "Quarta domanda")]
        public string Domanda_4 { get; set; }
        [Required]
        [Display(Name = "Quarta risposta")]
        public string Risposta_4 { get; set; }
        [Required]
        [Display(Name = "Quinta domanda")]
        public string Domanda_5 { get; set; }
        [Required]
        [Display(Name = "Quinta risposta")]
        public string Risposta_5 { get; set; }
        [Display(Name ="Raola chiave")]
        public string ParolaChiave { get; set; }
        [Display(Name ="Pubblica")]
        public bool Pubblica { get; set; }
        [Display(Name = "Data publicazione")]
        public DateTime DataI { get; set; }
        [Display(Name = "Data risoluzione")]
        public DateTime DataF { get; set; }
        [Display(Name = "Casa che ha risolto")]
        public int Casa_Id { get; set; }
        [Display(Name = "Cognmome")]
        public string Cognome { get; set; }
        [Display(Name = "Nome")]
        public string NomeAlunno { get; set; }

    }
}