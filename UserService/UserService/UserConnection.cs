using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using UserService.Models;

namespace UserService
{
    public class UserConnection : DbContext
    {
        public UserConnection(DbContextOptions<UserConnection> options)
                : base(options)
        {
            //Database.EnsureCreated();
           // Database.Migrate();
        }

        public DbSet<User> Users { get; set; }

        //public static IDbConnection CreateDbConnection()
        //{
        //    IDbConnection connection = null;
        //    try
        //    {
        //        var sConnectionString = "Data Source = MSSQLSERVER; Server = LAPTOP - 49EKNLEF; Initial Catalog = remuseDB; Integrated Security = True";
        //        var sProviderName = "System.Data.SqlClient";

        //        var factory = DbProviderFactories.GetFactory(sProviderName);

        //        connection = factory.CreateConnection();
        //        connection.ConnectionString = sConnectionString;

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return connection;
        //}

        //public static IDbCommand CreateDbCommand(IDbConnection conn, string sSQL, CommandType type)
        //{
        //    IDbCommand command = null;

        //    try
        //    {
        //        command = conn.CreateCommand();
        //        command.CommandType = type;
        //        command.CommandText = sSQL;
        //    }
        //    catch (DbException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    return command;
        //}

    }
}
