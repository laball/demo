using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.PostgreSQL.Entities
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [Table("CommonFieldTable")]
    public class CommonFieldTable
    {
        /// <summary>
        /// long => bigint
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        /// <summary>
        /// bool => boolean
        /// </summary>
        [Column("BoolValue")]
        public bool BoolValue { get; set; }
        /// <summary>
        /// bool => boolean
        /// </summary>
        [Column("BoolValueNullable")]
        public bool? BoolValueNullable { get; set; }
        /// <summary>
        /// byte => smallint
        /// </summary>
        [Column("ByteValue")]
        public byte ByteValue { get; set; }
        /// <summary>
        /// short => smallint
        /// </summary>
        [Column("ShortValue")]
        public short ShortValue { get; set; }
        /// <summary>
        /// int => integer
        /// </summary>
        [Column("IntValue")]
        public int IntValue { get; set; }
        /// <summary>
        /// decimal => numeric(18,2)
        /// </summary>
        [Column("DecimalValue")]
        public decimal DecimalValue { get; set; }
        /// <summary>
        /// float => real
        /// </summary>
        [Column("FloatValue")]
        public float FloatValue { get; set; }
        /// <summary>
        /// double => double
        /// </summary>
        [Column("DoubleValue")]
        public double DoubleValue { get; set; }

        /// <summary>
        /// enum => integer
        /// </summary>
        [Column("EnumValue")]
        [EnumDataType(typeof(MyEnum))]
        public MyEnum EnumValue { get; set; }

        /// <summary>
        /// string with length => character varying(20) COLLATE pg_catalog."default"
        /// </summary>
        [Column("StrField1")]
        [StringLength(20)]
        public string StrField1 { get; set; }
        /// <summary>
        /// string with max and min length => character varying(20) COLLATE pg_catalog."default" NOT NULL DEFAULT ''::character varying
        /// </summary>
        [Column("StrField2")]
        [Required]
        [MaxLength(20), MinLength(10)]

        public string StrField2 { get; set; }
        [Column("BlogDescription", TypeName = "text")]
        public string Description { get; set; }

        /// <summary>
        /// DateTime => timestamp without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
        /// </summary>
        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// 未映射字段
        /// 一般用于拼接并返回信息
        /// </summary>
        [NotMapped]
        public string NotMappedField { get; set; }
        /// <summary>
        /// 索引默认映射：
        /// [Index] => {TableName}_IX_{FieldName}
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
        /// Guid => uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000'::uuid
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
