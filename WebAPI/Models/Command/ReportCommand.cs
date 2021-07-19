using System;

namespace WebAPI.Models.Command
{
    public class ReportCommand
    {
        public string Reason { get; set; }

        public string Explanation { get; set; }

        public int ReportObjectId { get; set; }

        public int SenderId { get; set; }
    }
}
