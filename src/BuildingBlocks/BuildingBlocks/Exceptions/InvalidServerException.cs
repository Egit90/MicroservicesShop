namespace BuildingBlocks.Exceptions;

public sealed class InternalServerException : Exception
{
    public string? Details;

    public InternalServerException(string message) : base(message) { }
    public InternalServerException(string message, string details) : base(message)
    {
        this.Details = details;
    }
}