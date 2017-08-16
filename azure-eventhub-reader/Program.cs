using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_eventhub_reader
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = EventHubClient.CreateFromConnectionString("from https://portal.azure.com/#resource/subscriptions/485bc17f-97bd-40fa-a1b0-918fc639be40/resourceGroups/l22-apidebugging-test-rg/providers/Microsoft.EventHub/namespaces/l22-apidebugging-test/saskey", "iflogssit2");
            EventHubConsumerGroup group = client.GetDefaultConsumerGroup();
            var startDate = new DateTime(2017,8,15,0,0,0);
            var receiver = group.CreateReceiver(client.GetRuntimeInformation().PartitionIds[0],startDate);
            //var message = receiver.Receive();
            Console.ReadLine();

            bool receive = true;
            int counter = 0;
            while (receive)
            {
                var message = receiver.Receive();
                var body = Encoding.UTF8.GetString(message.GetBytes());
                var time = message.EnqueuedTimeUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");

                
                Console.WriteLine("MESSAGE " + message.Offset + " @ " + time);
                Console.WriteLine(body);
                Console.WriteLine("");

                Console.Write(".");
                if (counter % 60 == 59)
                {
                    Console.WriteLine("");
                }
                counter++;
            }
        }
    }
}
