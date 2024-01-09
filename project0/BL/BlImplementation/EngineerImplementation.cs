namespace BlImplementation;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;
/// <summary>
/// Engineer Implementation
/// </summary>
internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// The function create a new engineer
    /// </summary>
    /// <param name="boEngineer"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullOrNotIllegalPropertyException"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public int create(BO.Engineer boEngineer)
    {
        
        if (boEngineer?.Id <=0|| boEngineer!.Name ==""||!isValidEmail(boEngineer?.Email)|| boEngineer?.Cost <= 0)
        {
            throw new BO.BlNullOrNotIllegalPropertyException("There are valuse null or not illegal");
           
        }
            DO.Engineer doEngineer = new DO.Engineer(boEngineer!.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
             throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }
    /// <summary>
    /// THe function deletes an engineer.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDeletionImpossible"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        if (GetCurrentTaskOfEngineer(id) != null||_dal.Engineer.Read(id)==null)
        {
            throw new BO.BlDeletionImpossible($"Engineer with id={id} is impossible to delete");
        }
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex) {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} is not exists", ex);
        }    
    }
    /// <summary>
    /// The function read an Engineer.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Engineer? </returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Enginir with id={id}is not exists");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer!.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            CurrentTask=GetCurrentTaskOfEngineerActive(id)

        };
    }
    /// <summary>
    /// The function reads all the engineers.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<BO.Engineer></returns>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?,bool> ?filter=null){
                                    
        IEnumerable<BO.Engineer> allTasks = from doEngineer in _dal.Engineer.ReadAll()
                                            select new BO.Engineer
                                            {
                                                Id = doEngineer.Id,
                                                Name = doEngineer.Name,
                                                Email = doEngineer.Email,
                                                Level = (BO.EngineerExperience)doEngineer.Level,
                                                Cost = doEngineer.Cost,
                                                CurrentTask = GetCurrentTaskOfEngineerActive(doEngineer.Id)
                                            };
        return filter == null ? allTasks : allTasks.Where(filter);
 
    }
    /// <summary>
    /// The function updates an engineer.
    /// </summary>
    /// <param name="boEngineer"></param>
    /// <exception cref="BO.BlNullOrNotIllegalPropertyException"></exception>
    /// <exception cref="Exception"></exception>
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer?.Id <= 100000000|| !isValidEmail(boEngineer?.Email)|| boEngineer?.Name == ""|| boEngineer?.Cost <= 0)
        {
            throw new BO.BlNullOrNotIllegalPropertyException("There are valuse null or not illegal");
        }

        if (Read(boEngineer!.Id) is not null)
            throw new Exception($"Engineer with ID={boEngineer.Id} already exists");
        Update(boEngineer);
    }
    /// <summary>
    /// THe function gets current actives's taskInEngineer.
    /// </summary>
    /// <param name="idOfEngineer"></param>
    /// <returns>  BO.TaskInEngineer?</returns>
    private BO.TaskInEngineer? GetCurrentTaskOfEngineerActive(int idOfEngineer)
    {
        var tasks = _dal.Task.ReadAll();
        var taskInEngineer=
          (from task in tasks
           where task.Engineerid == idOfEngineer && task.IsActive == true
           select new BO.TaskInEngineer
           {
               Id = task.Id,
               Alias = task.Description,
           }).FirstOrDefault();
        return taskInEngineer;
    }
    /// <summary>
    /// THe function gets current taskInEngineer.
    /// </summary>
    /// <param name="idOfEngineer"></param>
    /// <returns></returns>
    private IEnumerable<BO.TaskInEngineer>? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
        var tasks = _dal.Task.ReadAll();
        IEnumerable<BO.TaskInEngineer>? taskInEngineer =
          (from task in tasks
           where task.Engineerid == idOfEngineer
           select new BO.TaskInEngineer
           {
               Id = task.Id,
               Alias = task.Description,
           });
        return taskInEngineer;
    }
    /// <summary>
    /// The function checks if the email is valid.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlNullOrNotIllegalPropertyException"></exception>
    private bool isValidEmail(string? email)
    {
        bool valid = true;
        try
        {
            var emailAddress = new MailAddress(email ?? "");
        }
        catch
        {
            throw new BO.BlNullOrNotIllegalPropertyException($"This property {email} isIllegal");
        }
        return valid;
    }
}

