using System.Runtime.Serialization;

namespace Exceptions;

public class FileDetectorException : Exception
{
    public FileDetectorException()
    {
    }

    public FileDetectorException(string? message) : base(message)
    {
    }

    public FileDetectorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected FileDetectorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}