using System;

namespace Hub.Storage.Dto
{
    public class WorkerLogDto
    {
        public string Name { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}