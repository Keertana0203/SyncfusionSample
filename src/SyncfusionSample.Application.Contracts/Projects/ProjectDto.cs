using System;
using Volo.Abp.Application.Dtos;

namespace SyncfusionSample.Projects

{
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }
    }
}