using Core.DataAccess;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    interface ICustomerDal:IEntityRepository<Customer>
    {
    }
}
