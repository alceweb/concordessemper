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

}