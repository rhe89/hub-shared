namespace Hub.Storage.Core.Dto
{
    public class WorkerLogDto : EntityDtoBase
    {
        public string Name { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string InitiatedBy { get; set; }
    }
}