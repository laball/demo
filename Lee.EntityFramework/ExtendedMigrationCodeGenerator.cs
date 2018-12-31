using System;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Lee.EntityFramework
{
    public class ExtendedMigrationCodeGenerator : CSharpMigrationCodeGenerator
    {
        protected override void Generate(ColumnModel column, IndentedTextWriter writer, bool emitName = false)
        {
            if (column.Annotations.Keys.Contains("DefaultValue"))
            {
                var value = Convert.ChangeType(column.Annotations["DefaultValue"].NewValue, column.ClrDefaultValue.GetType());
                column.DefaultValue = value;
            }
            
            //MySql不支持默认值使用函数；
            //if (column.Annotations.Keys.Contains("DefaultValueSQL"))
            //{
            //    column.DefaultValueSql = (string)column.Annotations["DefaultValueSQL"].NewValue;
            //}

            base.Generate(column, writer, emitName);
        }
    }
}
