using System.Collections.Generic;
using Address_Book.Models;
using AddressBook.Common.ViewModels;
using AddressBook.EF.UnitOfWork;
using AutoMapper;

namespace AddressBook.Application.DepartmentApp
{
    public class ManageDepartment : IManageDepartment
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManageDepartment(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool AddDepartment(Departmentviewmodel model)
        {
            Departments department = _mapper.Map<Departments>(model);
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteDepartment(int id)
        {
            _unitOfWork.DepartmentRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateDepartment(int id, Departmentviewmodel model)
        {
            if (id == 0)
            {
                Departments department = _mapper.Map<Departments>(model);
                _unitOfWork.DepartmentRepository.Add(department);
            }
            else
            {
                Departments department = _unitOfWork.DepartmentRepository.GetDataById(id);

                if (department == null)
                {
                    return false;
                }

                department.Name = model.Name;

                _unitOfWork.DepartmentRepository.Modify(department);
            }

            _unitOfWork.Save();
            return true;
        }

        public Departmentviewmodel GetDepartment(int id)
        {
            Departments department = _unitOfWork.DepartmentRepository.GetDataById(id);
            Departmentviewmodel departmentView = _mapper.Map<Departmentviewmodel>(department);
            return departmentView;
        }

        public IEnumerable<Departmentviewmodel> GetAllDepartments()
        {
            IEnumerable<Departments> departments = _unitOfWork.DepartmentRepository.GetAllData();
            IEnumerable<Departmentviewmodel> items = _mapper.Map<IEnumerable<Departmentviewmodel>>(departments);
            return items;
        }
    }
}