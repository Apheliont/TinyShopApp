﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLib.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> GetData<T, U>(string storedProcedure, U parameters);
        Task SaveData<U>(string storedProcedure, U parameters);
    }
}