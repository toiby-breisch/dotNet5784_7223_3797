namespace DalApi;
using DO;
using System.Xml.Linq;

public interface ITask : ICrud<Task>
{
  public XElement createXelement(Task item);
   
}
