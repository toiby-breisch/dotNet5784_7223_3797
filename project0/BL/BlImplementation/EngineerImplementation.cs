namespace BlImplementation;
using BlApi;
using BO;

internal class Engineer : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Add(int id, string name, int cost, string email)
    {
        DO.Engineer doStudent = new DO.Engineer
       (boStudent.Id, boStudent.Name, boStudent.Alias, boStudent.IsActive, boStudent.BirthDate);
        try
        {
            int idStud = _dal.Student.Create(doStudent);
            return idStud;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = doEngineer.Level,
            Cost = doEngineer.Cost,
            IsActive = doEngineer.IsActive,
           
        };
    }

    public IEnumerable<EngineerInList> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }
}

//public BO.Student? Read(int id)
//{
//    DO.Student? doStudent = _dal.Student.Read(id);
//    if (doStudent == null)
//        throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");
//    return new BO.Student()
//    {
//        Id = id,
//        Name = doStudent.Name,
//        Alias = doStudent.Alias,
//        IsActive = doStudent.IsActive,
//        BirthDate = doStudent.BirthDate,
//        RegistrationDate = doStudent.RegistrationDate,
//        CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
//    };
//}
