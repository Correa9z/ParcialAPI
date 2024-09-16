using System.ComponentModel.DataAnnotations;

namespace ParcialAPI.DAL.Entities
{
    public class Ticket
    {
        [Display(Name ="Tiquete")]
        [Required]
        public Guid id { get; set; }

        public DateTime? useDate { get; set; }


        public Boolean isUsed { get; set; }



        public String? entranceGate { get; set; }







    }
}
