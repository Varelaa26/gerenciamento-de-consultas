using MySql.Data.MySqlClient;

class Medico
{
    private readonly string? _connectionString;

    public Medico(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void Cadastrar(string nomeCompleto, string cargo, decimal salario)
    {
        //comando sql
        string sql = "INSERT INTO medicos (nomeCompleto, cargo, salario) VALUES (@nomeCompleto, @cargo, @salario)";

        //conexão com o banco de dados
        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            //parametros
            command.Parameters.AddWithValue("@nomeCompleto", nomeCompleto);
            command.Parameters.AddWithValue("@cargo", cargo);
            command.Parameters.AddWithValue("@salario", salario);

            //executar comando
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Médico cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao cadastrar médico: {e.Message}");
            }
        }
    }
    public bool ExistePorNome(string nomeCompleto)
    {
        string sql = "SELECT COUNT(1) FROM medicos WHERE nomeCompleto = @nomeCompleto";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nomeCompleto", nomeCompleto);

            try
            {
                connection.Open();
                var resultado = Convert.ToInt32(command.ExecuteScalar());
                return resultado > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao verificar médico: {e.Message}");
                return false;
            }
        }
    }

    public void ListarTodos()
    {
        string sql = "SELECT nomeCompleto, cargo, salario FROM medicos ORDER BY nomeCompleto";

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
                        Console.WriteLine("Nenhum médico cadastrado.");
                        return;
                    }

                    Console.WriteLine("\nLista de médicos cadastrados:");
                    while (reader.Read())
                    {
                        string nome = reader.GetString("nomeCompleto");
                        string cargo = reader.GetString("cargo");
                        decimal salario = reader.GetDecimal("salario");

                        Console.WriteLine($"Nome: {nome} | Cargo: {cargo} | Salário: {salario:C}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao listar médicos: {e.Message}");
            }
        }
    }

    public bool Alterar(string nomeCompleto, string cargo, decimal salario)
    {
        string sql = "UPDATE medicos SET cargo = @cargo, salario = @salario WHERE nomeCompleto = @nomeCompleto";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nomeCompleto", nomeCompleto);
            command.Parameters.AddWithValue("@cargo", cargo);
            command.Parameters.AddWithValue("@salario", salario);

            try
            {
                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Médico alterado com sucesso!");
                    return true;
                }

                Console.WriteLine("Nenhum médico foi alterado.");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao alterar médico: {e.Message}");
                return false;
            }
        }
    }
    public bool Excluir(string nomeCompleto)
    {
        string sql = "DELETE FROM medicos WHERE nomeCompleto = @nomeCompleto";

        using (var connection = new MySqlConnection(_connectionString))
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nomeCompleto", nomeCompleto);

            try
            {
                connection.Open();
                int linhasAfetadas = command.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    Console.WriteLine("Médico excluído com sucesso!");
                    return true;
                }

                Console.WriteLine("Nenhum médico foi excluído.");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao excluir médico: {e.Message}");
                return false;
            }
        }
    }
}
