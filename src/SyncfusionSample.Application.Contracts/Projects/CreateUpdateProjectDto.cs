using System;
using System.ComponentModel.DataAnnotations;

namespace SyncfusionSample.Projects
{
    public class CreateUpdateProjectDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string URL{ get; set; }
    }
}