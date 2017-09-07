using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HolidayRequestSystem.Domain.Read.Model
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}