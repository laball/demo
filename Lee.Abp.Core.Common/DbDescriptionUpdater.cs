using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;

namespace Lee.Abp.Core.Common
{
    /// <summary>
    /// using: <code>var updater = new DbDescriptionUpdater<BusinessDbContext>(this);updater.UpdateDatabaseDescriptions();</code>
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class DbDescriptionUpdater<TContext>
           where TContext : DbContext
    {
        public DbDescriptionUpdater(TContext context)
        {
            this.context = context;
        }

        Type contextType;
        TContext context;
        IDbContextTransaction transaction;

        public void UpdateDatabaseDescriptions()
        {
            contextType = typeof(TContext);
            var props = contextType.GetProperties(BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            transaction = null;
            try
            {
                transaction = context.Database.BeginTransaction();
                foreach (var prop in props)
                {
                    if (prop.PropertyType.Name == typeof(DbSet<>).Name)
                    {
                        var tableType = prop.PropertyType.GetGenericArguments()[0];
                        SetTableDescriptions(tableType);
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();

                throw ex;
            }
            finally
            {

            }
        }

        private void SetTableDescriptions(Type tableType)
        {
            //string fullTableName = context.GetTableName(tableType);

            //if (string.IsNullOrEmpty(fullTableName))
            //{
            //    return;
            //}
            //Regex regex = new Regex(@"(\[\w+\]\.)?\[(?<table>.*)\]");
            //Match match = regex.Match(fullTableName);
            //string tableName;
            //if (match.Success)
            //    tableName = match.Groups["table"].Value;
            //else
            //    tableName = fullTableName;
            //if(tableName== "wcs_sys_server")
            //{
            //    var i = 0;
            //}
            //if(tableType.Name == "WcsBusinessCacheSiteAgvEmptyOut")
            //{

            //}
            var tableName = "";
            var tableAttrs = tableType.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttrs.Length > 0)
                tableName = ((TableAttribute)tableAttrs[0]).Name;
            Trace.WriteLine("----------" + tableName);
            var descriptionAttrs = tableType.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (descriptionAttrs.Length > 0)
            {
                var description = ((DescriptionAttribute)descriptionAttrs[0]).Description;
                var sql = "ALTER TABLE `" + tableName + "` COMMENT='" + description + "';";
                Trace.WriteLine(sql);
                RunSql(sql);
            }
            foreach (var prop in tableType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if ((prop.PropertyType.IsClass && prop.PropertyType.Name != "String") || prop.PropertyType.IsGenericType)
                    continue;
                SetColumnDescription(tableName, prop);
            }
        }

        private void SetColumnDescription(string tableName, PropertyInfo prop)
        {
            string columnName = prop.Name;
            if (columnName == "ID" || columnName == "Id")
                return;
            var columnAttrs = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
            if (columnAttrs.Length > 0)
            {
                columnName = ((ColumnAttribute)columnAttrs[0]).Name;
            }
            var attrs = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string description = "";
            if (attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
            if (string.IsNullOrEmpty(description))
            {
                return;
            }
            var type = prop.PropertyType;

            var typeName = type.Name;
            var baseType = type.BaseType;
            if (baseType.Name == "Enum")
            {
                typeName = "Int32";
            }
            var dbTypeName = "";
            if (typeName == "String")
            {
                var maxLength = prop.GetCustomAttributes(typeof(MaxLengthAttribute), false);
                var stringLength = prop.GetCustomAttributes(typeof(StringLengthAttribute), false);
                var len = 0;
                if (maxLength.Length > 0)
                {
                    len = ((MaxLengthAttribute)maxLength[0]).Length;
                }
                else if (stringLength.Length > 0)
                {
                    len = ((StringLengthAttribute)stringLength[0]).MaximumLength;
                }
                if (len > 0)
                {
                    dbTypeName = string.Format("varchar({0})", len);
                }
                else
                {
                    return;
                }
            }
            else if (typeName == "Decimal")
            {
                var decimalPrecision = prop.GetCustomAttributes(typeof(DecimalPrecisionAttribute), false);
                var precision = 18;
                var scale = 2;
                if (decimalPrecision.Length > 0)
                {
                    var dp = (DecimalPrecisionAttribute)decimalPrecision[0];
                    precision = dp.Precision;
                    scale = dp.Scale;
                }
                dbTypeName = string.Format("decimal({0},{1})", precision, scale);
            }
            else if (typeName == "Int32")
            {
                dbTypeName = "int";
            }
            else if (typeName == "Int64")
            {
                dbTypeName = "bigint";
            }
            else if (typeName == "Int16")
            {
                dbTypeName = "smallint";
            }
            else
            {
                dbTypeName = typeName.ToLower();
            }
            var sql = "alter table `" + tableName + "` modify column `" + columnName + "` " + dbTypeName + " comment '" + description + "';";
            Trace.WriteLine(sql);
            RunSql(sql);
        }

        //DbCommand CreateCommand(string cmdText, params MySqlParameter[] parameters)
        //{

        //    context.Database.ExecuteSqlCommand("");

        //    //Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade

        //    var cmd = context.Database.Connection.CreateCommand();
        //    cmd.CommandText = cmdText;
        //    cmd.Transaction = transaction;
        //    foreach (var p in parameters)
        //        cmd.Parameters.Add(p);
        //    return cmd;
        //}

        void RunSql(string cmdText, params MySqlParameter[] parameters)
        {
            try
            {
                context.Database.ExecuteSqlCommand(cmdText);
                //var cmd = CreateCommand(cmdText, parameters);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }


        object RunSqlScalar(string cmdText, params MySqlParameter[] parameters)
        {
            return context.Database.ExecuteSqlCommand(cmdText);
            //var cmd = CreateCommand(cmdText, parameters);
            //return cmd.ExecuteScalar();
        }
    }
}
