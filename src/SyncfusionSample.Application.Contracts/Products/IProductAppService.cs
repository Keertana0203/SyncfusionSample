using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SyncfusionSample.Permissions;
using Volo.Abp.Application.Services;

namespace SyncfusionSample.Products
{
	public interface IProductAppService : IApplicationService
	{
		Task<List<ProductDto>> GetProductListAsync();

		Task<ProductDto> GetProductAsync(Guid id);

		Task<ProductDto> CreateProductAsync(CreateUpdateProductDto input);

		Task<ProductDto> UpdateProductAsync(Guid id, CreateUpdateProductDto input);

		Task DeleteProductAsync(Guid id);

        
    }
}