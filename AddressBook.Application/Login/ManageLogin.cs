using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address_Book.Models;
using AddressBook.Common.ViewModels;
using AddressBook.EF.GenericRepository;
using AddressBook.EF.UnitOfWork;
using AutoMapper;

namespace AddressBook.Application.Login
{
    public class ManageLogin : IManageLogin
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManageLogin(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Login(LoginViewModel model)
        {
            DB.Models.Login login = _unitOfWork.LoginRepository.GetDataByCondition(l => l.Email == model.Email && l.Password == model.Password).FirstOrDefault();
            return login != null;
        }
    }
}