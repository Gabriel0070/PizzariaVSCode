using Microsoft.AspNetCore.Hosting;
using Npgsql;

using Pizzaria.Models;
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

    

    While (reader.Read())
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
} 
