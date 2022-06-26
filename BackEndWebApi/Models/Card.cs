using System.ComponentModel.DataAnnotations;

namespace BackEndWebApi.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string NomeCard { get; set; }
        public string NumeroCard { get; set; } 
        public int MesDeVencimento { get; set; }
        public int AnoDeVencimento { get; set; }
        public int CVC { get; set; }
    }
}
