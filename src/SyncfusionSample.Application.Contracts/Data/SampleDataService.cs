using System;
using System.Collections.Generic;
using System.Linq;
using SyncfusionSample.Products;
using Volo.Abp.DependencyInjection;

namespace SyncfusionSample.Data
{
	public class SampleDataService : ISingletonDependency
	{
		private List<ProductDto> DataSource = new List<ProductDto>
		{
			new ProductDto
			{
				Id = Guid.NewGuid(),
				Name = "Fan",
				Description="Amazon Basics 3 Speed Small Room Air Circulator Fan, 7-Inch Blade, Black",
				Price = 2500F
			},
			new ProductDto
			{
				Id = Guid.NewGuid(),
				Name = "Redmi smartphone",
				Description="Xiaomi Redmi 9A - Smartphone 2 GB + 32 GB, Dual Sim, Grigio (Granite Grey)",
				Price = 25000F
			},
			
		};

		public List<ProductDto> GetProducts()
		{
			return DataSource;
		}

		public ProductDto GetProduct(Guid id)
		{
			return DataSource.SingleOrDefault(x => x.Id == id);
		}

		public ProductDto CreateProduct(ProductDto input)
		{
			DataSource.Add(new ProductDto
			{
				Id = input.Id,
				Name = input.Name,
				Description = input.Description,
				Price = input.Price,
				
			});

			return input;
		}

		public ProductDto UpdateProduct(ProductDto input)
		{
			DeleteProduct(input);
			CreateProduct(input);

			return input;
		}

		public void DeleteProduct(ProductDto input)
		{
			DataSource.Remove(input);
		}
	}
}