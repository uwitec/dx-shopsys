using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBFramework.Entities;
using System.Reflection;
using DBFramework.Entities.Attribute;
using System.Xml;
using DBFramework.Reader;
using System.Threading;
using System.Diagnostics;

namespace DBFramework
{
    public class SQLDHelper
    {
        public string ConnectionString = null;

        public bool IsSetup = false;

        public void Setup(string connectionString)
        {
            ConnectionString = connectionString;
            if (Connected())
                IsSetup = true;
            else
                IsSetup = false;
        }

        public bool Connected()
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

        public T CreateEntity<T>(T t) where T : IEntity
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public T CreateIdentityEntity<T>(T t) where T : IEntity
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sqlStr = Reflector.GetIdentityInsertSQL(t);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    object drd = cmd.ExecuteNonQuery();
                    if (drd != null)
                    {
                        return t;
                    }
                    throw new System.Exception("Create identity entity failed.");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public T UpdateEntity<T>(T t) where T : IEntity
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    string sqlStr = Reflector.GetUpdateSQL(t);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    SqlDataReader drd = cmd.ExecuteReader();
                    return t;
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public void DeleteEntity<T>(T t) where T : IEntity
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public List<T> GetTopEntities<T>(string conditions, int topCount) where T : IEntity, new()
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public List<T> GetEntities<T>(string conditions) where T : IEntity, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                try
                {
                    string sqlStr = Reflector.GetSelectSQL<T>();
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public List<T> GetEntities<T>() where T : IEntity, new()
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

        public List<T> GetEntitiesByQuery<T>(string query) where T : IEntity, new()
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
                    SqlConnection.ClearAllPools();
                }
            }
        }


        public T CreateMonthlyEntity<T>(T t) where T : IMonthly
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        string sqlStr = Reflector.GetMonthlyInsertSQL(t);
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
                        SqlConnection.ClearAllPools();
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public List<T> GetMonthlyEntities<T>(DateTime time, string conditions) where T : IMonthly, new()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                List<T> entities = new List<T>();
                try
                {
                    string sqlStr = Reflector.GetMonthlySelectSQL<T>(time);
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public int ExecuteNonQuery(string sqlStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    return cmd.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public DataReader ExecuteReader(string sqlStr)
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public object ExecuteScalar(string sqlStr)
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public string ExecuteXmlReader(string sqlStr)
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
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public List<T> GetEntitiesByXML<T>(string xmlString) where T : IEntity, new()
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
            finally
            {

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
