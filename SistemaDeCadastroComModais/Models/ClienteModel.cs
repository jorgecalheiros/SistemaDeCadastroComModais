namespace SistemaDeCadastroComModais.Models
{
    public class ClienteModel
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }


        public ClienteModel(string name, string email, int telefone) {
            if(name == null) throw new ArgumentNullException("name");
            if(email == null) throw new ArgumentNullException("email");
            if (telefone <= 0) throw new Exception("telefone");
            Name = name;
            Email = email;
            Telefone = telefone;
        }
        public void setId(int id)
        {
            if(id <= 0)throw new Exception("Error id");
            Id = id;    
        }
    }
}
