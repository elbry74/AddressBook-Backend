using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Application.DepartmentApp;
using AddressBook.Common.ViewModels;

namespace Address_Book_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IManageDepartment _departmentManager;

        public DepartmentController(IManageDepartment departmentManager)
        {
            _departmentManager = departmentManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Departmentviewmodel>> GetAllDepartments()
        {
            var departments = _departmentManager.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public ActionResult<Departmentviewmodel> GetDepartment(int id)
        {
            var department = _departmentManager.GetDepartment(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Departmentviewmodel model)
        {
            if (ModelState.IsValid)
            {
                var result = _departmentManager.AddDepartment(model);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Failed to add department");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] Departmentviewmodel model)
        {
            if (ModelState.IsValid)
            {
                var result = _departmentManager.UpdateDepartment(id, model);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Failed to update department");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var result = _departmentManager.DeleteDepartment(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Failed to delete department");
            }
        }
    }
}