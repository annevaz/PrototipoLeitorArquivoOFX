namespace PrototipoLeitorOFX.Domain
{
    public class Status
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Categoria { get; set; }
        public string Messagem { get; set; }

        public Status() { }

        public Status(int Codigo, string Categoria, string Messagem)
        {
            this.Codigo = Codigo;
            this.Categoria = Categoria;
            this.Messagem = Messagem;
        }
    }
}