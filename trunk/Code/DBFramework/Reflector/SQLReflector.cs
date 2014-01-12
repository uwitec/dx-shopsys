#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：11/20/2012 9:25:54 AM 
* File name：SQLReflector 
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
using System.Reflection;
using DBFramework.Entities.Attribute;
using DBFramework.Exception;

namespace DBFramework
{
    public static class Reflector
    {
        public static string GetInsertSQL<T>(T t) where T : IEntity
        {
            try
            {
                String fileds = null;
                String values = null;

                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                bool hasPrimaryKey = false;
                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                    if (propertyType != null && propertyType.IsPrimaryKey)
                    {
                        hasPrimaryKey = true;
                        continue;
                    }
                    Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                    if (navigationType != null) continue;

                    fileds += property.Name + ",";
                    object ProperyValue = property.GetValue(t, null);
                    if (ProperyValue == null)
                        values += "NULL,";
                    else
                        values += "'" + ProperyValue.ToString() + "',";
                }

                fileds = fileds.Substring(0, fileds.Length - 1);
                values = values.Substring(0, values.Length - 1);

                string SelectId = "";
                if (hasPrimaryKey)
                {
                    SelectId = "SELECT   @@IDENTITY";
                }

                return String.Format("INSERT INTO {0} ({1}) VALUES({2}) " + SelectId + ";", type.Name, fileds, values);
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetSelectSQL<T>() where T : IEntity
        {
            try
            {
                StringBuilder SqlString = new StringBuilder();

                Type type = typeof(T);
                SqlString.Append("SELECT * FROM " + type.Name + "");
                SqlString.Append(";");
                return SqlString.ToString();
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetUpdateSQL<T>(T t) where T : IEntity
        {
            try
            {
                string PrimaryKey = null;
                string PrimaryKeyValue = null;
                StringBuilder SqlString = new StringBuilder();

                Type type = typeof(T);
                SqlString.Append("UPDATE "+ type.Name +" SET ");

                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                    if (propertyType != null && propertyType.IsPrimaryKey)
                    {
                        PrimaryKey = property.Name;
                        PrimaryKeyValue = property.GetValue(t, null).ToString();
                        continue;
                    }

                    Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                    if (navigationType != null) continue;

                    else
                    {
                        string valueString = "NULL,";
                        object ProperyValue = property.GetValue(t, null);
                        if (ProperyValue != null)
                            valueString = "'" + ProperyValue.ToString() + "',";

                        SqlString.Append(" " + property.Name + " = " + valueString + "");
                    }
                }

                SqlString = SqlString.Remove(SqlString.Length - 1, 1);
                SqlString.Append(" WHERE " + PrimaryKey + " = '" + PrimaryKeyValue + "'");
                SqlString.Append(";");

                return SqlString.ToString();
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetDeleteSQL<T>(T t) where T : IEntity
        {
            try
            {
                StringBuilder SqlString = new StringBuilder();

                Type type = typeof(T);
                SqlString.Append("DELETE FROM "+ type.Name +" WHERE ");

                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                    if (propertyType != null && propertyType.IsPrimaryKey)
                    {
                        SqlString.Append(""+ property.Name +" = ");
                        SqlString.Append("'"+ property.GetValue(t, null).ToString() +"'");
                        SqlString.Append(";");
                        return SqlString.ToString();
                    }
                }
                throw new ReflectorException("Can not find PrimaryKey.");
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static void SetIdentity(Object t, int Identity)
        {
            try
            {
                Type type = t.GetType();

                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                    if (propertyType != null && propertyType.IsPrimaryKey)
                    {
                        property.SetValue(t, Identity, null);
                        return;
                    }
                }

                throw new ReflectorException("Can npt find Identity.");
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetTopSelectSQL<T>(int topCount) where T : IEntity
        {
            try
            {
                StringBuilder SqlString = new StringBuilder();

                Type type = typeof(T);
                SqlString.Append("SELECT TOP " + topCount + " * FROM " + type.Name + "");
                return SqlString.ToString();
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetIdentityInsertSQL<T>(T t) where T : IEntity
        {
            try
            {
                String fileds = null;
                String values = null;

                StringBuilder query = new StringBuilder();

                Type type = typeof(T);
                query.Append("SET IDENTITY_INSERT " + type.Name + " ON;");
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                    if (navigationType != null) continue;

                    fileds += property.Name + ",";
                    object ProperyValue = property.GetValue(t, null);
                    if (ProperyValue == null)
                        values += "NULL,";
                    else
                        values += "'" + ProperyValue.ToString() + "',";
                }

                fileds = fileds.Substring(0, fileds.Length - 1);
                values = values.Substring(0, values.Length - 1);

                query.Append(String.Format("INSERT INTO {0} ({1}) VALUES({2});", type.Name, fileds, values));
                query.Append("SET IDENTITY_INSERT " + type.Name + " OFF;");
                return query.ToString();
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetCheckExistTable<T>(T t) where T : IEntity
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT COUNT(*) FROM SYSOBJECTS WHERE ID = OBJECT_ID('"+ t.GetType().Name +"')");
            return sqlBuilder.ToString();
        }

        public static string GetCreateTableSQL<T>(T t) where T: IEntity
        {
            StringBuilder fileds = new StringBuilder();
            bool isPrimary = false;

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                if (navigationType != null) continue;

                fileds.AppendFormat("[{0}]", property.Name);
                fileds.AppendFormat(" [{0}]", TypeConverter.GetSQLType(property.PropertyType));

                PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                if (propertyType != null && propertyType.IsPrimaryKey)
                {
                    isPrimary = true;
                    fileds.AppendFormat(" {0}",PrimaryConverter.GetDefaultPrimaryFiledString());
                }

                if (TypeConverter.IsNullableType(property.PropertyType))
                {
                    fileds.AppendFormat(" {0}", DefaultValueConverter.GetDefaultNullValue());
                }
                else
                {
                    fileds.AppendFormat(" {0}", DefaultValueConverter.GetDefaultNOTNullValue());
                }
            }

            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendFormat("CREATE TABLE {0} ({1}) ", type.Name, fileds.ToString());
            if (isPrimary)
                commandBuilder.Append(PrimaryConverter.GetDefaultPrimaryTableEndString());

            return commandBuilder.ToString();
        }


        public static string GetMonthlyInsertSQL<T>(T t) where T : IMonthly
        {
            try
            {
                String fileds = null;
                String values = null;
                string month = "";

                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                    if (propertyType != null && propertyType.IsPrimaryKey) continue;

                    Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                    if (navigationType != null) continue;

                    fileds += property.Name + ",";
                    object ProperyValue = property.GetValue(t, null);
                    if (ProperyValue == null)
                        values += "NULL,";
                    else
                        values += "'" + ProperyValue.ToString() + "',";

                    if (property.Name == "TimeStamp")
                    {
                        DateTime timeStampeValue = Convert.ToDateTime(property.GetValue(t, null));
                        month = Convert.ToString(timeStampeValue.Year).PadLeft(4, '0');
                        month += Convert.ToString(timeStampeValue.Month).PadLeft(2, '0');
                    }
                }

                fileds = fileds.Substring(0, fileds.Length - 1);
                values = values.Substring(0, values.Length - 1);

                return String.Format("INSERT INTO {0} ({1}) VALUES({2})  SELECT   @@IDENTITY;", type.Name + month, fileds, values);
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetMonthlySelectSQL<T>(DateTime dateTime) where T : IMonthly
        {
            try
            {
                StringBuilder SqlString = new StringBuilder();
                string monthString = Convert.ToString(dateTime.Year).PadLeft(4, '0');
                monthString += Convert.ToString(dateTime.Month).PadLeft(2, '0');

                Type type = typeof(T);
                SqlString.Append("SELECT * FROM " + type.Name + monthString + "");
                SqlString.Append(";");
                return SqlString.ToString();
            }
            catch (ReflectorException)
            {
                throw;
            }
        }

        public static string GetMonthlyCheckExistTable<T>(DateTime dateTime) where T : IMonthly
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (dateTime.Year < 2013)
                return "";

            string monthString = Convert.ToString(dateTime.Year).PadLeft(4, '0');
            monthString += Convert.ToString(dateTime.Month).PadLeft(2, '0');

            sqlBuilder.Append("SELECT COUNT(*) FROM SYSOBJECTS WHERE ID = OBJECT_ID('" + typeof(T).Name + monthString + "');");
            return sqlBuilder.ToString();
        }

        public static string GetMonthlyCreateTableSQL<T>(DateTime time) where T : IMonthly
        {
            StringBuilder fileds = new StringBuilder();
            bool isPrimary = false;

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Navigation navigationType = (Navigation)Attribute.GetCustomAttribute(property, typeof(Navigation));
                if (navigationType != null) continue;

                fileds.AppendFormat(" [{0}]", property.Name);
                fileds.AppendFormat(" [{0}]", TypeConverter.GetSQLType(property.PropertyType));

                PropertyType propertyType = (PropertyType)Attribute.GetCustomAttribute(property, typeof(PropertyType));
                if (propertyType != null && propertyType.IsPrimaryKey)
                {
                    isPrimary = true;
                    fileds.AppendFormat(" {0}", PrimaryConverter.GetDefaultPrimaryFiledString());
                }

                if (TypeConverter.IsNullableType(property.PropertyType))
                {
                    fileds.AppendFormat(" {0},", DefaultValueConverter.GetDefaultNullValue());
                }
                else
                {
                    fileds.AppendFormat(" {0},", DefaultValueConverter.GetDefaultNOTNullValue());
                }
            }

            string monthString = Convert.ToString(time.Year).PadLeft(4, '0');
            monthString += Convert.ToString(time.Month).PadLeft(2, '0');

            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendFormat("CREATE TABLE {0} ({1}) ", type.Name + monthString, fileds.ToString() + "");
            if (isPrimary)
                commandBuilder.Append(PrimaryConverter.GetDefaultPrimaryTableEndString());

            commandBuilder.Append(";");
            return commandBuilder.ToString();
        }

        public static string GetMonthlyCreateIndexSQL<T>(DateTime time) where T : IMonthly
        {
            Type type = typeof(T);
            string monthString = time.ToString("yyyyMM");
            StringBuilder commandBuilder = new StringBuilder();
            commandBuilder.AppendFormat(@"CREATE NONCLUSTERED INDEX [Idx_timestamp] ON {0} ([TimeStamp] DESC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];", type.Name + monthString);
            return commandBuilder.ToString();
        }
    }
}
