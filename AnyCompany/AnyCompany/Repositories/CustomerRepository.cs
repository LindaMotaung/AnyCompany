using AnyCompany.EntityClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Repositories
{
    //Using delegates makes the solution resusable by the calling client, extends it's modifiability aspect
    //and allows for it to provide it's custom implementation without modifying existing code/components

    public delegate Customer LoadDataFromDBDelegate(SqlConnection sqlConnection, int customerId, Customer customer);
    public delegate void DeleteCustomerDelegate(SqlConnection sqlConnection, int customerId, Customer customer);
    public delegate void UpdateCustomerDelegate(int customerId, Customer customer);
    public delegate void CreateDelegate(Customer customer);

    public static class CustomerRepository
    {
        private static string ConnectionString = @"Data Source=(local);Database=Customers;User Id=admin;Password=password;";

        public static Customer Load(int customerId, LoadDataFromDBDelegate loadDataDelegate)
        {
            Customer customer = new Customer();
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Making this part generic means that it is reusable
            //This can be called by a method that wants to create, delete, create, update customers, etc

            loadDataDelegate(connection, customerId, customer);
            connection.Close();

            return customer;
        }

        public static void DeleteCustomer(int customerId, DeleteCustomerDelegate deleteCustomerDelegate)
        {
            Customer customer = new Customer();
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            deleteCustomerDelegate(connection, customerId, customer);

            connection.Close();
        }
    }
}
