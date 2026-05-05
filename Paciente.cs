using MySql.Data.MySqlClient;
using System;

class Paciente
{
    private readonly string? _connectionString;

    public Paciente(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Cadastrar(string nome, int idade, string sexo, string endereco, string telefone, string email)
    {
        string sql = "INSERT INTO pacientes (nome, idade, sexo, endereco, telefone, email) VALUES (@nome, @idade, @sexo, @endereco, @telefone, @email)";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@idade", idade);
            command.Parameters.AddWithValue("@sexo", sexo);
            command.Parameters.AddWithValue("@endereco", endereco);
            command.Parameters.AddWithValue("@telefone", telefone);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Paciente cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao cadastrar paciente: {e.Message}");
            }
        }
    }

    public bool ExistePorNome(string nome)
    {
        string sql = "SELECT COUNT(1) FROM pacientes WHERE nome = @nome";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nome", nome);
            try
            {
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao verificar paciente: {e.Message}");
                return false;
            }
        }
    }

    public void ListarTodos()
    {
        string sql = "SELECT nome, idade, telefone, email FROM pacientes ORDER BY nome";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            try
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Nenhum paciente cadastrado.");
                        return;
                    }

                    Console.WriteLine("\nLista de pacientes:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Nome: {reader.GetString("nome")} | Idade: {reader.GetInt32("idade")} | Tel: {reader.GetString("telefone")} | Email: {reader.GetString("email")}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao listar pacientes: {e.Message}");
            }
        }
    }

    public void Alterar(string nome, int idade, string endereco, string telefone, string email)
    {
        string sql = "UPDATE pacientes SET idade = @idade, endereco = @endereco, telefone = @telefone, email = @email WHERE nome = @nome";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@idade", idade);
            command.Parameters.AddWithValue("@endereco", endereco);
            command.Parameters.AddWithValue("@telefone", telefone);
            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0) Console.WriteLine("Paciente atualizado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao alterar paciente: {e.Message}");
            }
        }
    }

    public void Excluir(string nome)
    {
        string sql = "DELETE FROM pacientes WHERE nome = @nome";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nome", nome);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Paciente excluído com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao excluir paciente: {e.Message}");
            }
        }
    }
}