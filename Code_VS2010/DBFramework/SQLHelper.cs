#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：11/20/2012 11:50:55 AM 
* File name：SQLHelper 
* Version：V1.0.1
* Company: APHT
* 
* ======================================================================== 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBFramework.Entities;
using System.Data.SqlClient;
using System.Reflection;
using DBFramework.Entities.Attribute;
using System.Xml;
using System.Reflection.Emit;
using System.Linq.Expressions;
using DBFramework.Reader;
using System.Threading;
using System.Diagnostics;

namespace DBFramework
{
    public class SQLHelper
    {
        public static string ConnectionString = null;

        public static bool IsSetup = false;

        public static void Setup(string connectionString)
        {
            ConnectionString = connectionString;

            if (Connected())
                IsSetup = true;
            else
                IsSetup = false;
        }

        public static bool Connected()
        {
            bool success = false;
            if (ConnectionString == null) return false;

            var thread = new Thread(() =>
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        success = true;
                    }
                    catch (SqlException)
                    {
                    }
                    catch (ThreadAbortException)
                    {
                    }
                }
            });

            thread.IsBackground = true;
            var sw = Stopwatch.StartNew();
            thread.Start();

            var timeout = TimeSpan.FromMilliseconds(1200);
            while (sw.Elapsed < timeout && !success)
                thread.Join(TimeSpan.FromMilliseconds(200));
            sw.Stop();

            return success;
        }

        public static bool CreateMonthlyTable<T>(DateTime time) where T : IMonthly
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(Reflector.GetMonthlyCreateTableSQL<T>(time), conn);
                    object drd = cmd.ExecuteScalar();
                    if (drd != null)
                    {
                        return Convert.ToInt32(drd) > 0;
                    }
                    throw new System.Exception("Create table failed.");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void CreateMonthlyTableIndex<T>(DateTime time) where T : IMonthly
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(Reflector.GetMonthlyCreateIndexSQL<T>(time), conn);
                    cmd.ExecuteNonQuery();
                    throw new System.Exception("Create table failed.");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool IsExistMonthlyTable<T>(DateTime time) where T : IMonthly
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(Reflector.GetMonthlyCheckExistTable<T>(time), conn);
                    object drd = cmd.ExecuteScalar();
                    if (drd != null)
                    {
                        return Convert.ToInt32(drd) > 0;
                    }
                    return false;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> GetMonthlyEntites<T>(DateTime datetime, string conditions) where T : IMonthly, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                string sqlStr = Reflector.GetMonthlySelectSQL<T>(datetime).Replace(";", " ");
                if (conditions != null)
                    sqlStr += " " + conditions;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    //cmd.CommandTimeout = 1;
                    SqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        T t = new T();
                        Type type = typeof(T);
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                            if (navigationType != null) continue;

                            if (Convert.IsDBNull(drd[property.Name]))
                            {
                                property.SetValue(t, null, null);
                            }
                            else
                            {
                                property.SetValue(t, GetPropertyValue(property, drd[property.Name].ToString()), null);
                            }
                        }
                        entities.Add(t);
                    }
                    return entities;
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception(ex.Message + ",sql=" + sqlStr);
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static T CreateEntity<T>(T t) where T : IEntity
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sqlStr = Reflector.GetInsertSQL(t);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    object drd = cmd.ExecuteScalar();
                    if (drd != null)
                    {
                        Reflector.SetIdentity(t, Convert.ToInt32(drd));
                        return t;
                    }
                    throw new System.Exception("Create entity failed.");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> CreateEntities<T>(List<T> t) where T : IEntity
        {
            if (t.Count == 0) return t;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in t)
                    {
                        builder.Append(Reflector.GetInsertSQL(item));
                    }
                    string sqlStr = builder.ToString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    object drd = cmd.ExecuteScalar();
                    int fristId = 0;

                    if (drd != null)
                    {
                        fristId = Convert.ToInt32(drd);
                        if (fristId != 0)
                        {
                            foreach (var item in t)
                            {
                                Reflector.SetIdentity(item, fristId++);
                            }
                        }
                        return t;
                    }
                    throw new System.Exception("Create entity failed.");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool UpdateEntity<T>(T t) where T : IEntity
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sqlStr = Reflector.GetUpdateSQL(t);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool UpdateEntities<T>(List<T> t) where T : IEntity
        {
            if (t.Count == 0) return true;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in t)
                    {
                        builder.Append(Reflector.GetUpdateSQL(item));
                    }
                    string sqlStr = builder.ToString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    int r = cmd.ExecuteNonQuery();
                    return r == t.Count;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void DeleteEntity<T>(string id) where T : IEntity,new()
        {
            List<T> ts = GetEntities<T>(" Id="+id.ToString());
            if (ts.Count > 0)
            {
                DeleteEntity<T>(ts[0]);
            }
        }

        public static void DeleteEntity<T>(T t) where T : IEntity
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sqlStr = Reflector.GetDeleteSQL(t);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    SqlDataReader drd = cmd.ExecuteReader();
                    return;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> GetTopEntities<T>(string conditions, int topCount) where T : IEntity, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                try
                {
                    string sqlStr = Reflector.GetTopSelectSQL<T>(topCount);
                    if (conditions != null)
                        sqlStr += " " + conditions;

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    SqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        T t = new T();
                        Type type = typeof(T);
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                            if (navigationType != null) continue;

                            if (Convert.IsDBNull(drd[property.Name]))
                            {
                                property.SetValue(t, null, null);
                            }
                            else
                            {
                                property.SetValue(t, GetPropertyValue(property, drd[property.Name].ToString()), null);
                            }
                        }
                        entities.Add(t);
                    }
                    return entities;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> GetEntities<T>(string conditions) where T : IEntity, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                string sqlStr = Reflector.GetSelectSQL<T>().Replace(";", " ");
                if (conditions != null)
                    sqlStr += " WHERE " + conditions;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    //cmd.CommandTimeout = 1;
                    SqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        T t = new T();
                        Type type = typeof(T);
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                            if (navigationType != null) continue;

                            if (Convert.IsDBNull(drd[property.Name]))
                            {
                                property.SetValue(t, null, null);
                            }
                            else
                            {
                                property.SetValue(t, GetPropertyValue(property, drd[property.Name].ToString()), null);
                            }
                        }
                        entities.Add(t);
                    }
                    return entities;
                }
                catch (System.Exception ex)
                {
                    throw new System.Exception(ex.Message + ",sql=" + sqlStr);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> GetEntities<T>() where T : IEntity, new()
        {
            try
            {
                return GetEntities<T>(null);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static List<T> GetEntitiesByQuery<T>(string query) where T : IEntity, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader drd = cmd.ExecuteReader();
                    while (drd.Read())
                    {
                        T t = new T();
                        Type type = typeof(T);
                        PropertyInfo[] properties = type.GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                            if (navigationType != null) continue;

                            if (Convert.IsDBNull(drd[property.Name]))
                            {
                                property.SetValue(t, null, null);
                            }
                            else
                            {
                                property.SetValue(t, GetPropertyValue(property, drd[property.Name].ToString()), null);
                            }
                        }
                        entities.Add(t);
                    }
                    return entities;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static int ExecuteNonQuery(string sqlStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    int result = cmd.ExecuteNonQuery();
                    return result;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static DataReader ExecuteReader(string sqlStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    return new DataReader(conn, cmd.ExecuteReader());
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                }
            }
        }

        public static object ExecuteScalar(string sqlStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    return cmd.ExecuteScalar();
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static string ExecuteXmlReader(string sqlStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    StringBuilder xmlString = new StringBuilder();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    XmlReader reader = cmd.ExecuteXmlReader();
                    reader.Read();
                    while (reader.ReadState != ReadState.EndOfFile)
                    {
                        xmlString.Append(reader.ReadOuterXml());
                    }
                    reader.Close();
                    return xmlString.ToString();
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static List<T> GetEntitiesByXML<T>(string xmlString) where T : IEntity, new()
        {
            try
            {
                Type type = typeof(T);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);

                List<T> entities = new List<T>();
                XmlNode oNode = doc.DocumentElement;
                XmlNodeList oList = oNode.ChildNodes;

                foreach (XmlNode node in oList)
                {
                    if (node.Name != type.Name) continue;
                    T o = new T();

                    if (node.HasChildNodes)
                    {
                        XmlNodeList properties = node.ChildNodes;
                        foreach (XmlNode property in properties)
                        {
                            PropertyInfo pInfo = type.GetProperty(property.Name);
                            pInfo.SetValue(o, GetPropertyValue(pInfo, property.InnerText), null);
                        }
                    }
                    entities.Add(o);
                }
                return entities;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static object GetPropertyValue(PropertyInfo info, string stringValue)
        {
            try
            {
                if (stringValue == "")
                    return null;

                object value = null;
                if (info.PropertyType.Equals(typeof(string)))
                {
                    value = stringValue;
                }
                else if (info.PropertyType.Equals(typeof(int)))
                {
                    value = Convert.ToInt32(stringValue);
                }
                else if (info.PropertyType.Equals(typeof(int?)))
                {
                    if (!Convert.IsDBNull(stringValue))
                    {
                        value = Convert.ToInt32(stringValue);
                    }
                }
                else if (info.PropertyType.Equals(typeof(decimal)))
                {
                    value = Convert.ToDecimal(stringValue);
                }
                else if (info.PropertyType.Equals(typeof(DateTime)))
                {
                    value = Convert.ToDateTime(stringValue);
                }
                else if (info.PropertyType.Equals(typeof(DateTime?)))
                {
                    if (!Convert.IsDBNull(stringValue))
                    {
                        value = Convert.ToDateTime(stringValue);
                    }
                }
                else if (info.PropertyType.Equals(typeof(double)))
                {
                    value = Convert.ToDouble(stringValue);
                }
                else if (info.PropertyType.Equals(typeof(double?)))
                {
                    if (!Convert.IsDBNull(stringValue))
                    {
                        value = Convert.ToDouble(stringValue);
                    }
                }
                else if (info.PropertyType.Equals(typeof(bool)))
                {
                    value = Convert.ToBoolean(stringValue);
                }
                else if (info.PropertyType.Equals(typeof(bool?)))
                {
                    if (!Convert.IsDBNull(stringValue))
                    {
                        value = Convert.ToBoolean(stringValue);
                    }
                }
                else
                {
                    return null;
                }

                return value;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
