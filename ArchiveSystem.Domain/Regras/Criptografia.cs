using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace ArchiveSystem.Domain.Regras
{
    internal class Criptografia
    {
        public string TransformaEmHash(string cod)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(cod));
                var stringBuilder = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
