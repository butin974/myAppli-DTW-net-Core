﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTW_Repository.config
{
    public interface IBaseRepository
    {
        public MySqlConnection OpenConnection();
    }
}
