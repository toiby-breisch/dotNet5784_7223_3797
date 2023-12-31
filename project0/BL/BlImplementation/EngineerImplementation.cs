namespace BlImplementation;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;


//(
//,רחאלוןכמתאראש.תויושיה
//ומישבללדבהלןיבתויושיםינותנתויושילתויגול."תוליבקמ"
//,לשמלתושיבסדנהמתבכשבתיגולהעיפומעדימלעהמישמהתיחכונהאוהשדבועהילעהחנהה)איה
//סדנהמשדבועלכלרתויהלעהמישמתחאותואב,(ןמזעדימהזאוהאלעדימיסיסבלשסדנהמאלא
//הנתשמםע, ןמזהןכלוםיצורשכגיצהלסדנהמךירצשפחלתבכשבםינותנהתאהמישמהתיחכונהאוהש
//דבועהילעלבקלוהרובעתא.
internal class EngineerImplementation : BlApi.IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private bool IsValidEmail(string? email)
    {
        bool valid = true;
        try
        {
            var emailAddress = new MailAddress(email ??"");
        }
        catch
        {
            //ליצור שגיאה 
            valid = false;
        }
        return valid;
    }
    private BO.TaskInEngineer GetCurrentTaskOfEngineerActive(int idOfEngineer)
    {
        return null!;
    }
    private IEnumerable<BO.TaskInEngineer>? GetCurrentTaskOfEngineer(int idOfEngineer)
    {
     DalApi.IDal _dal = DalApi.Factory.Get;
     var tasks = _dal.Task.ReadAll(null!);
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
    public int create(BO.Engineer boEngineer)
    {
        
        if (boEngineer?.Id <=0|| boEngineer!.Name ==""||!IsValidEmail(boEngineer?.Email)|| boEngineer?.Cost <= 0)
        {
            //throw new BO.BlAlreadyExistsException();
           
        }
            DO.Engineer doEngineer = new DO.Engineer(boEngineer!.Id, boEngineer.Name, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost);
        try
        {
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new Exception(ex.Message);
            //  throw new BO.BlAlreadyExistsException($"Engineer with ID={boStudent.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
 
        if (GetCurrentTaskOfEngineer(id) == null)
        {
            throw new Exception();
        }
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }    
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new Exception();
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

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?,bool> filter){

        IEnumerable<DO.Engineer> allTasks = _dal.Engineer.ReadAll((Func<DO.Engineer?, bool>)filter)!;
        IEnumerable<BO.Engineer> allTaskinBo= from doEngineer in allTasks
        select new BO.Engineer
        {
             Id = doEngineer.Id,
             Name = doEngineer.Name,
             Email = doEngineer.Email,
             Level = (BO.EngineerExperience)doEngineer.Level,
             Cost = doEngineer.Cost,
             CurrentTask = GetCurrentTaskOfEngineerActive(doEngineer.Id)
         };
        return allTaskinBo;
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer?.Id <= 100000000)
        {
            //throw new BO.BlAlreadyExistsException();
        }

        if (!IsValidEmail(boEngineer?.Email))
        {
            //throw new BO.BlAlreadyExistsException();
        }
        if (boEngineer?.Name == "")
        {
            // throw new DalAlreadyExistsException("");
        }
        if (boEngineer?.Cost <= 0)
        {
           // throw new DalAlreadyExistsException("");
        }
        if (Read(boEngineer!.Id) is not null)
            throw new Exception($"Engineer with ID={boEngineer.Id} already exists");
        Update(boEngineer);
    }
 
}

