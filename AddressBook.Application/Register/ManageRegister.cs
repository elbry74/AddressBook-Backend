using System.Linq;
using AddressBook.Common.ViewModels;
using AddressBook.DB.Models;
using AddressBook.EF.UnitOfWork;
using AutoMapper;

namespace AddressBook.Application.Login
{
    public class ManageRegister : IManageRegister
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManageRegister(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool Register(RegisterViewModel model)
        {
            if (_unitOfWork.UserRepository.GetDataByCondition(u => u.Email == model.Email).Any())
            {
                return false;
            }

            Register userEntity = _mapper.Map<Register>(model);

            _unitOfWork.UserRepository.Add(userEntity);
            _unitOfWork.Save();

            return true;
        }
    }
}