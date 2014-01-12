using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using web;
using System.Collections;
using System.IO;
namespace web1.install
{
    //参考：ASP.NET(C#)执行.SQL脚本实现数据库建表
    //http://blog.csdn.net/FuCity/article/details/2462295

    public partial class install : System.Web.UI.Page
    {
        string logFile = "log.txt";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (File.Exists(Server.MapPath(logFile)))
                {
                    Response.Write("请删除文件:install/" + logFile + "，然后再执行此程序。");
                    Response.End();
                }
            }
        }
        public static ArrayList ExecuteSqlFile(string varFileName)
        {
            //
            // TODO:读取.sql脚本文件
            //
            StreamReader sr = File.OpenText(varFileName);//传入的是文件路径及完整的文件名
            ArrayList alSql = new ArrayList();           //每读取一条语名存入ArrayList
            string commandText = "";
            string varLine = "";
            while (sr.Peek() > -1)
            {
                varLine = sr.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO")
                {
                    commandText += varLine;
                    commandText += " ";
                }
                else
                {
                    alSql.Add(commandText);
                    commandText = "";
                }
            }

            sr.Close();
            return alSql;
        }

        protected void btnInstall_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();
            string user = txtDbUsername.Text.Trim();
            string pwd = txtDbPwd.Text.Trim();
            string dbName = txtDbName.Text.Trim(), dbName2 = txtDbName2.Text.Trim();
            string dbVersion = ddlVersion.SelectedValue;
            string constr = "";
            //链接数据库，执行脚本
            if (dbName2 != "")
            {
                constr = "data source=" + ip + "\\" + dbName2 + ";uid=" + user + ";pwd=" + pwd + ";database=" + dbName + "";    // 定义链接字符窜
            }
            else
            {
                constr = "data source=" + ip + ";uid=" + user + ";pwd=" + pwd + ";database=" + dbName + "";
            }
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            //ArrayList Lists = ExecuteSqlFile(Server.MapPath("../DB/MianDB.sql")); //调用ExecuteSqlFile()方法，反回 ArrayList对象;

            ArrayList Lists = ExecuteSqlFile(Server.MapPath("../DB/" + dbVersion)); //调用ExecuteSqlFile()方法，反回 ArrayList对象;


            string teststr;                           //定义遍历ArrayList 的变量;
            foreach (string varcommandText in Lists)
            {
                teststr = varcommandText;             //遍历并符值;
                //Response.Write(teststr + "|@|<br>");
                cmd.CommandText = teststr;            //为SqlCommand赋Sql语句;
                cmd.ExecuteNonQuery();                //执行
            }
            conn.Close();
            


            
            TxtIO.writeFile(logFile, "安装完成");
            if (!TxtIO.writeFile("../DB/conn.txt", constr))
            {
                Response.Write("conn写入失败<br>");
            }
            Response.Write("执行完毕，安装完成，<a href='../Admin/Login.aspx'>进入后台管理</a>");
            Response.End();
        }
    }
}
