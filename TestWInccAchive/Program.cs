using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TestWInccAchive
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //{
                //    OleDbConnection conn = new OleDbConnection("Provider=WinCCOLEDBProvider.1;Catalog=CC_OS_1__20_12_11_11_40_31R;Data Source=10.0.0.32\\WINCC");
                //    conn.Open();
                //    var list = conn.Query<WinccData>("TAG:R,'SystemArchive\\DO110T1/MonAnalog.PV#Value','2020-12-16 10:00:00.000','2020-12-18 00:00:00.000'");

                //    conn.Close();
                //    foreach (WinccData item in list)
                //    {
                //        Console.WriteLine(item.RealValue);
                //    }
                //}
                {
                    //SqlConnection conn = new SqlConnection("Data Source=.\\WINCC;Initial Catalog=CC_OS_1__20_12_11_11_40_31R;Integrated Security=SSPI");
                    //var archivlist = conn.Query<Archive>("SELECT *  FROM [Archive]").ToList();
                    //Console.WriteLine(archivlist.Count);
                    //SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=CC_OS_1__20_12_04_13_48_36R;Integrated Security=SSPI");
                    //var archivlist = conn.Query<Archive>("SELECT *  FROM [Archive]");

                    SqlConnection conn = new SqlConnection("Data Source=.\\WINCC;Integrated Security=SSPI");
                    List<string> DBNameList = conn.Query<string>("select name from master..sysdatabases").ToList();
                    Console.WriteLine(DBNameList.Count);
                }
                Console.ReadLine();

            }
            catch (OleDbException oledbex)
            {
                if (oledbex.Message.Contains("E_INVALIDARG(0x80070057)"))
                {
                    Console.WriteLine("E_INVALIDARG");
                }
                else if (oledbex.Message.Contains("DB_E_ERRORSINCOMMAND(0x80040E14)"))
                {
                    Console.WriteLine("ERRORSINCOMMAND");
                }
                else
                {
                    throw;
                }

            }
        }
    }
}
