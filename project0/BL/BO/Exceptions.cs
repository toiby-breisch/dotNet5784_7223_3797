

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
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}

[Serializable]
public class BlMustNotBeDeleted : Exception
{
    public BlMustNotBeDeleted(string? message) : base(message) { }
}
// לטפל בerrors
//[Serializable]
//public class DalDoesNotExistException : Exception
//{
//    public DalDoesNotExistException(string? message) : base(message) { }
//}
//[Serializable]
////<summary>
////Dal already existException
////</summary>
//public class DalAlreadyExistsException : Exception
//{
//    public DalAlreadyExistsException(string? message) : base(message) { }
//}
////<summary>
////Dal deletion impossible
////</summary>
//[Serializable]
//public class DalDeletionImpossible : Exception
//{
//    public DalDeletionImpossible(string? message) : base(message) { }
//}
//public class DalXMLFileLoadCreateException : Exception
//{
//    public DalXMLFileLoadCreateException(string? message) : base(message) { }
//}