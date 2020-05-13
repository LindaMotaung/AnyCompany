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

        public void PerformDelete(SqlConnection connection, int customerId)
        {
            string procedureName = "delete from Customer where ID='" + customerId + "'";
            SqlCommand cmd = new SqlCommand(procedureName, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void UpdateCustomer(int customerId, Customer customer)
        {
            UpdateCustomerDelegate delegateInstanceObj = new UpdateCustomerDelegate(PerformUpdate);
            CustomerRepository.UpdateCustomer(customerId, customer ,delegateInstanceObj);
        }

        public void PerformUpdate(SqlConnection connection,int customerId, Customer customer)
        {
            string update = "Update Customer set Name='" + customer.Name + "', DateOfBirth='" + customer.DateOfBirth + "', Country='" + customer.Country + "' where ID= '" + customerId + "'";
            SqlCommand cmd = new SqlCommand(update, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
