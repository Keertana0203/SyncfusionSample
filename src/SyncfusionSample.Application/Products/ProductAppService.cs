using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncfusionSample.Data;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using SyncfusionSample.Permissions;
using Volo.Abp.Application.Dtos;


namespace SyncfusionSample.Products
{
	public class ProductAppService : CrudAppService<
        Product, 
        ProductDto, 
        Guid, 
        PagedAndSortedResultRequestDto, 
        CreateUpdateProductDto> , IProductAppService
	{
		private readonly SampleDataService _sampleBookDataService;
        public ProductAppService(IRepository<Product, Guid> repository,SampleDataService sampleBookDataService)
			: base(repository)
        {

            GetPolicyName = SyncfusionSamplePermissions.Products.Default;
            GetListPolicyName = SyncfusionSamplePermissions.Products.Default;
            CreatePolicyName = SyncfusionSamplePermissions.Products.Create;
            UpdatePolicyName = SyncfusionSamplePermissions.Products.Edit;
            DeletePolicyName = SyncfusionSamplePermissions.Products.Delete;
            _sampleBookDataService = sampleBookDataService;
		}

		public async Task<List<ProductDto>> GetProductListAsync()
		{
			return await Task.Run(() => _sampleBookDataService.GetProducts());
		}

		public async Task<ProductDto> GetProductAsync(Guid id)
		{
			var book = await Task.Run(() => _sampleBookDataService.GetProduct(id));

			return book;
		}

		public async Task<ProductDto> CreateProductAsync(CreateUpdateProductDto input)
		{
			var newBook = new ProductDto
			{
				Id = GuidGenerator.Create(),
				Name = input.Name,
				Description= input.Description,
				Price = input.Price
			};

			newBook = await Task.Run(() => _sampleBookDataService.CreateProduct(newBook));

			return newBook;
		}

		public async Task<ProductDto> UpdateProductAsync(Guid id, CreateUpdateProductDto input)
		{
			var book = await Task.Run(() => _sampleBookDataService.GetProduct(id));

			book.Name = input.Name;
			book.Description = input.Description;
			book.Price = input.Price;
			

			book = await Task.Run(() => _sampleBookDataService.UpdateProduct(book));

			return book;
		}

		public async Task DeleteProductAsync(Guid id)
		{
			var book = await Task.Run(() => _sampleBookDataService.GetProduct(id));

			await Task.Run(() => _sampleBookDataService.DeleteProduct(book));
		}
       

    }
	
}