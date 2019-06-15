using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services.Interfaces
{
    public interface IUploadFileService
    {
        string UploadImage(ImageParam imageParam);
    }
}
