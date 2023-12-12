using System.Collections.Generic;
using Address_Book.Models;
using AddressBook.Application.JobApp;
using AddressBook.Common.ViewModels;
using AddressBook.EF.UnitOfWork;
using AutoMapper;

namespace AddressBook.Application.JobApp
{
    public class ManageJob : IManageJob
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManageJob(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool AddJob(Jobviewmodel model)
        {
            Jobs job = _mapper.Map<Jobs>(model);
            _unitOfWork.JobRepository.Add(job);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteJob(int id)
        {
            _unitOfWork.JobRepository.Delete(id);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateJob(int id, Jobviewmodel model)
        {
            if (id == 0)
            {
                Jobs job = _mapper.Map<Jobs>(model);
                _unitOfWork.JobRepository.Add(job);
            }
            else
            {
                Jobs job = _unitOfWork.JobRepository.GetDataById(id);

                if (job == null)
                {
                    return false;
                }

                job.Title = model.Title;

                _unitOfWork.JobRepository.Modify(job);
            }

            _unitOfWork.Save();
            return true;
        }

        public Jobviewmodel GetJob(int id)
        {
            Jobs job = _unitOfWork.JobRepository.GetDataById(id);
            Jobviewmodel jobview = _mapper.Map<Jobviewmodel>(job);
            return jobview;
        }

        public IEnumerable<Jobviewmodel> GetJobs()
        {
            IEnumerable<Jobs> Jobs = _unitOfWork.JobRepository.GetAllData();
            IEnumerable<Jobviewmodel> items = _mapper.Map<IEnumerable<Jobviewmodel>>(Jobs);
            return items;
        }
    }
}