using UserServices.Models;

namespace UserServices.Reponsitories.Interfaces
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block Add(Block document);
        Block Delete(Block document);
    }
}