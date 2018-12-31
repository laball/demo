using System;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Lee.PostgreSQL.EntityFramework
{
    public class ExtendedMigrationCodeGenerator : CSharpMigrationCodeGenerator
    {
        private const string DefaultValueKey = "DefaultValue";
        private const string DefaultValueSQLKey = "DefaultValueSQL";

        protected override void Generate(ColumnModel column, IndentedTextWriter writer, bool emitName = false)
        {
            // 处理默认值设置

            if (column.Annotations.Keys.Contains(DefaultValueKey))
            {
                var value = Convert.ChangeType(column.Annotations[DefaultValueKey].NewValue, column.ClrDefaultValue.GetType());
                column.DefaultValue = value;
            }

            if (column.Annotations.Keys.Contains(DefaultValueSQLKey))
            {
                column.DefaultValueSql = (string)column.Annotations[DefaultValueSQLKey].NewValue;
            }

            base.Generate(column, writer, emitName);
        }
    }
}
