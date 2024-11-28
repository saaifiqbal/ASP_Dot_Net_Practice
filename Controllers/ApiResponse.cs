namespace asp_net_ecommerce_web_api.Controllers {
    public class ApiResponse<T> {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        // Private constructor to initialize properties
        private ApiResponse(bool success, T? data = default, string message = "", List<string>? errors = null) {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        // Factory method for success response
        public static ApiResponse<T> SuccessResponse(T data, string message = "") {
            return new ApiResponse<T>(true, data, message, null);
        }

        // Factory method for error response
        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null) {
            return new ApiResponse<T>(false, default, message, errors);
        }
    }
}
