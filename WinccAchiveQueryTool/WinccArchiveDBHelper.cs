using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ruifei.Common;
using Ruifei.Models;

namespace Ruifei.AppMain
{
    public class WinccArchiveDBHelper
    {
        public WinccArchiveDBHelper()
        {
        }
        public WinccArchiveDBHelper(string serverName)
        {
            ServerName = serverName;
        }

        public bool UseUserLogin { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; } = ".\\WINCC";

        /// <summary>
        /// 用于查询本机
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetDabaseNames()
        {
            SqlConnection conn = new SqlConnection($"Data Source={ServerName};Integrated Security=SSPI");
            var DBNameList = (await conn.QueryAsync<string>("select name from master..sysdatabases")).ToList();

            return DBNameList.Where(db => db.StartsWith("CC") && db.EndsWith("R")).ToList();
        }

        /// <summary>
        /// 查询远程机
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<List<string>> GetDabaseNames(string uid, string password)
        {
            SqlConnection conn = new SqlConnection($"Data Source={ServerName};User ID={uid};Password={password};");
            var DBNameList = (await conn.QueryAsync<string>("select name from master..sysdatabases")).ToList();

            return DBNameList.Where(db => db.StartsWith("CC") && db.EndsWith("R")).ToList();
        }

        public async Task<List<Archive>> GetAchiveNames(string database)
        {
            SqlConnection conn = new SqlConnection($"Data Source={ServerName};Initial Catalog={database};Integrated Security=SSPI");
            List<Archive> DBNameList = (await conn.QueryAsync<Archive>("SELECT * FROM [Archive]")).ToList();

            return DBNameList;
        }

        public async Task<List<WinccData>> QueryWinccData(string achivename, string database, DateTime starttime, DateTime endtime)
        {
            OleDbConnection conn = new OleDbConnection($"Provider=WinCCOLEDBProvider.1;Catalog={database};Data Source={ServerName}");
            await conn.OpenAsync();
            var list = await conn.QueryAsync<WinccData>($"TAG:R,'{achivename}','{starttime.ToSqlFomat()}','{endtime.ToSqlFomat()}'");
            conn.Close();

            return list.ToList();
        }

        public async Task<List<WinccData>> QueryWinccData(OleDbConnection conn, string achivename, DateTime starttime, DateTime endtime)
        {
            var list = await conn.QueryAsync<WinccData>($"TAG:R,'{achivename}','{starttime.ToSqlFomat()}','{endtime.ToSqlFomat()}'");
            return list.ToList();
        }

        public async Task<List<WinccData>> QueryWinccData(string achivename, string database, DateTime starttime, DateTime endtime, int interop, int interopType)
        {
            OleDbConnection conn = new OleDbConnection($"Provider=WinCCOLEDBProvider.1;Catalog={database};Data Source={ServerName}");
            await conn.OpenAsync();
            var list = await conn.QueryAsync<WinccData>($"TAG:R,'{achivename}','{starttime.ToSqlFomat()}','{endtime.ToSqlFomat()}','TIMESTEP={interop},{interopType}'");
            conn.Close();

            return list.ToList();
        }

        public async Task<List<WinccData>> QueryWinccData(OleDbConnection conn, string achivename, DateTime starttime, DateTime endtime, int interop, int interopType)
        {
            var list = await conn.QueryAsync<WinccData>($"TAG:R,'{achivename}','{starttime.ToSqlFomat()}','{endtime.ToSqlFomat()}','TIMESTEP={interop},{interopType}'");
            return list.ToList();
        }

        public async Task<List<AlarmLog>> QueryAlermLog(string database, DateTime starttime, DateTime endtime)
        {
            OleDbConnection conn = new OleDbConnection($"Provider=WinCCOLEDBProvider.1;Catalog={database};Data Source={ServerName}");
            await conn.OpenAsync();
            var list = await conn.QueryAsync<AlarmLog>($"ALARMVIEW:SELECT * FROM AlgViewDeu Where DateTime>'{starttime.ToStandardString()}' AND DateTime<'{endtime.ToStandardString()}'");
            conn.Close();

            return list.ToList();
        }
    }
}
