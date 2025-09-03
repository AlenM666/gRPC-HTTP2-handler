using CustomGrpcBackend.Models.Entities;


namespace CustomGrpcBackend.Services.Interfaces;


public interface IUserService
{
  Task<UserEntity?> GetUserAsync(int id);
  Task<IEnumerable<UserEntity>> GetAllUsersAsync(int pageSize = 10 ,int pageNumber = 1);
  Task<UserEntity> CreateUserAsync(string name, string email);
  Task<UserEntity?> UpdateUserAsync(int id ,string name, string email);
  Task<bool> DeleteUserAsync(int id);
  Task<int> GetTotalUsersCountAsync();
  Task<bool> UserExistsAsync(int id);
}


