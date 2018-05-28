//using System.IO;
//using System.Threading.Tasks;
//
//namespace AMKsGear.Core.Fx.CacheManager
//{
//    public interface ICacheFsAccessService
//    {
//        bool Exists(string name);
//        bool Delete(string name);
//
//        Stream GetStreamAsync(string name);
//        void SaveStreamAsync(string name, Stream stream);
//    }
//    public interface IAsyncCacheFsAccessService
//    {
//        Task<bool> Exists(string name);
//        Task<bool> Delete(string name);
//
//        Task<Stream> GetStreamAsync(string name);
//        Task SaveStreamAsync(string name, Stream stream);
//    }
//}