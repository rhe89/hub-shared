namespace Hub.Storage.Entities
{
    public class WorkerLog : EntityBase
    {
        public string Name { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string InitiatedBy { get; set; }
    }
}