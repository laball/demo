using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lee.Abp.EntityFrameworkCore.EntityFrameworkCore
{
    class CustomMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        public CustomMigrationsSqlGenerator([NotNull] MigrationsSqlGeneratorDependencies dependencies)
            : base(dependencies)
        {

        }

        

    }
}
