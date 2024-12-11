using MySqlConnector;
using UnitOfWork_Interface;

namespace UnitOfWork_MySQL;

public class UnitOfWorkMySQLAdapter : IUnitOfWorkAdapter
{
    public UnitOfWorkMySQLAdapter(string connectionString)
    {
        _context = new MySqlConnection(connectionString);
        _context.Open();

        _transaction = _context.BeginTransaction();

        Repositories = new UnitOfWorkMySQLRepository(_context, _transaction);
    }

    private MySqlConnection _context { get; }

    private MySqlTransaction _transaction { get; }

    public IUnitOfWorkRepository Repositories { get; set; }

    public void Dispose()
    {
        _transaction?.Dispose();

        if (_context != null)
        {
            _context.Close();
            _context.Dispose();
        }

        Repositories = null;
    }

    public void SaveChanges()
    {
        _transaction.Commit();
    }
}