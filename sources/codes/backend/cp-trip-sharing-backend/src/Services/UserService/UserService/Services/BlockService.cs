using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;

namespace UserServices.Services
{
    public class BlockService : IBlockService
    {
        private readonly BlockRepository _blockRepository = null;

        public BlockService(BlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public BlockService(IOptions<AppSettings> settings)
        {
            _blockRepository = new BlockRepository(settings);
        }

        public Block Block(Block block)
        {
            return _blockRepository.Add(block);
        }

        public Block UnBlock(Block block)
        {
            return _blockRepository.Delete(block);
        }
    }
}
