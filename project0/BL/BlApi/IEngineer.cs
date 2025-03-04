﻿using BO;
using System;

namespace BlApi;
public interface IEngineer
{
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool> ?filter=null);
     public void Update(BO.Engineer item);
    public void Delete(int id);
    public int Create(BO.Engineer boEngineer);
  
}