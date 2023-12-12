using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Common.ViewModels;

namespace AddressBook.Application.JobApp
{
    public interface IManageJob
    {
        bool AddJob(Jobviewmodel model);

        bool DeleteJob(int id);

        bool UpdateJob(int id, Jobviewmodel model);

        Jobviewmodel GetJob(int id);

        IEnumerable<Jobviewmodel> GetJobs();
    }
}