using Hazelcast.Client;
using Hazelcast.Config;
using Hazelcast.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelcastDotNetProblem
{
    class Program
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            try
            {
                //Environment.SetEnvironmentVariable("hazelcast.logging.level", "All");
                //Environment.SetEnvironmentVariable("hazelcast.logging.type", "console");

                Logger.SetLoggerFactory(new CustomLogFactory());

                log.Info("CONFIGURATION STARTED ");

                var clientConfig = new ClientConfig();

                var groupName = ConfigurationManager.AppSettings["groupName"];

                var groupPassword = ConfigurationManager.AppSettings["groupPassword"];

                log.Info("Using groupName: " + groupName + " groupPassword: " + groupPassword);

                clientConfig.SetGroupConfig(new GroupConfig(groupName, groupPassword));

                string memberAddresses = ConfigurationManager.AppSettings["memberAddresses"];

                string[] adresses = memberAddresses.Split(',');

                foreach (var adress in adresses)
                {
                    log.Info("Adding address " + adress);
                    clientConfig.GetNetworkConfig().AddAddress(adress);
                }




                clientConfig.GetSerializationConfig()
                  .AddPortableFactory(MyPortableFactory.FactoryId, new MyPortableFactory());



                var client = HazelcastClient.NewHazelcastClient(clientConfig);

                var topicName = ConfigurationManager.AppSettings["topicName"];

                log.Info("Using topicName: " + topicName);

                var topic = client.GetTopic<TopicData>(topicName);

                topic.AddMessageListener(new TopicListener());

                Console.ReadLine();
            }
            catch (Exception e)
            {
                log.Error("Error in HazelcastConfig", e);
                throw;
            }

        }
    }
}
