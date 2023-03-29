using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using SyncfusionSample.Permissions;
using SyncfusionSample.Products;

namespace SyncfusionSample.Blazor.Pages
{
	public partial class Products
	{
		[Inject]
		private IProductAppService ProductAppService { get; set; }

		private IReadOnlyList<ProductDto> ProductList { get; set; }
		private CreateUpdateProductDto NewProductDto { get; set; }
		private CreateUpdateProductDto EditingProductDto { get; set; }
		private Guid EditingProductId { get; set; }
		private bool Loading { get; set; }
		private bool NewDialogOpen { get; set; }
		private bool EditingDialogOpen { get; set; }
		private bool CanCreateProduct { get; set; }
		private bool CanDeleteProduct { get; set; }
		private bool CanEditProduct { get; set; }

		public Products()
		{
			ProductList = new List<ProductDto>();
			NewProductDto = new CreateUpdateProductDto();
			EditingProductDto = new CreateUpdateProductDto();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await SetPermissionsAsync();

                await GetProductsAsync();
			}

			await base.OnAfterRenderAsync(firstRender);
		}
        private async Task SetPermissionsAsync()
        {
            CanCreateProduct = await AuthorizationService
                .IsGrantedAsync(SyncfusionSamplePermissions.Products.Create);

            CanEditProduct = await AuthorizationService
                .IsGrantedAsync(SyncfusionSamplePermissions.Products.Edit);

            CanDeleteProduct = await AuthorizationService
                .IsGrantedAsync(SyncfusionSamplePermissions.Products.Delete);
        }
        private async Task GetProductsAsync()
		{
			try
			{
				Loading = true;

				await InvokeAsync(() => StateHasChanged());

				ProductList = await ProductAppService.GetProductListAsync();
			}
			finally
			{
				Loading = false;

				await InvokeAsync(() => StateHasChanged());
			}
		}

		private Task OpenCreateProductModalAsync()
		{
			NewDialogOpen = true;
			NewProductDto = new CreateUpdateProductDto();

			return Task.CompletedTask;
		}

		private async Task CreateAsync()
		{
			try
			{
				await ProductAppService.CreateProductAsync(NewProductDto);

				await GetProductsAsync();
			}
			finally
			{
				NewDialogOpen = false;
			}
		}

		private Task OpenEditingProductModalAsync(ProductDto product)
		{
			EditingDialogOpen = true;
			EditingProductId = product.Id;
			EditingProductDto = new CreateUpdateProductDto
			{
				Name = product.Name,
				Description= product.Description,
				Price = product.Price,
				
			};

			return Task.CompletedTask;
		}

		private async Task UpdateAsync()
		{
			try
			{
				await ProductAppService.UpdateProductAsync(EditingProductId, EditingProductDto);
			}
			finally
			{
				EditingDialogOpen = false;

				await GetProductsAsync();
			}
		}

		private async Task DeleteAsync(ProductDto product)
		{
			var confirmMessage = L["ProductDeletionConfirmationMessage", product.Name];
			if (!await Message.Confirm(confirmMessage))
			{
				return;
			}

			await ProductAppService.DeleteProductAsync(product.Id);
			await GetProductsAsync();
		}
	}
}