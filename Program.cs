using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("   Sistema de gerenciamento de consultas");
        Console.WriteLine("===========================================");
        Conexao c = new("localhost", "consultas", "root", "root");
        c.TestarConexao();

        Console.WriteLine("\nEscolha uma opção:");
        Console.WriteLine("1. Cadastrar médico");
        Console.WriteLine("2. Listar médicos");
        Console.WriteLine("3. Alterar médico");
        Console.WriteLine("4. Excluir médico");
        Console.WriteLine("5. Cadastrar paciente");
        Console.WriteLine("6. Listar pacientes");
        Console.WriteLine("7. Alterar paciente");
        Console.WriteLine("8. Excluir paciente");
        Console.WriteLine("9. Sair");
        string? opcao = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(opcao) && args.Length > 0)
        {
            opcao = args[0];
        }

        switch (opcao)
        {
            case "1":
                CadastrarMedico();
                break;
            case "2":
                ListarMedicos();
                break;
            case "3":
                AlterarMedico();
                break;
            case "4":
                ExcluirMedico();
                break;
            case "5":
                CadastrarPaciente();
                break;
            case "6":
                ListarPacientes();
                break;
            case "7":
                AlterarPaciente();
                break;
            case "8":
                ExcluirPaciente();
                break;
            case "9":
                Console.WriteLine("Saindo...");
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    public static void CadastrarMedico()
    {
        Console.WriteLine("Digite o nome completo do médico:");
        string? nomeCompleto = Console.ReadLine();

        Console.WriteLine("Digite o cargo do médico:");
        string? cargo = Console.ReadLine();

        Console.WriteLine("Digite o salário do médico:");
        string? salarioInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeCompleto) || string.IsNullOrWhiteSpace(cargo) || !decimal.TryParse(salarioInput, out decimal salario))
        {
            Console.WriteLine("Entrada inválida. Cadastro cancelado.");
            return;
        }

        Medico medico = new Medico(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        medico.Cadastrar(nomeCompleto, cargo, salario);
    }

    public static void AlterarMedico()
    {
        Console.WriteLine("Digite o nome completo do médico que deseja alterar:");
        string? nomeCompleto = Console.ReadLine();

        Console.WriteLine("Digite o novo cargo do médico:");
        string? cargo = Console.ReadLine();

        Console.WriteLine("Digite o novo salário do médico:");
        string? salarioInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeCompleto) || string.IsNullOrWhiteSpace(cargo) || !decimal.TryParse(salarioInput, out decimal salario))
        {
            Console.WriteLine("Entrada inválida. Alteração cancelada.");
            return;
        }

        Medico medico = new Medico(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());

        if (!medico.ExistePorNome(nomeCompleto))
        {
            Console.WriteLine("O nome informado não existe na tabela medicos.");
            return;
        }

        medico.Alterar(nomeCompleto, cargo, salario);
    }

    public static void ListarMedicos()
    {
        Medico medico = new Medico(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        medico.ListarTodos();
    }

    public static void ExcluirMedico()
    {
        Console.WriteLine("Digite o nome completo do médico que deseja excluir:");
        string? nomeCompleto = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nomeCompleto))
        {
            Console.WriteLine("Entrada inválida. Exclusão cancelada.");
            return;
        }

        Medico medico = new Medico(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());

        if (!medico.ExistePorNome(nomeCompleto))
        {
            Console.WriteLine("O nome informado não existe na tabela medicos.");
            return;
        }

        medico.Excluir(nomeCompleto);
    }

    public static void CadastrarPaciente()
    {
        Console.WriteLine("Nome do paciente:");
        string nome = Console.ReadLine() ?? "";
        Console.WriteLine("Idade:");
        int idade = int.Parse(Console.ReadLine() ?? "0");
        Console.WriteLine("Sexo:");
        string sexo = Console.ReadLine() ?? "";
        Console.WriteLine("Endereço:");
        string endereco = Console.ReadLine() ?? "";
        Console.WriteLine("Telefone:");
        string telefone = Console.ReadLine() ?? "";
        Console.WriteLine("Email:");
        string email = Console.ReadLine() ?? "";

        Paciente p = new Paciente(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        p.Cadastrar(nome, idade, sexo, endereco, telefone, email);
    }

    public static void ListarPacientes()
    {
        Paciente p = new Paciente(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        p.ListarTodos();
    }

    public static void AlterarPaciente()
    {
        Console.WriteLine("Digite o nome do paciente que deseja alterar:");
        string nome = Console.ReadLine() ?? "";

        Paciente p = new Paciente(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        
        if (!p.ExistePorNome(nome)) {
            Console.WriteLine("Paciente não encontrado.");
            return;
        }

        Console.WriteLine("Nova Idade:");
        int idade = int.Parse(Console.ReadLine() ?? "0");
        Console.WriteLine("Novo Endereço:");
        string endereco = Console.ReadLine() ?? "";
        Console.WriteLine("Novo Telefone:");
        string telefone = Console.ReadLine() ?? "";
        Console.WriteLine("Novo Email:");
        string email = Console.ReadLine() ?? "";

        p.Alterar(nome, idade, endereco, telefone, email);
    }

    public static void ExcluirPaciente()
    {
        Console.WriteLine("Digite o nome do paciente para excluir:");
        string nome = Console.ReadLine() ?? "";

        Paciente p = new Paciente(new Conexao("localhost", "consultas", "root", "root").ObterStringDeConexao());
        if (p.ExistePorNome(nome)) {
            p.Excluir(nome);
        } else {
            Console.WriteLine("Paciente não encontrado.");
        }
    }
}
