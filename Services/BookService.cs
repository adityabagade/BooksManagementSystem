using BooksManagementSystem.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;

namespace BooksManagementSystem.Services
{
    

        // This Is Interface 
        public interface IBookService
        {
            List<Books> GetAllBooks();
            void AddBook(Books Books);
            void DeleteBook(int id);

            void UpdateBook(Books Books);

        }



        public class BookService : IBookService
        {
            // Update to match your DB

            private readonly string _connectionString = "User Id=ADI;Password=ADI;Data Source=localhost:1521/XE";

            //This is a Show Employee Method
            public List<Books> GetAllBooks()
            {
                var Books = new List<Books>();

                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("SELECT Id, Name, Description, Price FROM Book", conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Books.Add(new Books
                    {
                        Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Price = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                    });
                }

                return Books;
            }

            //This is a Add Employee Method
            public void AddBook(Books Books)
            {
                if (Books.Id <= 0 || string.IsNullOrWhiteSpace(Books.Name) ||
                    string.IsNullOrWhiteSpace(Books.Description) || Books.Price <= 0)
                {
                    throw new ArgumentException("Id, Name, Description, and Price must be valid.");
                }

                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand(
                    "INSERT INTO Book (Id, Name, Description, Price) VALUES (:id, :name, :Description, :Price)", conn);

                cmd.Parameters.Add("id", OracleDbType.Int32).Value = Books.Id;
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = Books.Name;
                cmd.Parameters.Add("Description", OracleDbType.Varchar2).Value = Books.Description;
                cmd.Parameters.Add("Price", OracleDbType.Decimal).Value = Books.Price;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Error inserting Books data.", ex);
                }
            }

            //This is a Delete Employee Method
            public void DeleteBook(int id)
            {
                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("DELETE FROM Book WHERE Id = :id", conn);
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = id;

                cmd.ExecuteNonQuery();
            }

            public void UpdateBook(Books Books)
            {
                using var conn = new OracleConnection(_connectionString);
                conn.Open();

                using var cmd = new OracleCommand("UPDATE Book SET Name = :name, Description = :email, Price = :salary WHERE Id = :id", conn);
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = Books.Name;
                cmd.Parameters.Add("Description", OracleDbType.Varchar2).Value = Books.Description;
                cmd.Parameters.Add("Price", OracleDbType.Decimal).Value = Books.Price;
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = Books.Id;

                cmd.ExecuteNonQuery();
            }

        }
    }


