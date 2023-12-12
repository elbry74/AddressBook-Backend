using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressBook.Common.ViewModels;
using Address_Book.Models;
using AddressBook.EF.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace AddressBook.Application.AddressApp
{
    public class ManageAddress : IManageAddress
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManageAddress(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<AddressVM> GetAddressEntries()
        {
            IEnumerable<AddressBooks> allAddresses = _unitOfWork.AddressRepository.GetAllData();
            return _mapper.Map<IEnumerable<AddressVM>>(allAddresses);
        }

        public AddressVM GetAddressEntry(int id)
        {
            AddressBooks address = _unitOfWork.AddressRepository.GetDataById(id);
            return _mapper.Map<AddressVM>(address);
        }

        public bool AddAddress(AddressVM model)
        {
            AddressBooks address = _mapper.Map<AddressBooks>(model);
            // Handle photo upload
            if (!string.IsNullOrEmpty(model.PhotoFileName) && model.Photo != null && model.Photo.Length > 0)
            {
                // Use the same file name for both properties
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_photo.jpg";
                address.PhotoFileName = uniqueFileName;
                address.Photo = model.Photo;
            }
            _unitOfWork.AddressRepository.Add(address);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateAddress(int id, AddressVM model)
        {
            AddressBooks existingAddress = _unitOfWork.AddressRepository.GetDataById(id);

            if (existingAddress == null)
            {
                return false;
            }

            // Handle photo upload only if a new photo is provided
            if (!string.IsNullOrEmpty(model.PhotoFileName) && model.Photo != null && model.Photo.Length > 0)
            {
                // Use the same file name for both properties
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_photo.jpg";
                existingAddress.PhotoFileName = uniqueFileName;
                existingAddress.Photo = model.Photo;
            }

            existingAddress.FullName = model.FullName;
            existingAddress.MobileNumber = model.MobileNumber;
            existingAddress.DateOfBirth = model.DateOfBirth;
            existingAddress.Address = model.Address;
            existingAddress.Email = model.Email;
            existingAddress.Age = model.Age;
            existingAddress.JobId = model.JobId;
            existingAddress.DepartmentId = model.DepartmentId;

            _unitOfWork.Save();

            return true;
        }

        public bool DeleteAddress(int id)
        {
            AddressBooks address = _unitOfWork.AddressRepository.GetDataById(id);
            if (address == null)
                return false;

            _unitOfWork.AddressRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public IEnumerable<AddressVM> Search(string searchTerm)
        {
            IEnumerable<AddressBooks> addresses = _unitOfWork.AddressRepository.GetDataByCondition(a => a.FullName.Contains(searchTerm));
            return _mapper.Map<IEnumerable<AddressVM>>(addresses);
        }

        public IEnumerable<AddressVM> RangeByDate(DateTime? minDateOfBirth, DateTime? maxDateOfBirth)
        {
            IEnumerable<AddressBooks> addresses = _unitOfWork.AddressRepository.GetDataByCondition(a =>
                (!minDateOfBirth.HasValue || a.DateOfBirth >= minDateOfBirth.Value) &&
                (!maxDateOfBirth.HasValue || a.DateOfBirth <= maxDateOfBirth.Value)
            );
            return _mapper.Map<IEnumerable<AddressVM>>(addresses);
        }

        public (MemoryStream stream, bool success) ExportToExcel()
        {
            try
            {
                var addresses = _unitOfWork.AddressRepository.GetAllData();
                var excelPackage = new ExcelPackage();

                var worksheet = excelPackage.Workbook.Worksheets.Add("AddressBookEntries");

                worksheet.Cells["A1"].Value = "Full Name";
                worksheet.Cells["B1"].Value = "Mobile Number";
                worksheet.Cells["C1"].Value = "Date of Birth";
                worksheet.Cells["D1"].Value = "Address";
                worksheet.Cells["E1"].Value = "Email";
                worksheet.Cells["F1"].Value = "Job Title";
                worksheet.Cells["G1"].Value = "Department Name";
                worksheet.Cells["H1"].Value = "Age";

                int row = 2;
                foreach (var address in addresses)
                {
                    var addressVm = _mapper.Map<AddressVM>(address);

                    worksheet.Cells[$"A{row}"].Value = addressVm.FullName;
                    worksheet.Cells[$"B{row}"].Value = addressVm.MobileNumber;
                    worksheet.Cells[$"C{row}"].Value = addressVm.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cells[$"D{row}"].Value = addressVm.Address;
                    worksheet.Cells[$"E{row}"].Value = addressVm.Email;
                    worksheet.Cells[$"F{row}"].Value = addressVm.JobTitle;
                    worksheet.Cells[$"G{row}"].Value = addressVm.DepartmentName;
                    worksheet.Cells[$"H{row}"].Value = addressVm.Age;

                    row++;
                }

                var stream = new MemoryStream();
                excelPackage.SaveAs(stream);

                stream.Position = 0;

                return (stream, true);
            }
            catch (Exception ex)
            {
                return (null, false);
            }
        }
    }
}