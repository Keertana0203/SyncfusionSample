using System;
using System.ComponentModel.DataAnnotations;

namespace SyncfusionSample.Products
{
	public class CreateUpdateProductDto
	{
		[Required]
		[StringLength(128)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public float Price { get; set; }
	}
}