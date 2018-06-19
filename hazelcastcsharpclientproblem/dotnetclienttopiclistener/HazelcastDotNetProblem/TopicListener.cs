using Hazelcast.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelcastDotNetProblem
{
    public class TopicListener : IMessageListener<TopicData>
    {
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void OnMessage(Message<TopicData> message)
        {

            var consumer = Task.Factory.StartNew(() =>
            {

                var obj = message.GetMessageObject();
                log.Info("recevied at : " + GetFormattedCurrentTimeStamp() + " obj " + obj.ToString());
            });

        }

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static String GetFormattedCurrentTimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss:fff");
        }
    }
}
