namespace Portal.Core.AzureStorage
{
    public class StorageConfig
    {
        //Conta de armazenamento
        public string AccountName { get; set; }
        //Chaves de acesso
        public string AccountKey { get; set; }
        public string QueueName { get; set; }
        //Nome do Container de Armazenamento
        public string Container { get; set; }
        public string ThumbnailContainer { get; set; }
    }
}
