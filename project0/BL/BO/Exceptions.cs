namespace BO;
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
    : base(message, innerException) { }
}
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string? massage, Exception innerException) : base(massage, innerException) { }
}

[Serializable]
public class BlNullOrNotIllegalPropertyException : Exception
{
    public BlNullOrNotIllegalPropertyException(string? message) : base(message) { }
}

[Serializable]
public class BlCantReadUnActive : Exception
{
    public BlCantReadUnActive(string? message) : base(message) { }
}
[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message) : base(message) { }
    public BlDeletionImpossible(string message, Exception innerException)
    : base(message, innerException) { }
}
