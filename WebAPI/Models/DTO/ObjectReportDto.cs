using System;

namespace WebAPI.Models.DTO
{
    public class ObjectReportDto
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public DateTime? DateTime { get; set; }

        public int? ObjectReportId { get; set; }

        public int? SenderId { get; set; }

        public string SenderFirstLastName { get; set; }
    }
}
