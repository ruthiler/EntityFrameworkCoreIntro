using System;
using System.Collections.Generic;
using System.Linq;
using EntityFCore.Domain;
using EntityFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EntityFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // InserirDados();
            // InserirDadosEmMassa();
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoCarregamentoAdiandado();
            // AtualizarDados();
            // AtualizarDadosDesconetado();
            // AtualizarDadosDesconetadoInformandoSomenteId();
            RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1002);

            // Opcao 1
            //db.Clientes.Remove(cliente);

            // Opcao 2
            //db.Remove(cliente);

            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            cliente.Nome = "Nome alterado - Passo 2";

            //Atualiza todas as propriedades, mesmo que não tenham sofrido alteração.
            //Utilizar somente o SaveChanges, assim só será atualizado a propriedade que sofreu alteração.
            //db.Entry(cliente).State = EntityState.Modified;

            //Atualiza todas as propriedades, mesmo que não tenham sofrido alteração.
            //Utilizar somente o SaveChanges, assim só será atualizado a propriedade que sofreu alteração.
            //db.Clientes.Update(cliente); 

            db.SaveChanges();
        }

        private static void AtualizarDadosDesconetado()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);

            var clienteDesconectado = new
            {
                Nome = "Cliente desconectado",
                Telefone = "85955558888"
            };

            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            db.SaveChanges();
        }

        private static void AtualizarDadosDesconetadoInformandoSomenteId()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);

            var cliente = new Cliente
            {
                Id = 3
            };

            var clienteDesconectado = new
            {
                Nome = "Cliente desconectado Informando Somente Id",
                Telefone = "85955558888"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiandado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db
                .Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                StatusPedido = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
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

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes.Where( p => p.Id > 0).ToList();

            foreach(var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault( p => p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste Inserção em Massa",
                CodigoBarras = "64321987",
                Valor = "20",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

             var cliente = new Cliente
            {
                Nome = "Tallys Ruthiler",
                CEP = "60010250",
                Cidade = "Fortaleza",
                Estado = "CE",
                Telefone = "85999141258",
            };

            var listaClientes = new[]
            {
                new Cliente
                {
                    Nome = "Theo Ruthiler",
                    CEP = "60010250",
                    Cidade = "Fortaleza",
                    Estado = "CE",
                    Telefone = "85999141258",
                },
                new Cliente
                {
                    Nome = "Marcela",
                    CEP = "60010250",
                    Cidade = "Fortaleza",
                    Estado = "CE",
                    Telefone = "85999141258",
                }
            };

            using var db = new Data.ApplicationContext();
            // db.AddRange(produto, cliente);

            db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registros em massa {registros}");

        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste Inserção",
                CodigoBarras = "12345678912346",
                Valor = "10",
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }
    }
}
