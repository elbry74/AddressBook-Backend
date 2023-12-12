using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Common.ViewModels;

namespace AddressBook.Application.DepartmentApp
{
    public interface IManageDepartment
    {
        bool AddDepartment(Departmentviewmodel model);

        bool DeleteDepartment(int id);

        bool UpdateDepartment(int id, Departmentviewmodel model);

        Departmentviewmodel GetDepartment(int id);

        IEnumerable<Departmentviewmodel> GetAllDepartments();
    }
}