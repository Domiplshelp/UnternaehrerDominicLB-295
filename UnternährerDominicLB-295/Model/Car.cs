using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace UnternährerDominicLB_295.Model
{
    public class Car
    {
        public int CarId { get; set; }
        [Required] 
        public string Name { get; set; }
        public int ps { get; set; }

    }
}
