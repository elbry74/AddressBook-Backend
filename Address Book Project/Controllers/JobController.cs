using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Application.JobApp;
using AddressBook.Common.ViewModels;

namespace Address_Book_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IManageJob _jobManager;

        public JobController(IManageJob jobManager)
        {
            _jobManager = jobManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jobviewmodel>> GetJobs()
        {
            var jobs = _jobManager.GetJobs();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public ActionResult<Jobviewmodel> GetJob(int id)
        {
            var job = _jobManager.GetJob(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        [HttpPost]
        public IActionResult AddJob([FromBody] Jobviewmodel model)
        {
            if (ModelState.IsValid)
            {
                var result = _jobManager.AddJob(model);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Failed to add job");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, [FromBody] Jobviewmodel model)
        {
            if (ModelState.IsValid)
            {
                var result = _jobManager.UpdateJob(id, model);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Failed to update job");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(int id)
        {
            var result = _jobManager.DeleteJob(id);

            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Failed to delete job");
            }
        }
    }
}