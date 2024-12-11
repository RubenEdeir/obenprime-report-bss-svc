using Repository_Interface;

namespace UnitOfWork_Interface;

public interface IUnitOfWorkRepository
{
    IHelperRepository HelperRepository { get; }
}