using Address_Book.Models;
using System;

namespace AddressBook.EF.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly MyDbContext _dbContext;
        private readonly object lockPad = new object();

        private AddressRepository.AddressRepository _addressRepository;
        private DepartmentRepository.DepartmentRepository _departmentRepository;
        private JobRepository.JobRepository _jobRepository;
        private LoginRepository.LoginRepository _loginRepository;
        private RegisterRepository.UserRepository _userRepository;

        public UnitOfWork(MyDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public AddressRepository.AddressRepository AddressRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (_addressRepository == null)
                        _addressRepository = new AddressRepository.AddressRepository(_dbContext);
                }
                return _addressRepository;
            }
        }

        public JobRepository.JobRepository JobRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (_jobRepository == null)
                        _jobRepository = new JobRepository.JobRepository(_dbContext);
                }
                return _jobRepository;
            }
        }

        public DepartmentRepository.DepartmentRepository DepartmentRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (_departmentRepository == null)
                        _departmentRepository = new DepartmentRepository.DepartmentRepository(_dbContext);
                }
                return _departmentRepository;
            }
        }

        public LoginRepository.LoginRepository LoginRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (_loginRepository == null)
                        _loginRepository = new LoginRepository.LoginRepository(_dbContext);
                }
                return _loginRepository;
            }
        }

        public RegisterRepository.UserRepository UserRepository
        {
            get
            {
                lock (lockPad)
                {
                    if (_userRepository == null)
                        _userRepository = new RegisterRepository.UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}