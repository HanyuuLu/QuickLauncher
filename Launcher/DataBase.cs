using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Launcher
{
    class DataBase
    {
        private SQLiteConnection conn;
        private SQLiteCommand command;
        private SQLiteDataReader reader;
        /// <summary>
        /// SQLite数据库读写类封装
        /// </summary>
        /// <param name="connString"></param>
        /// <exception cref=""
        public DataBase(string connString)
        {
            while (true)
            {
                try
                {
                    conn = new SQLiteConnection(connString);
                    conn.Open();
                    break;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
