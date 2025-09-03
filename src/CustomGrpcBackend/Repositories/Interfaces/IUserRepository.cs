using CustomGrpcBackend.Models.Entities;

namespace CustomGrpcBackend.Repositories.Interfaces;

public interface IUserRepository
{
  Task<UserEntity?> GetByIdAsync(int id);
  Task<IEnumerable<UserEntity>> GetAllAsync(int pageSize = 10, int pageNumber = 1);
  Task<UserEntity> CreateAsync(UserEntity user);
  Task<UserEntity?> UpdateAsync(UserEntity user);
  Task<bool> DeleteAsync(int id);
  Task<int> GetTotalCountAsync();
  Task<bool> ExistsAsync(int id);
  Task<UserEntity?> GetByEmailAsync(string email);
}


