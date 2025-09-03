namespace CustomGrpcBackend.Models.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    // Convert to gRPC User message
    public Protos.User ToGrpcUser()
    {
        return new Protos.User
        {
            Id = Id,
            Name = Name,
            Email = Email,
            CreatedAt = CreatedAt.ToString("O"),
            UpdatedAt = UpdatedAt.ToString("O")
        };
    }

    // Create from gRPC CreateUserRequest
    public static UserEntity FromCreateRequest(Protos.CreateUserRequest request)
    {
        return new UserEntity
        {
            Name = request.Name,
            Email = request.Email
        };
    }

    // Update from gRPC UpdateUserRequest
    public void UpdateFromRequest(Protos.UpdateUserRequest request)
    {
        Name = request.Name;
        Email = request.Email;
        UpdateTimestamp();
    }
}
