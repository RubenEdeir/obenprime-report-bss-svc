using Microsoft.Extensions.Configuration;
using UnitOfWork_Interface;

namespace UnitOfWork_MySQL;

public class UnitOfWorkMySQL : IUnitOfWork
{
    private readonly IConfiguration _configuration;

    public UnitOfWorkMySQL(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IUnitOfWorkAdapter Create()
    {
        var connectionString = _configuration.GetConnectionString("MariaDBConnection");

        return new UnitOfWorkMySQLAdapter(connectionString);
    }
}