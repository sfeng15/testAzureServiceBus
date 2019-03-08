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

            string ServiceBusUriScheme = "sb";
            string RootManageSharedAccessKeyName = "RootManageSharedAccessKey";
            string RootManageSharedAccessKeyValue = "64Ohm/hiKkNaHOLHPd63yLQ0QxlrCFvD9HGO1LSnQ3A=";
            //var RootManageSharedAccessKey = new ApSecureStoreAzureStorageCredentialConfiguration()
            //{
            //    EncryptedKeyFilePath = "bsx-commsearch-asb-intmd-scu--primarykey.txt.encr"
            //};


            string Namespace = "msfb-seatblocks-int-asb-wcus";


            //{
            //    ServiceUri = new Uri("sb://msfb-seatblocks-int-asb-wcus.servicebus.windows.net/"),
            //    Credentials = new KeyFileCredentialConfiguration.ApSecureStore
            //    {
            //        EncryptedKeyFilePath = "msfb-seatblocks-int-asb-wcus.servicebus-primarykey.txt.encr"
            //    };
            

            TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(RootManageSharedAccessKeyName, RootManageSharedAccessKeyValue);

            Uri uri = ServiceBusEnvironment.CreateServiceUri(ServiceBusUriScheme, Namespace, String.Empty);

            var namespaceManager = new NamespaceManager(uri, tokenProvider);
            IEnumerable<QueueDescription> queuesDescriptions = namespaceManager.GetQueuesAsync().Result;

            foreach (var queuesDescription in queuesDescriptions)
            {
                Console.WriteLine("count of deadletter message is :" + queuesDescription.MessageCountDetails.DeadLetterMessageCount);
                Console.WriteLine("count of acive message is :" + queuesDescription.MessageCountDetails.ActiveMessageCount);

                Console.WriteLine("path is :" + queuesDescription.Path);
                Console.WriteLine();
            }


            while (true)
            {
                
            }
        }
    }
}
