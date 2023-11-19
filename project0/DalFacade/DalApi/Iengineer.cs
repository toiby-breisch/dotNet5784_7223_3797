namespace DalApi;
using DO;
using System;
using System.Collections.Generic;

public interface IEngineer : ICrud<Engineer>
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    List<Engineer>? ReadAll(Func<Engineer, bool> filter);

}
