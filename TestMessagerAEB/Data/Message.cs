using System.ComponentModel.DataAnnotations;

namespace TestMessagerAEB.Data
{
    public class Message
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(128)]
        public string Text { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int SequenceNumber { get; set; }
    }
}