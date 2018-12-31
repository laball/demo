using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.Entities
{
    [Table("Demo_ItemTable")]
    public class ItemTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(30)]
        public string Description {get;set;}
        /// <summary>
        /// FOREIGN KEY (`MasterTable_ID`) REFERENCES `demo_matertable` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
        /// </summary>
        public MasterTable MasterTable { get; set; }
    }
}
