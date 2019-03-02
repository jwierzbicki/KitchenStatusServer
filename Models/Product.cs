using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenStatusServer.Models
{
    public class Product
    {
        public Product()
        {
        }

        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("state_current")]
        public float StateCurrent { get; set; }

        [Column("state_minimal")]
        public float StateMinimal { get; set; }

        [Column("unit_quantity")]
        public float UnitQuantity { get; set; }

        [Column("unit_type")]
        public string UnitType { get; set; }
    }
}
