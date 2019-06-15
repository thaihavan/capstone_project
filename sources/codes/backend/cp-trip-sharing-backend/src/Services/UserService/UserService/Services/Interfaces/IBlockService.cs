using UserServices.Models;

namespace UserServices.Services.Interfaces
{
    public interface IBlockService
    {
        Block Block(Block block);
        Block UnBlock(Block block);
    }
}