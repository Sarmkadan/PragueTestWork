using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PragueTestWorkApi.Models
{
    class UrlLog
    {

        public class UrlLogdbContext : DbContext
        {
            public UrlLogdbContext() : base("Connstring") { }
            public DbSet<LogUrl> ItemDbSet { get; set; }
        }
        public class LogUrl
        {
            [Key, Column(Order = 0)]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string OldUrl { get; set; }
            public string NewUrl { get; set; }
            public string Result { get; set; }
            public DateTime Datetimeofrequest { get; set; }
        }
    }
}