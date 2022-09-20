using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveSystem.Models.Entidades
{
    public class ArquivoModel
    {
        public string Cod_Arquivo { get; set; }
        public string Nome { get; set; }
        public byte[] Conteudo { get; set; }
        public string Tipo { get; set; }
        public int fk_Cod_Usuario { get; set; }


    }
}
