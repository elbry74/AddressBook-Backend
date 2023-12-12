using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Common.ViewModels;

namespace AddressBook.Application.Login
{
    public interface IManageRegister
    {
        bool Register(RegisterViewModel model);
    }
}