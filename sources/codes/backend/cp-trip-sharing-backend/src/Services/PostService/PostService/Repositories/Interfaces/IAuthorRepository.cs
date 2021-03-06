﻿using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author InsertOrUpdate(Author author);
    }
}
