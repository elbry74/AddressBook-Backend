using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Common.ViewModels;
using AddressBook.Application.AddressApp;
using System.IO;
using System.Linq;

namespace Address_Book_Project.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IManageAddress _addressManager;

        public AddressController(IManageAddress addressManager)
        {
            _addressManager = addressManager;
        }

        // GET api/address
        [HttpGet]
        public IActionResult GetAddressEntries()
        {
            var addressEntries = _addressManager.GetAddressEntries();
            return Ok(addressEntries);
        }

        // GET api/address/5
        [HttpGet("{id}")]
        public IActionResult GetAddressEntry(int id)
        {
            var addressEntry = _addressManager.GetAddressEntry(id);
            if (addressEntry == null)
            {
                return NotFound();
            }
            return Ok(addressEntry);
        }

        // POST api/address
        [HttpPost]
        public IActionResult Post([FromBody] AddressVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _addressManager.AddAddress(model);

            return CreatedAtAction(nameof(GetAddressEntry), new { id = model.Id }, model);
        }

        // PUT api/address/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AddressVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _addressManager.UpdateAddress(id, model);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/address/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _addressManager.DeleteAddress(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("search")]
        public IActionResult Search(string searchTerm)
        {
            try
            {
                var addresses = _addressManager.Search(searchTerm);

                if (addresses.Any())
                {
                    return Ok(addresses);
                }
                else
                {
                    return NotFound($"No addresses found containing '{searchTerm}'.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET api/address/rangeByDate
        [HttpGet("rangeByDate")]
        public IActionResult RangeByDate(DateTime? minDateOfBirth, DateTime? maxDateOfBirth)
        {
            try
            {
                var addresses = _addressManager.RangeByDate(minDateOfBirth, maxDateOfBirth);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var (stream, success) = _addressManager.ExportToExcel();

                if (success)
                {
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AddressBookEntries.xlsx");
                }

                // Handle export failure
                return StatusCode(500, "Export failed");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Export failed: {ex.Message}");
            }
        }
    }
}