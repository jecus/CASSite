using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity.General
{
    [Table("Aircrafts", Schema = "dbo")]
	
	public class Aircraft : BaseEntity
	{
		[Column("ManufactureDate")]
		public DateTime ManufactureDate { get; set; }

		
		[Column("RegistrationNumber"), MaxLength(50)]
		public string RegistrationNumber { get; set; }

		
		[Column("SerialNumber"), MaxLength(25)]
		public string SerialNumber { get; set; }
	}
}