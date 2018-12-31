namespace Lee.PostgreSQL.EntityFramework.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "test.CommonFieldTable",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        BoolValue = c.Boolean(nullable: false),
                        BoolValueNullable = c.Boolean(),
                        ByteValue = c.Short(nullable: false),
                        ShortValue = c.Short(nullable: false),
                        IntValue = c.Int(nullable: false),
                        DecimalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FloatValue = c.Single(nullable: false),
                        DoubleValue = c.Double(nullable: false),
                        EnumValue = c.Int(nullable: false),
                        StrField1 = c.String(maxLength: 20),
                        StrField2 = c.String(nullable: false, maxLength: 20),
                        BlogDescription = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        TimeStampValue = c.DateTime(nullable: false),
                        GuidField = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.Rating);
            
            CreateTable(
                "test.GuidTable",
                c => new
                    {
                        id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "test.Student",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 20),
                        code = c.String(maxLength: 20),
                        Version = c.DateTime(nullable: false, defaultValueSql: "now()",
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValueSQL",
                                    new AnnotationValues(oldValue: null, newValue: "now()")
                                },
                            }),
                        type = c.Int(nullable: false, defaultValue: 1,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "DefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropIndex("test.CommonFieldTable", new[] { "Rating" });
            DropTable("test.Student",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "type",
                        new Dictionary<string, object>
                        {
                            { "DefaultValue", "1" },
                        }
                    },
                    {
                        "Version",
                        new Dictionary<string, object>
                        {
                            { "DefaultValueSQL", "now()" },
                        }
                    },
                });
            DropTable("test.GuidTable");
            DropTable("test.CommonFieldTable");
        }
    }
}
