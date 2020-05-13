using AnyCompany.EntityClasses;
using AnyCompany.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Services
{
    public class CustomerService
    {
        public void DeleteCustomer(int customerId)
        {
            DeleteCustomerDelegate delegateInstanceObj = new DeleteCustomerDelegate(PerformDelete);
            CustomerRepository.DeleteCustomer(customerId, delegateInstanceObj);
        }

        public void PerformDelete(SqlConnection connection, int customerId, Customer customer)
        {
            string procedureName = "delete from Customer where ID='" + customerId + "'";
            SqlCommand cmd = new SqlCommand(procedureName, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
