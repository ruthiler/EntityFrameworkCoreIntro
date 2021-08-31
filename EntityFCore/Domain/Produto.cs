using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFCore.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFCore.Domain
{
    public class Produto
    {
        public int Id { get; set; }

        public string CodigoBarras { get; set; }

        public string Descricao { get; set; }

        public string Valor { get; set; }

        public TipoProduto TipoProduto { get; set; }

        public bool Ativo { get; set; }

    }
}
