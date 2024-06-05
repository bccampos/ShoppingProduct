namespace bruno.Klir.Application.Shopping.Commands
{
    public class CommandResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }

        public static CommandResult<T> Ok(T data) => new CommandResult<T>
        {
            Success = true,
            Data = data
        };

        public static CommandResult<T> Ok() => new CommandResult<T>
        {
            Success = true,
            Data = default
        };

        public static CommandResult<T> Fail() => new CommandResult<T>
        {
            Data = default
        };
    }
}
