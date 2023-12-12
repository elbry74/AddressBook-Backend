using System;
using System.Collections.Generic;
using System.IO;
using AddressBook.Common.ViewModels;

namespace AddressBook.Application.AddressApp
{
    public interface IManageAddress
    {
        IEnumerable<AddressVM> GetAddressEntries();

        AddressVM GetAddressEntry(int id);

        bool AddAddress(AddressVM model);

        bool UpdateAddress(int id, AddressVM model);

        bool DeleteAddress(int id);

        IEnumerable<AddressVM> Search(string searchTerm);

        IEnumerable<AddressVM> RangeByDate(DateTime? minDateOfBirth, DateTime? maxDateOfBirth);

        (MemoryStream stream, bool success) ExportToExcel();
    }
}