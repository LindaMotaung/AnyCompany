using AnyCompany.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Interfaces
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
