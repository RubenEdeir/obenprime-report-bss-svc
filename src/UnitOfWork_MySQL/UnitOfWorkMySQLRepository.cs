using MySqlConnector;
using Repository_Interface;
using Repository_MySQL;
using UnitOfWork_Interface;


namespace UnitOfWork_MySQL;

public class UnitOfWorkMySQLRepository : IUnitOfWorkRepository
{
    public UnitOfWorkMySQLRepository(MySqlConnection context, MySqlTransaction transaction)
    {
        HelperRepository = new HelperRepository(context, transaction);
    }

    public IHelperRepository HelperRepository { get; }
}