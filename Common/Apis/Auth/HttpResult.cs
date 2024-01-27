namespace Common.Apis.Auth;

public class HttpResult<T> {
    public bool DidSucceed => Succeeded == ResultType.Success;
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
    Failure,
    Forbidden
}
