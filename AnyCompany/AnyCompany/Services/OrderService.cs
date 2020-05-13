using AnyCompany.EntityClasses;
using AnyCompany.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Services
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository = new OrderRepository();

        public bool PlaceOrder(Order order, int customerId)
        {
            LoadDataFromDBDelegate delegateInstanceObj = new LoadDataFromDBDelegate(LoadCustomerFromDB);
            Customer customer = CustomerRepository.Load(customerId, delegateInstanceObj);

            if (order.Amount == 0)
                return false;

            if (customer.Country == "UK")
                order.VAT = 0.2d;
            else
                order.VAT = 0;

            orderRepository.Save(order);

            return true;
        }

        public Customer LoadCustomerFromDB(SqlConnection sqlConnection, int customerId, Customer customer)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = " + customerId,
               sqlConnection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                customer.Name = reader["Name"].ToString();
                customer.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                customer.Country = reader["Country"].ToString();
            }
            return customer;
        }
    }
}
