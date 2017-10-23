using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcordesSemper.Models
{
    public class AssegnaViewModels
    {
    }

    public class AssegnaUserViewModel
    {
        public int Quesito_Id { get; set; }
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