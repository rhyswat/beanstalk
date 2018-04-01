using System;
using System.ComponentModel.DataAnnotations;

namespace Beanstalk.Db.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Stadium { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Drawn { get; set; }
    }
}