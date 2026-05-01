using MySql.Data.MySqlClient;

class Medico
{
    private readonly string? _ConnectionString;

    public Medico(string connectionString)
    {
        _ConnectionString = connectionString;
    }
    public void Cadastrar(string nomeCompleto, string cargo, decimal salario)
    {
        //comando sql
        string sql = "INSERT INTO medicos (nomeCompleto, cargo, salario) VALUES (@nomeCompleto, @cargo, @salario)";

        //conexão com o banco de dados
        using (var connection = new MySqlConnection(_ConnectionString))
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
}
