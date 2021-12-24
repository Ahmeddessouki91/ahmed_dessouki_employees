using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace envolvice.taskAPI.FileReader
{
    public class TextFileReader : IFileReader
    {
        public async Task<IEnumerable<string>> ReadAsync(Stream stream)
        {
            if (stream == null)
                throw new FileNotFoundException();

            if (stream.Length == 0)
                throw new FileLoadException();


            using var streamReader = new StreamReader(stream);

            var headerLine = await streamReader.ReadLineAsync();
           
            List<string> lines = new List<string>();
            string line;
            while ((line = await streamReader.ReadLineAsync()) != null)
                lines.Add(line);

            return lines;
        }
    }
}
