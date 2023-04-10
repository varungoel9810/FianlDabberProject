using Dapper;
using DL.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DL.Implementations
{
    public class DlRepository : IDlRepository
    {
        private IDbConnection _db;
        public DlRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void Createrepo(Repository repo)
        {
            var sql = "INSERT INTO Repositories (Name,Description,Owner) VALUES(@Name,@Description,@Owner);";
            _db.Execute(sql, new
            {
                @Name = repo.Name,
                @Description = repo.Description,
                @Owner = repo.Owner
            });
        }

        public void Deleterepo(int id)
        {
            var sql = "DELETE FROM Repositories WHERE Id=@id;";
            _db.Execute(sql, new
            {
                @id = id
            });
        }

        public List<Repository> Getall()
        {
            var sql = "SELECT * FROM Repositories";
            return _db.Query<Repository>(sql).ToList();
        }

        
        public Repository GetById(int id)
        {
            var sql = "SELECT * FROM Repositories WHERE Id = @id";
            return _db.Query<Repository>(sql, new {@id = id }).Single();
            //return _db.QuerySingleOrDefault <Repository>(sql,new { @id=id });
        }

        public void Updaterepo(Repository repo)
        {
            var sql = "UPDATE Repositories SET Name=@Name,Description=@Description,Owner=@Owner " +
                    "WHERE Id=@id";
            _db.Execute(sql, new
            {
                @id = repo.Id,
                @Name = repo.Name,
                @Description = repo.Description,
                @Owner = repo.Owner
            });
        }

        
       
    }
}
