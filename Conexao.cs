using MySql.Data.MySqlClient;

public class Conexao
{
    private readonly string _connectionString;
    private const string _server = "localhost";
    private const string _database = "consultas";
    private const string _password = "root";


    public Conexao(string server, string database, string user, string password)
    {
        _connectionString = $"Server={server};Database={database};User ID={user};Password={password};";
    }

   public void TestarConexao()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                //Console.WriteLine("Conexão bem-sucedida!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao conectar: {ex.Message}");
            }
        }
    }

    public string ObterStringDeConexao()
    {
        return _connectionString;
    }
}