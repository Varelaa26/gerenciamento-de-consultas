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
        Console.WriteLine("5. Cadastrar usuário");
        Console.WriteLine("6. Listar usuários");
        Console.WriteLine("7. Alterar usuário");
        Console.WriteLine("8. Excluir usuário");
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
                //ExcluirMedico();
                break;
            case "5":
                //CadastrarUsuário();
                break;
            case "6":
                //ListarUsuários();
                break;
            case "7":
                //AlterarUsuario();
                break;
            case "8":
                //ExcluirUsuario();
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
        // Implementar exclusão de médico
        Console.WriteLine("Funcionalidade de excluir médico não implementada ainda.");
    }

    public static void AlterarUsuario()
    {
        // Implementar alteração de usuário
        Console.WriteLine("Funcionalidade de alterar usuário não implementada ainda.");
    }

    public static void ExcluirUsuario()
    {
        // Implementar exclusão de usuário
        Console.WriteLine("Funcionalidade de excluir usuário não implementada ainda.");
    }
}
