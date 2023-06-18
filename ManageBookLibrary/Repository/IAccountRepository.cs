﻿using ManageBookLibrary.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageBookLibrary.Repository
{
    public interface IAccountRepository
    {
        Account GetAccountByEmailAndPass(Account account);
        void UpdateAccount(Account account);
        void InsertAccount(Account account);

    }
}