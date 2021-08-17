using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Portal.Core.AzureStorage;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Storage
{
    public class StorageHelper
    {
        private readonly StorageConfig _storageConfig = null;

        public StorageHelper(StorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }

        /// <summary>
        /// Upload dos arquivos para o Storage
        /// </summary>
        /// <param name="stream">arquivo que vai ser enviado</param>
        /// <param name="nameFile">nome dos arquivos</param>
        /// <param name="container">nome do container</param>
        /// <returns></returns>
        public async Task<JsonArquivo> UploadAsync(Stream stream, string nameFile, string container)
        {
            return await UploadFileToStorageAsync(stream, nameFile, container, _storageConfig);
        }
        /// <summary>
        /// Verifica se um arquivo existe no Storage
        /// </summary>
        /// <param name="stream">arquivo que vai ser enviado</param>
        /// <param name="nameFile">nome dos arquivos</param>
        /// <param name="container">nome do container</param>
        /// <returns></returns>
        public async Task<JsonArquivo> ExistsAsync(string nome, string tabela)
        {
            return await ExistsBlobAsync(nome, tabela, _storageConfig);
        }
        public bool IsImage(string nameFile)
        {
            string[] formats = new string[] { ".jpg", ".png", ".jpeg" };
            return formats.Any(item => nameFile.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Faz o Upload de arquivos no Storage e retornra o Url
        /// </summary>
        /// <param name="fileStream">arquivo que vai ser enviado</param>
        /// <param name="fileName">nome do arquivo</param>
        /// <param name="tabela">nome do container</param>
        /// <param name="storageConfig">Model do storage com os dados para configuração</param>
        /// <returns></returns>
        private static async Task<JsonArquivo> UploadFileToStorageAsync(Stream fileStream, string fileName,
            string tabela, StorageConfig storageConfig)
        {
            var storageCredentials = new StorageCredentials(storageConfig.AccountName, storageConfig.AccountKey);

            fileStream.Position = 0;

            var storageAccount = new CloudStorageAccount(storageCredentials, true);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(tabela);
            var newCreate = await container.CreateIfNotExistsAsync();

            if (newCreate)
            {
                await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(fileStream);

            var retorno = await blockBlob.ExistsAsync();

            return new JsonArquivo()
            {
                Url = blockBlob.SnapshotQualifiedStorageUri.PrimaryUri.ToString(),
                Retorno = retorno
            };
        }

        /// <summary>
        /// Verifica se um arquivo existe no Blob do Azure e retornra o Url.
        /// </summary>
        /// <param name="tabela"> Tabela para inserir o arquivo </param>
        /// <param name="nome"> Nome do Arquivo </param>
        /// /// <param name="storageConfig">Model do storage com os dados para configuração</param>
        /// <returns></returns>
        private async Task<JsonArquivo> ExistsBlobAsync(string nome, string tabela, StorageConfig storageConfig)
        {
            try
            {
                var storageCredentials = new StorageCredentials(storageConfig.AccountName, storageConfig.AccountKey);

                var storageAccount = new CloudStorageAccount(storageCredentials, true);

                var client = storageAccount.CreateCloudBlobClient();

                var cont = client.GetContainerReference(tabela);

                //await cont.CreateIfNotExistsAsync().ConfigureAwait(false);

                var blockblob = cont.GetBlockBlobReference(nome);

                //await cont.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                bool retorno = await blockblob.ExistsAsync();

                return new JsonArquivo()
                {
                    Url = blockblob.SnapshotQualifiedStorageUri.PrimaryUri.ToString(),
                    Retorno = retorno
                };
            }
            catch (StorageException StorageExceptionObj)
            {
                throw StorageExceptionObj;
            }
            catch (Exception ExceptionObj)
            {
                throw ExceptionObj;
            }
        }
    }
}
