using System;
using System.IO;
using System.Threading.Tasks;

namespace MonkeyService
{
    public interface IPickerImage
    {
        Task<Stream> GetImage();
    }
}
