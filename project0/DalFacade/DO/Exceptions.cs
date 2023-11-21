//<summary>
//Exception types
//</summary>
namespace DO;
//<summary>
//Dal does not existException
//</summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
[Serializable]
//<summary>
//Dal already existException
//</summary>
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
//<summary>
//Dal deletion impossible
//</summary>
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}
