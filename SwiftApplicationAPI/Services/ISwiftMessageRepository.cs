using SwiftApplicationAPI.Models;

namespace SwiftApplicationAPI.Services
{
    public interface ISwiftMessageRepository
    {
        public Task<int> Create(MT799Model mT799Model);
        public Task<IEnumerable<MT799Model>> GetAll();
        public Task<int> DeleteById(int id);
        public Task<int> UpdateById(int id, MT799Model mT799Model);
    }
}
