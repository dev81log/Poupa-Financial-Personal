

Console.WriteLine("-----PoupaDev-----");

var objetivos = new List<ObjetivoFinanceiro>();

ExibirMenu();
var opcao = Console.ReadLine();


while (opcao != "0")
{
    switch (opcao)
    {
        case "1":
            CadastrarObjetivo();
            break;
        case "2":
            RealizarOperacao(TipoOperacao.Deposito);
            break;
        case "3":
            RealizarOperacao(TipoOperacao.Saque);
            break;
        case "4":
            ObterDetalhes();
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
    ExibirMenu();
    opcao = Console.ReadLine();
}


void CadastrarObjetivo()
{
    Console.WriteLine("Digite um título para o objetivo");
    var titulo = Console.ReadLine();

    Console.WriteLine("Digite o valor do objetivo");
    var valor = decimal.Parse(Console.ReadLine());

    var objetivo = new ObjetivoFinanceiro(titulo, valor);

    objetivos.Add(objetivo);
    Console.WriteLine($"Objetivo ID: {objetivo.Id} cadastrado com sucesso");
}


void RealizarOperacao(TipoOperacao tipo)
{
    Console.WriteLine("Digite o ID do objetivo");
    var id = int.Parse(Console.ReadLine());

    Console.WriteLine("Digite o valor da operação");
    var valor = decimal.Parse(Console.ReadLine());

    var operacao = new Operacao(valor, tipo, id);

    var objetivo = objetivos.SingleOrDefault(o => o.Id == id);

    objetivo.Operacoes.Add(operacao);
}


void ObterDetalhes()
{
    Console.WriteLine("Digite o ID do objetivo");
    var id = int.Parse(Console.ReadLine());

    var objetivo = objetivos.SingleOrDefault(o => o.Id == id);

    objetivo.ImprimirResumo();
}


void ExibirMenu()
{
    Console.WriteLine("Digite 1 - Cadastro de objetivo");
    Console.WriteLine("Digite 2 - Realizar um depósito");
    Console.WriteLine("Digite 3 - Realizar um saque");
    Console.WriteLine("Digite 4 - Exibir detalhes de um objetivo");
    Console.WriteLine("Digite 0 - Encerrar terminal");
}


public enum TipoOperacao
{
    Saque = 0,
    Deposito = 1,
}


public class Operacao
{
    public Operacao(decimal valor, TipoOperacao tipo, int idObjetivo)
    {
        Id = new Random().Next(1, 1000);
        Valor = valor;
        Tipo = tipo;
        IdObjetivo = idObjetivo;
    }

    public int Id { get; private set; }
    public decimal Valor { get; private set; }
    public TipoOperacao Tipo { get; private set; }
    public int IdObjetivo { get; private set; }
}


public class ObjetivoFinanceiro
{
    public ObjetivoFinanceiro(string titulo, decimal valorObjetivo)
    {
        Id = new Random().Next(1, 1000);
        Titulo = titulo;
        ValorObjetivo = valorObjetivo;

        Operacoes = new List<Operacao>();
    }


    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public decimal ValorObjetivo { get; private set; }
    public List<Operacao> Operacoes { get; private set; }
    public decimal Saldo => ObterSaldo();


    public decimal ObterSaldo()
    {
        var totalDeposito = Operacoes
            .Where(o => o.Tipo == TipoOperacao.Deposito)
            .Sum(o => o.Valor);

        var totalSaque = Operacoes
            .Where(o => o.Tipo == TipoOperacao.Saque)
            .Sum(o => o.Valor);

        return totalDeposito - totalSaque;
    }


    public void ImprimirResumo()
    {
        Console.WriteLine($"Objetivo {Titulo}, Valor: {ValorObjetivo}, com Saldo: R${Saldo}");
    }
}


