using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace envolvice.taskAPI.FileReader
{
    public interface IFileReader
    {
        Task<IEnumerable<string>> ReadAsync(Stream stream);
    }
}