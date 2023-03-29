using System;
using Volo.Abp.Application.Dtos;

namespace SyncfusionSample.Products

{
	public class ProductDto : AuditedEntityDto<Guid>
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public float Price { get; set; }
	}
}