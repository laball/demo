using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.Entities
{
    [Table("Demo_AllCommonFieldTable")]
    public class AllCommonFieldTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        /// <summary>
        /// bool => tinyint(1)
        /// 
        /// </summary>
        [Column("BoolValue")]
        public bool BoolValue { get; set; }
        /// <summary>
        /// bool => tinyint(1)
        /// 
        /// </summary>
        [Column("BoolValueNullable")]
        public bool? BoolValueNullable { get; set; }
        /// <summary>
        /// byte => tinyint(3) UNSIGNED
        /// </summary>
        [Column("ByteValue")]
        public byte ByteValue { get; set; }
        /// <summary>
        /// short => smallint(6)
        /// </summary>
        [Column("ShortValue")]
        public short ShortValue { get; set; }
        /// <summary>
        /// int => int(11)
        /// </summary>
        [Column("IntValue")]
        public int IntValue { get; set; }
        /// <summary>
        /// decimal => decimal(18,2)
        /// </summary>
        [Column("DecimalValue")]
        public decimal DecimalValue { get; set; }
        /// <summary>
        /// float => float
        /// </summary>
        [Column("FloatValue")]
        public float FloatValue { get; set; }
        /// <summary>
        /// double => double
        /// </summary>
        [Column("DoubleValue")]
        public double DoubleValue { get; set; }

        /// <summary>
        /// enum => int(11)
        /// </summary>
        [Column("EnumValue")]
        [EnumDataType(typeof(MyEnum))]
        public MyEnum EnumValue { get; set; }

        /// <summary>
        /// string with length => varchar(length)
        /// </summary>
        [Column("StrField1")]
        [StringLength(20)]
        public string StrField1 { get; set; }
        /// <summary>
        /// string with max and min length => varchar(max length) not null
        /// </summary>
        [Column("StrField2")]
        [Required]
        [MaxLength(20), MinLength(10)]

        public string StrField2 { get; set; }
        [Column("BlogDescription", TypeName = "text")]
        public string Description { get; set; }

        [Column("DateCreated")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]//TODO:无效
        //[DefaultValue(DefaultValueSql = "now()")] //TODO:MySql不支持默认值使用函数
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// 未映射字段
        /// 一般用于拼接并返回信息
        /// </summary>
        [NotMapped]
        public string NotMappedField { get; set; }
        /// <summary>
        /// 索引默认映射：
        /// [Index] => IX_{FieldName}
        /// </summary>
        [Column("Rating")]
        [Index]
        public int Rating { get; set; }
        /// <summary>
        /// `TimeStampValue`  timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
        /// </summary>
        [Column("TimeStampValue", TypeName = "timestamp")]
        //[Timestamp] //The store type 'rowversion' could not be found in the MySql provider manifest
        public DateTime TimeStampValue { get; set; }
        /// <summary>
        /// Guid => char(36)
        /// </summary>
        [Column("GuidField")]
        public Guid GuidField { get; set; }
    }

    public enum MyEnum
    {
        Value1 = 1,
        Value2 = 2,
        Value3 = 3,
        Value4 = 4
    }
}
