using System.ComponentModel.DataAnnotations;

namespace UnternährerDominicLB_295.Model
{
    public class Owner
    {
        public int OwnerId { get; set; }
        
        [Required]
        public string Name { get; set; }

    }
}
