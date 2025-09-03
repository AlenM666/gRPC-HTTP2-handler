using CustomGrpcBackend.Models.Entities;
using CustomGrpcBackend.Repositories.Interfaces;
using System.Collections.Concurrent;

namespace CustomGrpcBackend.Repositories;

public class UserRepository : IUserRepository
{
  private readonly ConcurrentDictionary<int, UserEntity> _users;
  private readonly ILogger<UserRepository> _logger;
  private int _nextId = 1;

  public UserRepository(ILogger<UserRepository> logger)
  {
    _logger = logger;
    _users = new ConcurrentDictionary<int, UserEntity>();

    //initilize with sample data
    SeedData();
  }

  private void SeedData()
  {
    var sampleUsers = new[]
    {
      new UserEntity { Id = _nextId++, Name = "Alen Muratovic", Email = "alen@example.com" },
      new UserEntity { Id = _nextId++, Name = "Ziga Zoric", Email = "ziga@example.com" },
      new UserEntity { Id = _nextId++, Name = "David Lazic", Email = "david@example.com" }
    };

    foreach ( var user in sampleUsers )
    {
      _users.TryAdd(user.Id, user);
    }
  }

  public Task<UserEntity?> GetByIdAsync(int id)
  {
    _logger.LogDebug("Getting user by ID: {UserId}", id);
    _users.TryGetValue(id, out var user);
    return Task.FromResult(user);
  }

  public Task<IEnumerable<UserEntity>> GetAllAsync(int pageSize = 10, int pageNumber = 1)
  {
    _logger.LogDebug("Getting all users - Page: {PageNumber}, Size: {PageSize}", pageNumber, pageSize);
    
    var skip = ( pageNumber -1 ) * pageSize;
    var users = _users.Values
      .OrderBy(u => u.Id)
      .Skip(skip)
      .Take(pageSize);
    
    return Task.FromResult(users);
  }

  Task<UserEntity> CreateAsync(UserEntity user)
  {
    _logger.LogDebug("Createing User: {UserName}", user.Name);

    user.Id = _nextId++;
    _users.TryAdd(user.Id, user);


    return Task.FromResult(user);
  }

  Task<UserEntity?> UpdateAsync(UserEntity user)
  {
    _logger.LogDebug("Updating User: {UserId}", user.Id);

    if(_users.TryGetValue(user.Id, out var existingUser))
    {
      existingUser.Name = user.Name;
      existingUser.Email = user.Email;
      existingUser.UpdateTimestamp();
      
      return Task.FromResult<UserEntity?>(existingUser);
    }
    return Task.FromResult<UserEntity?>(null);
  }

  Task<bool> DeleteAsync(int id)
  {
    _logger.LogDebug("Deleting User: {UserId}",id);

    return Task.FromResult(_users.TryRemove(id, out _));
  }

  //

  Task<int> GetTotalCountAsync()
  {
    return Task.FromResult(_users.Count);
  }

  Task<bool> ExistsAsync(int id)
  {
    return Task.FromResult(_users.ContainsKey(id));
  }

  Task<UserEntity?> GetByEmailAsync(string email)
  {
    _logger.LogDebug("Getting user by Email: {Email}", email);
    var user = _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    return Task.FromResult(user);
  }

  Task<UserEntity?> IUserRepository.GetByIdAsync(int id)
  {
      throw new NotImplementedException();
  }

  Task<IEnumerable<UserEntity>> IUserRepository.GetAllAsync(int pageSize, int pageNumber)
  {
      throw new NotImplementedException();
  }

  Task<UserEntity> IUserRepository.CreateAsync(UserEntity user)
  {
      return CreateAsync(user);
  }

  Task<UserEntity?> IUserRepository.UpdateAsync(UserEntity user)
  {
      return UpdateAsync(user);
  }

  Task<bool> IUserRepository.DeleteAsync(int id)
  {
      return DeleteAsync(id);
  }

  Task<int> IUserRepository.GetTotalCountAsync()
  {
      return GetTotalCountAsync();
  }

  Task<bool> IUserRepository.ExistsAsync(int id)
  {
      return ExistsAsync(id);
  }

  Task<UserEntity?> IUserRepository.GetByEmailAsync(string email)
  {
      return GetByEmailAsync(email);
  }
}
