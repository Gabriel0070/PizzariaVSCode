using Microsoft.AspNetCore.Hosting;
using Npgsql;

//using Pizzaria.Models;
using Npgsql;
using pizzariaggn.models;

namespace Pizzaria.Repository;

public class UserRepository{

    private readonly DatabaseConnection _dbconnection;
    private readonly IWebHostEnvironment _webHostEnviroment;
    public UserRepository(DatabaseConnection dbconnection,IWebHostEnvironment webHostEnviroment){
        _dbconnection = dbconnection;
        _webHostEnviroment = webHostEnviroment;
    }

public List<User> ListUser(){
    var users = new List<User>();

    using(var connection = _dbconnection.GetConnection()){

    using(var command = new NpgsqlCommand("Slect id_usuario, nome,login,email,foto from usuario",(NpgsqlConnection)connection))
    {

    using(var reader = command.ExecuteReader()){

    

    While (reader.Read());
    {

        users.Add(new User
        {
        Id= reader.GetInt32(0),
        Name= reader.GetString(1),
        Login= reader.GetString(2),
        Email= reader.GetString(3),
        Photo= reader.IsDBNull(4) ? null: reader.GetString(4), 
     });
    };
}
}
}
return users;
}


    private void While(bool v)
    {
        throw new NotImplementedException();
    }
    public async Task InsertUser(User user, IFormFile arquivo)
    {
        string caminhoArquivo = await Upload(arquivo, "img");

        using(var connection = _dbconnection.GetConnection()){
            
            using (var command = connection.CreateCommand()){

                command.CommandText= "INSERT INTO usuario(nome,login,email,senha,foto) values(@nome,@login,@emial,@senha,foto)";
                 
                 var nomeParam = command.CreateParameter();
                 nomeParam.ParameterName ="@nome";
                 nomeParam.Value= user.Name;
                 command.Parameters.Add(nomeParam);

                 var loginParam = command.CreateParameter();
                 loginParam.ParameterName ="@login";
                 loginParam.Value= user.Login;
                 command.Parameters.Add(loginParam);

                 var emailParam = command.CreateParameter();
                 emailParam.ParameterName ="@email";
                 emailParam.Value= user.Email;
                 command.Parameters.Add(emailParam);

                 var senhaParam = command.CreateParameter();
                 senhaParam.ParameterName ="@senha";
                 senhaParam.Value= user.Password;
                 command.Parameters.Add(senhaParam);

                 user.Photo = caminhoArquivo;

                 var fotoParam = command.CreateParameter();
                 fotoParam.ParameterName ="@foto";
                 fotoParam.Value= user.Photo;
                 command.Parameters.Add(fotoParam);

                 command.ExecuteNonQuery();
            }
        }

    }

    public async Task<string> Upload(IFormFile arquivo, string pasta){
        if(arquivo == null || arquivo.Length == 0){
            return "";
        }

        string caminhoPasta = Path.Combine(_webHostEnviroment.WebRootPath,pasta);

        if(!Directory.Exists(caminhoPasta))
        Directory.CreateDirectory(caminhoPasta);

        string nomeArquivo = Guid.NewGuid().ToString()+Path.GetExtension(arquivo.FileName);
        string caminhoCompleto =Path.Combine(caminhoPasta,nomeArquivo);

        using(var stream = new FileStream(caminhoCompleto,FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }
        return $"/{pasta}/{nomeArquivo}";
    }
    
                           //...\\

} 
