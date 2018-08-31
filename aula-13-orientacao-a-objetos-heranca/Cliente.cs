using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Classes
{
    public class Cliente
    {
        public Cliente(string nome, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.CPF = cpf;

        }
        public Cliente()
        {
        }

        public string Nome;
        public string Telefone;
        public string CPF;

        public void Gravar()
        {
            if(this.GetType() == typeof(Cliente))
            {
                var clientes = Cliente.LerClientes();
                clientes.Add(this);
                if (File.Exists(caminhoBaseCliente()))
                {
                    StreamWriter r = new StreamWriter(caminhoBaseCliente());
                    r.WriteLine("nome;telefone;cpf;");

                    foreach (Cliente c in clientes)
                    {
                        var linha = c.Nome + ";" + c.Telefone + ";" + c.CPF + ";";
                        r.WriteLine(linha);
                    }

                    r.Close();
                }
            }
            else
            {
                var usuario = Usuario.LerUsuarios();
                Usuario u = new Usuario(this.Nome, this.Telefone, this.CPF);
                usuario.Add(u);
                if (File.Exists(caminhoBaseUsuario()))
                {
                    StreamWriter r = new StreamWriter(caminhoBaseUsuario());
                    r.WriteLine("nome;telefone;cpf;");

                    foreach (Usuario c in usuario)
                    {
                        var linha = c.Nome + ";" + c.Telefone + ";" + c.CPF + ";";
                        r.WriteLine(linha);
                    }

                    r.Close();
                }
            }

        }
        public static string caminhoBaseCliente()
        {
            return ConfigurationManager.AppSettings["BaseDeClientes"];
        }

        public static string caminhoBaseUsuario()
        {
            return ConfigurationManager.AppSettings["BaseDeUsuarios"];
        }

        public static List<Cliente> LerClientes()
        {
            var clientes = new List<Cliente>();

            if (File.Exists(caminhoBaseCliente()))
            {
                using (StreamReader arquivo = File.OpenText(caminhoBaseCliente()))
                {
                    string linha;
                    int i = 0;
                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        i++;
                        if (i == 1) continue;
                        var clienteArquivo = linha.Split(';');

                        var cliente = new Cliente(clienteArquivo[0], clienteArquivo[1], clienteArquivo[2]);

                        clientes.Add(cliente);
                    }
                }
            }

            return clientes;

        }
         public static List<Usuario> LerUsuarios()
         {
                var usuarios = new List<Usuario>();

                if (File.Exists(caminhoBaseCliente()))
                {
                    using (StreamReader arquivo = File.OpenText(caminhoBaseUsuario()))
                    {
                        string linha;
                        int i = 0;
                        while ((linha = arquivo.ReadLine()) != null)
                        {
                            i++;
                            if (i == 1) continue;
                            var usuarioArquivo = linha.Split(';');

                            var usuario = new Usuario(usuarioArquivo[0], usuarioArquivo[1], usuarioArquivo[2]);

                            usuarios.Add(usuario);
                        }
                    }
                }

                return usuarios;
         }
        
    }
}
