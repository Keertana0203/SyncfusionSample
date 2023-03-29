using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SyncfusionSample.Products;
using SyncfusionSample.Projects;
namespace SyncfusionSample.Blazor.Pages
{
    public partial class Projects
    {
        [Inject]
        private IProjectAppService ProjectAppService { get; set; }

        private IReadOnlyList<ProjectDto> ProjectList { get; set; }
        private CreateUpdateProjectDto NewProjectDto { get; set; }
        private CreateUpdateProjectDto EditingProjectDto { get; set; }
        private Guid EditingProjectId { get; set; }
        private bool LoadingProjects { get; set; }
        private bool NewDialogOpenProject { get; set; }
        private bool EditingDialogOpenProject { get; set; }
        public Projects()
        {
            ProjectList = new List<ProjectDto>();
            NewProjectDto = new CreateUpdateProjectDto();
            EditingProjectDto = new CreateUpdateProjectDto();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetProjectsAsync();
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task GetProjectsAsync()
        {
            try
            {
                LoadingProjects = true;

                await InvokeAsync(() => StateHasChanged());

                ProjectList = await ProjectAppService.GetListAsync();
            }
            finally
            {
                LoadingProjects = false;

                await InvokeAsync(() => StateHasChanged());
            }
        }
        private Task OpenCreateProjectModalAsync()
        {
            NewDialogOpenProject = true;
            NewProjectDto = new CreateUpdateProjectDto();

            return Task.CompletedTask;
        }
        private async Task CreateProjectAsync()
        {
            try
            {
                await ProjectAppService.CreateAsync(NewProjectDto);

                await GetProjectsAsync();
            }
            finally
            {
                NewDialogOpenProject = false;
            }
        }
        private Task OpenEditingProjectModalAsync(ProjectDto project)
        {
            EditingDialogOpenProject = true;
            EditingProjectId = project.Id;
            EditingProjectDto = new CreateUpdateProjectDto
            {
                Name = project.Name,
                Description = project.Description,
                URL = project.URL
            };

            return Task.CompletedTask;
        }

        private async Task UpdateProjectAsync()
        {
            try
            {
                await ProjectAppService.UpdateAsync(EditingProjectId, EditingProjectDto);
            }
            finally
            {
                EditingDialogOpenProject = false;

                await GetProjectsAsync();
            }
        }
        private async Task DeleteProjectAsync(ProjectDto project)
        {
            var confirmMessage = L["ProjectDeletionConfirmationMessage", project.Name];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await ProjectAppService.DeleteAsync(project.Id);
            await GetProjectsAsync();
        }

    }
}