using System.ComponentModel.DataAnnotations;

namespace Quanlybug.Models
{
    public class Project_1
    {
        public string? NameProject { get; set; }
        public string? ContextProject { get; set; }
        public int? IdUser { get; set; }
        public string? Picture { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }
        public string? Performer { get; set; }
        public string? Status { get; set; }
    }
}
