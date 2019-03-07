using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace GetServiceBusMetrics
{
    class Program
    {
        static void Main(string[] args)
        {
            //var namespaceConfiguration = new AzureServiceBusNamespaceConfiguration()
            //{
            //    Namespace = "bsx-commsearch-asb-intmd-scu",
            //    RootManageSharedAccessKey = new ApSecureStoreAzureStorageCredentialConfiguration()
            //    {
            //        EncryptedKeyFilePath = "bsx-commsearch-asb-intmd-scu--primarykey.txt.encr"
            //    },
            //    TopicConfiguration = new AzureServiceBusTopicConfiguration
            //    {
            //        TopicName = "IndexingRequests",
            //        MaxSizeInMegabytes = 5 * 1024
            //    }
            //};

            ServiceBusNamespaceDetails serviceBusNamespaceDetails = new ServiceBusNamespaceDetails()
            {
                Namespace = namespaceConfiguration.Namespace,
                NamespaceAuthKey = ""
            };
            {
                ServiceUri = new Uri("sb://msfb-seatblocks-int-asb-wcus.servicebus.windows.net/"),
                Credentials = new KeyFileCredentialConfiguration.ApSecureStore
                {
                    EncryptedKeyFilePath = "msfb-seatblocks-int-asb-wcus.servicebus-primarykey.txt.encr"
                };

            TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(RootManageSharedAccessKey, namespaceConfiguration.NamespaceAuthKey);

            Uri uri = ServiceBusEnvironment.CreateServiceUri(ServiceBusUriScheme, namespaceConfiguration.Namespace, String.Empty);

            var namespaceManager = new NamespaceManager(uri, tokenProvider);
            IEnumerable<QueueDescription> queuesDescriptions = namespaceManager.GetQueuesAsync().Result;

            Console.WriteLine(queuesDescriptions.FirstOrDefault().MessageCountDetails.DeadLetterMessageCount);

            }
        }
    }
}
