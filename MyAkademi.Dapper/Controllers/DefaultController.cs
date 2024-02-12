using Dapper;
using Microsoft.AspNetCore.Mvc;
using MyAkademi.Dapper.DapperContext;
using MyAkademi.Dapper.Dtos;
using System.Collections.Immutable;

namespace MyAkademi.Dapper.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string query = "Select * from Project";
            var connection=_context.CreateConnection();
            var values = await connection.QueryAsync<ResultProjectDto>(query);

            return View(values.ToList());
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto createProjectDto)
        {
            string query = "insert into Project (Title,Description,ProjectCategory,CompleteDay,Price) Values (@title,@description,@projectCategory,@completeDay,@price)";
            var parameters = new DynamicParameters();
            parameters.Add("@title", createProjectDto.Title);
            parameters.Add("@description", createProjectDto.Description);
            parameters.Add("@projectCategory", createProjectDto.ProjectCategory);
            parameters.Add("@completeDay", createProjectDto.CompleteDay);
            parameters.Add("@price", createProjectDto.Price);

            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,parameters);
            return RedirectToAction("Index");   

        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            string query = "Delete from Project where ProjectID=@ProjectID";
            var paramaters=new DynamicParameters();
            paramaters.Add("@ProjectID", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query,paramaters);
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult> UpdateProject(int id)
        {

            string query = $"select * from Project where ProjectId={id}";
            var connection=_context.CreateConnection();
            var values=await connection.QueryFirstAsync<UpdateProjectDto>(query);
            return View(values);

     
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(UpdateProjectDto updateProjectDto)
        {
            string query = "Update Project Set Title=@title, Description=@description,ProjectCategory=@projectcategory,CompleteDay=@completeday,Price=@price where ProjectId=@projectid";
            var paramaters = new DynamicParameters();
            paramaters.Add("@projectid", updateProjectDto.ProjectId);
            paramaters.Add("@description", updateProjectDto.Description);
            paramaters.Add("@projectcategory", updateProjectDto.ProjectCategory);
            paramaters.Add("@completeday", updateProjectDto.CompleteDay);
            paramaters.Add("@title", updateProjectDto.Title);
            paramaters.Add("@price", updateProjectDto.Price);
             var connection=_context.CreateConnection();
            await connection.ExecuteAsync(query, paramaters);
            return RedirectToAction("Index");   
            
        }
    }
}
