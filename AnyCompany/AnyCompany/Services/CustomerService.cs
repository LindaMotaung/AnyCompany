using AnyCompany.EntityClasses;
using AnyCompany.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Services
{
    public class CustomerService
    {
        public int GetCustomerID(string customerName)
        {
            LoadCustomerNameFromDBDelegate delegateInstance = new LoadCustomerNameFromDBDelegate(PerformGetCustomerByID);
            return CustomerRepository.GetCustomerID(customerName, delegateInstance);
        }

        public int PerformGetCustomerByID(SqlConnection connection, string customerName)
        {
            int result = 0;
            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE Name = " + customerName.Trim(),
              connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = Convert.ToInt32(reader["ID"]);
            }
            return result;
        }

        public void DeleteCustomer(int customerId)
        {
            DeleteCustomerDelegate delegateInstanceObj = new DeleteCustomerDelegate(PerformDelete);
            CustomerRepository.DeleteCustomer(customerId, delegateInstanceObj);
        }

        public void PerformDelete(SqlConnection connection, int customerId)
        {
            try
            {
                string procedureName = "delete from Customer where ID='" + customerId + "'";
                SqlCommand cmd = new SqlCommand(procedureName, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
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
