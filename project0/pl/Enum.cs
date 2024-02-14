using System.Collections.Generic;
using System;
using System.Collections;

namespace PL;
/// <summary>
/// Engeneer collection of level
/// </summary>
internal class EngineersCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
            (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
/// <summary>
/// Task collection of status
/// </summary>
internal class TasksCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
            (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
