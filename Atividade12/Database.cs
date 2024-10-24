using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Atividade12
{
    public class Database
    {
        private MySqlConnection connection;

        public Database()
        {
            try
            {
                string connectionString = "Server=localhost;Database=crud;Uid=root;Pwd=Joao2003;";
                connection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao configurar a conexão: {ex.Message}");
                throw new InvalidOperationException("Falha ao configurar a conexão com o banco de dados.", ex);
            }
        }

        // Método para obter a conexão
        public MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                throw new InvalidOperationException("A conexão não foi inicializada corretamente.");
            }

            return connection;
        }

        // Método para abrir a conexão com validação de estado
        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    Console.WriteLine("A conexão já está aberta.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao abrir a conexão com o banco de dados: {ex.Message}");
                throw new InvalidOperationException("Não foi possível abrir a conexão com o banco de dados.", ex);
            }
        }

        // Método para fechar a conexão com validação de estado
        public void CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                else
                {
                    Console.WriteLine("A conexão já está fechada.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao fechar a conexão com o banco de dados: {ex.Message}");
                throw new InvalidOperationException("Não foi possível fechar a conexão com o banco de dados.", ex);
            }
        }
    }
}
