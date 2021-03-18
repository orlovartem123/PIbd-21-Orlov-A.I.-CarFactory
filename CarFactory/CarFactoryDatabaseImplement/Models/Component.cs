using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactoryDatabaseImplement.Models
{
	public class Component
	{
		public int Id { get; set; }

		[Required]
		public string ComponentName { get; set; }

		[ForeignKey("ComponentId")]
		public virtual List<CarComponent> CarComponents { get; set; }
	}
}
