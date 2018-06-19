using Hazelcast.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelcastDotNetProblem
{
    public class CustomLogFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            return new CustomLogger { Name = name };
        }

        internal class CustomLogger : AbstractLogger
        {

            private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public string Name { get; set; }

            public override LogLevel GetLevel()
            {
                return LogLevel.All;
            }


            public override bool IsLoggable(LogLevel arg1)
            {
                return true;
            }

            public override void Log(LogLevel arg1, string arg2)
            {
                log.Info(arg2);
            }

            public override void Log(LogLevel arg1, string arg2, Exception arg3)
            {
                log.Info(arg2,arg3);
            }
        }
    }
}
