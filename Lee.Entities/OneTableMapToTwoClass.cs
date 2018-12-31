using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.Entities
{
    [Table("Demo_MainTable")]
    public class MainTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
        public DetailTable Detail { get; set; }
    }

    [ComplexType]
    public class DetailTable
    {
        /// <summary>
        /// string with length => varchar(length)
        /// </summary>
        [StringLength(100)]
        public string Address { get; set; }
        /// <summary>
        /// string no length => longtext
        /// </summary>
        public string Mail { get; set; }
    }

}
