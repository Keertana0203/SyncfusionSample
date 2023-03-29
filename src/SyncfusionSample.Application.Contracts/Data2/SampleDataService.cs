using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncfusionSample.Products;
using SyncfusionSample.Projects;
using Volo.Abp.DependencyInjection;


namespace SyncfusionSample.Data2
{
    public class SampleDataService : ISingletonDependency
    {
        private List<ProjectDto> DataSource = new List<ProjectDto>()
        {
            new ProjectDto
            {
                Id = Guid.NewGuid(),
                Name= "Project on sensors",
                Description="Accident Prevention Using Eye Blinking and Head Movement",
                URL="https://www.electronicshub.org/sensor-based-projects-ideas/"
            },
             new ProjectDto
            {
                Id = Guid.NewGuid(),
                Name= "Project on chatbot",
                Description="a computer program designed to simulate conversation with human users, especially over the internet:",
                URL="https://cloud.google.com/dialogflow?utm_source=bing&utm_medium=cpc&utm_campaign=japac-IN-all-en-dr-skws-all-super-trial-b-dr-1009882&utm_content=text-ad-none-none-DEV_c-CRE_-ADGP_Hybrid%20%7C%20SKWS%20-%20PHR%20%7C%20Txt%20~%20AI%20%26%20ML%20~%20Dialogflow_Dialogflow-chatbot-KWID_43700071937048970-kwd-74767060169556%3Aloc-90&userloc_143895-network_o&utm_term=KW_chatbot&gclid=7a288609d1d5188cf40fe214b94d5572&gclsrc=3p.ds"
            }
        };
        public List<ProjectDto> GetProjects()
        {
            return DataSource;
        }

        public ProjectDto GetProject(Guid id)
        {
            return DataSource.SingleOrDefault(x => x.Id == id);
        }
        public ProjectDto CreateProject(ProjectDto input)
        {
            DataSource.Add(new ProjectDto
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                URL= input.URL

            });

            return input;
        }
        public ProjectDto UpdateProject(ProjectDto input)
        {
            DeleteProject(input);
            CreateProject(input);

            return input;
        }
        public void DeleteProject(ProjectDto input) 
        {
            DataSource.Remove(input);
        }


    }
}
