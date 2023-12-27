using BO;
using System;

namespace BlApi;
public interface IEngineer
{
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool> filter);
     public void Update(BO.Engineer item);
    public void Delete(int id);
    public int create(BO.Engineer boEngineer);
  
}