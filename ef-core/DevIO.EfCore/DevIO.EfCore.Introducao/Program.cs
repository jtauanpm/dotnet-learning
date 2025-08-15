// See https://aka.ms/new-console-template for more information

using DevIO.EfCore.Introducao.Configuration;
using DevIO.EfCore.Introducao.Data;
using DevIO.EfCore.Introducao.Domain;
using DevIO.EfCore.Introducao.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

Settings.Configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// using var db = new ApplicationContext();

// Apply migration using code
// db.Database.Migrate();

// Verifica se há migrações pendentes
// var hasPendingMigrations = db.Database.GetPendingMigrations().Any();

// InserirDados();
// InserirDadosEmMassa();
ConsultarDados();
// CadastrarPedido();
// ConsultarPedidoCarregamentoAdiantado();
// AtualizarDados();
// RemoverRegistro();
return;

static void RemoverRegistro()
{
    using var db = new ApplicationContext();
    
    // var cliente = db.Clientes.Find(2);
    var cliente = new Cliente { Id = 3 }; // Entidade desconectada
    
    // db.Entry(cliente).State = EntityState.Deleted;
    // db.Remove(cliente);
    db.Clientes.Remove(cliente);

    db.SaveChanges();
}

static void AtualizarDados()
{
    using var db = new ApplicationContext();
    // var cliente = db.Clientes.Find(1);
    // cliente.Nome = "Cliente Alterado Passo 3";
    
    var cliente = new Cliente
    {
        Id = 1
    };
    
    var clienteDesconectado = new
    {
        Nome = "Cliente Desconectado Passo 3",
        Telefone = "12332112332"
    };
    
    // Esse comando obriga o EFCore a rastrear um dado desconectado, não advindo de nenhuma consulta
    db.Attach(cliente);
    db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
    
    // Baixo nível
    // db.Entry(cliente).State = EntityState.Modified;
    
    // Esse comando força o EfCore a atualizar todas as colunas
    // db.Clientes.Update(cliente); 
    
    db.SaveChanges();
}

static void ConsultarPedidoCarregamentoAdiantado()
{
    using var db = new ApplicationContext();
    var pedidos = db.Pedidos
        .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
        .ToList();
    
    Console.WriteLine(pedidos.Count);
}

static void CadastrarPedido()
{
    using var db = new ApplicationContext();

    var cliente = db.Clientes.FirstOrDefault();
    var produto = db.Produtos.FirstOrDefault();

    var pedido = new Pedido
    {
        ClienteId = cliente.Id,
        IniciadoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = "Pedido Teste",
        Status = StatusPedido.Analise,
        TipoFrete = TipoFrete.SemFrete,
        Itens = new List<PedidoItem>
        {
            new()
            {
                ProdutoId = produto.Id,
                Desconto = 0,
                Quantidade = 1,
                Valor = 10,
            }
        }
    };

    db.Pedidos.Add(pedido);
    
    db.SaveChanges();
}

static void InserirDados()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891231",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    using var db = new ApplicationContext();
    
    db.Produtos.Add(produto); 
    db.Set<Produto>().Add(produto); // útil para se trabalhar com tipos genéricos ou em tempo de execução
    db.Entry(produto).State = EntityState.Added; // mais baixo nível, controla o estado manualmente
    db.Add(produto); // exige maior processamento para identificar a tabela do tipo do parâmetro
    
    var registros = db.SaveChanges();
    Console.WriteLine($"Total registros: {registros}");
}

static void InserirDadosEmMassa()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891231",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    var cliente = new Cliente
    {
        Nome = "John Lennon",
        Telefone = "99000001111",
        CEP = "53214000",
        Estado = "PB",
        Cidade = "Cidade"
    };
    
    var clientes = new[]
    {
        new Cliente
        {
            Nome = "Ringo Star",
            Telefone = "99000001111",
            CEP = "53214000",
            Estado = "PB",
            Cidade = "Cidade"
        },
        new Cliente
        {
            Nome = "Paul McCartney",
            Telefone = "99000001111",
            CEP = "53214000",
            Estado = "PB",
            Cidade = "Cidade"
        }
    };
    
    using var db = new ApplicationContext();
    // db.AddRange(produto, cliente);
    
    db.Clientes.AddRange(clientes);
    
    var registros = db.SaveChanges();
    Console.WriteLine($"Total registros: {registros}");
}

static void ConsultarDados()
{
    using var db = new ApplicationContext();
    
    // Query syntax
    // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
    
    // Method syntax
    var consultaPorMetodo = db.Clientes
        .Where(c => c.Id > 0)
        // .AsNoTracking() // impede o rastreamento de dados em memória do EFCore
        .OrderBy(c => c.Id)
        .ToList();
    
    foreach (var cliente in consultaPorMetodo)
    {
        Console.WriteLine($"Consultando Cliente: {cliente.Id}");
        // db.Clientes.Find(cliente.Id); // Considera os dados já carregados em memória 
        db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
    }
}


