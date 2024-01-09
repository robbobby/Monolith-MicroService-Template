namespace Common.IdentityApi;

public class HttpResult<T> {
    public ResultType Succeeded { get; set; }
    public string[] Errors { get; set; } = [];
    public T? Data { get; set; }
}

public class HttpResult {
    public ResultType Succeeded { get; set; }
    public string[] Errors { get; set; } = [];
    public object? Data { get; set; }
}

public enum ResultType {
    Success,
    Conflict,
    Error,
    NotFound,
    Failure
}