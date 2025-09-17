using System;
using System.Collections.Generic;

namespace TennisClientApp
{
    class Program
    {
        static void Main()
        {
            var clientManager = new ClientManager();
            clientManager.Run();
        }
    }

    class ClientManager
    {
        private readonly List<ClientTennis> _clientes = new();

        public void Run()
        {
            CarregarClientesDeArquivo();
            while (true)
            {

                ExibirMenu();
                int option = InputValidator.GetValidOptionMenu("Escolha uma opção do menu (1-5): ");
                Console.Clear();

                switch (option)
                {
                    case 1:
                        CadastrarCliente();
                        break;
                    case 2:
                        ListarClientes();
                        break;
                    case 3:
                        BuscarClientePorNome();
                        break;
                    case 4:
                        ExcluirCliente();
                        break;
                    case 5:
                        Console.WriteLine("Saindo do programa...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void ExibirMenu()
        {
            Console.WriteLine("Bem-vindo ao sistema de cadastro de clientes de tênis!");
            Console.WriteLine("-------- MENU DE CADASTRO DE CLIENTES --------");
            Console.WriteLine("1- Cadastrar clientes");
            Console.WriteLine("2- Listar alunos");
            Console.WriteLine("3- Buscar aluno por nome");
            Console.WriteLine("4- Excluir aluno");
            Console.WriteLine("5- Sair)");
        }

        private void CadastrarCliente()
        {
            string nome = InputValidator.GetValidString("Digite o nome do cliente:");
            int idade = InputValidator.GetValidInt("Digite a idade do cliente:");
            var group = InputValidator.GetValidEnum<ClientTennis.GroupCategory>("Escolha a categoria do grupo:");
            bool clienteExiste = _clientes.Exists(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
                if (clienteExiste)
                {
                Console.WriteLine("Já existe um cliente cadastrado com esse nome!");
                ClearScreen();
                return;
                }
            _clientes.Add(new ClientTennis(nome, idade, group));
            SalvarClientesEmArquivo();
            Console.Clear();
            Console.WriteLine("Cliente cadastrado com sucesso!");
            ClearScreen();
        }

        private void ListarClientes()
        {
            if (_clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            else
            {
                foreach (var client in _clientes)
                {
                    client.Exibir();
                }
            }
            ClearScreen();
        }

        private void BuscarClientePorNome()
        {
            string searchName = InputValidator.GetValidString("Digite o nome do aluno que deseja buscar:");
            var foundClients = _clientes.FindAll(c => c.Nome.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (foundClients.Count == 0)
            {
                Console.WriteLine("Aluno não encontrado.");
            }
            else
            {
                foreach (var client in foundClients)
                {
                    client.Exibir();
                }
            }
            ClearScreen();
        }

        private void ExcluirCliente()
        {
            if (_clientes.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado para excluir.");
                ClearScreen();
                return;
            }

            for (int i = 0; i < _clientes.Count; i++)
            {
                Console.WriteLine($"{i}: {_clientes[i].Nome}");
            }

            int indexToRemove = InputValidator.GetValidInt("Digite o índice do aluno que você deseja excluir:");
            if (indexToRemove >= 0 && indexToRemove < _clientes.Count)
            {
                _clientes.RemoveAt(indexToRemove);
                Console.WriteLine("Aluno excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Índice inválido.");
            }
            ClearScreen();
        }

        private void SalvarClientesEmArquivo()
        {
            string caminho = "clientes.txt";
            try
            {
                using (var writer = new System.IO.StreamWriter(caminho))
                {
                    foreach (var client in _clientes)
                    {
                        writer.WriteLine($"{client.Nome},{client.Idade},{client.Group}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar clientes: {ex.Message}");
            }
        }

        private void CarregarClientesDeArquivo()
        {
            string line = "clientes.txt";
            if (!System.IO.File.Exists(line))
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }
            try
            {
                using (var reader = new System.IO.StreamReader(line))
                {
                    string fileLine;
                    while ((fileLine = reader.ReadLine()) != null)
                    {
                        var parts = fileLine.Split(',');
                        if (parts.Length == 3 &&
                            int.TryParse(parts[1], out int idade) &&
                            Enum.TryParse(parts[2], out ClientTennis.GroupCategory group))
                        {
                            _clientes.Add(new ClientTennis(parts[0], idade, group));
                        }
                    }
                    Console.WriteLine("Clientes carregados com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar clientes: {ex.Message}");
            }
        }

        private static void ClearScreen()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static class InputValidator
    {
        public static int GetValidOptionMenu(string prompt)
        {
            return GetValidInt(prompt, 1, 5);
        }

        public static string GetValidString(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("A entrada não pode ser vazia. Tente novamente.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public static int GetValidInt(string prompt, int min = 0, int max = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Por favor, insira um número válido entre {min} e {max}.");
            }
        }

        public static T GetValidEnum<T>(string prompt) where T : Enum
        {
            while (true)
            {
                Console.WriteLine(prompt);
                foreach (var value in Enum.GetValues(typeof(T)))
                {
                    Console.WriteLine($"{(int)value}. {value}");
                }

                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(T), choice))
                {
                    return (T)(object)choice;
                }
                Console.WriteLine("Entrada inválida. Tente novamente.");
            }
        }
    }

    class ClientTennis
    {
        public string Nome { get; }
        public int Idade { get; }
        public GroupCategory Group { get; }

        public enum GroupCategory
        {
            HighPerformance,
            AdultsBeginners,
            AdultsIntermediate,
            AdultsAdvanced,
            FullTime,
            Kids,
            TournamentTrainer
        }

        public ClientTennis(string nome, int idade, GroupCategory group)
        {
            Nome = nome;
            Idade = idade;
            Group = group;
        }

        public void Exibir()
        {
            Console.WriteLine($"Nome: {Nome}, Idade: {Idade}, Grupo: {Group}");
        }
    }
}
