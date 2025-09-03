namespace CustomGrpcBackend.Models.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; }
    public string RequestId { get; set; } = string.Empty;

    public ApiResponse()
    {
        Timestamp = DateTime.UtcNow;
        RequestId = Guid.NewGuid().ToString();
    }

    public static ApiResponse<T> SuccessResult(T data, string message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResult(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default(T)
        };
    }
}

public class HealthCheckResponse
{
    public string Status { get; set; } = "Healthy";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Protocol { get; set; } = string.Empty;
    public string Server { get; set; } = "CustomGrpcBackend";
    public string Version { get; set; } = "1.0.0";
}
