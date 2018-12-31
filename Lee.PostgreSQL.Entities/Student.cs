using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lee.Entities.Common;

namespace Lee.PostgreSQL.Entities
{

    /// <summary>
    /// 
    /// </summary>
    [Table("Student")]
    public class Student
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// 直接使用脚本，可以使用BIGSERIAL，但实际上是采用序列的方式（类似Oracle）；
        /// 如果Column中使用BIGSERIAL会报：The store type 'BIGSERIAL' could not be found in the Npgsql provider manifest
        /// 
        /// ALTER TABLE your_table ADD COLUMN key_column BIGSERIAL PRIMARY KEY;
        /// see https://stackoverflow.com/questions/7718585/how-to-set-auto-increment-primary-key-in-postgresql
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Column("name"), MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Column("code"), StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// 
        /// PG不支持ON UPDATE CURRENT_TIMESTAMP，只能使用触发器；
        /// see:https://www.postgresql.org/message-id/20060123143134.GH18894@webserv.wug-glas.de
        /// see:https://stackoverflow.com/questions/1035980/update-timestamp-when-row-is-updated-in-postgresql
        /// 
        /// DatabaseGenerated设置无效；
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [Column("Version", TypeName = "timestamp")]
        [DefaultValue(DefaultValueSql = "now()")]
        public DateTime Version { get; set; }

        //[Range(1, 2)] // 无效
        [DefaultValue(DefaultValue = 1)]
        public int Type { get; set; }

    }
}
