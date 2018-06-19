using Hazelcast.IO.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazelcastDotNetProblem
{
    public class MyPortableFactory : IPortableFactory
    {
        public IPortable Create(int classId)
        {
            if (classId == 1)
            {
                return new TopicData();
            }
            else
            {
                throw new NotImplementedException(classId + " unsupported classId");
            }

        }

        public MyPortableFactory()
        {

        }

        public static int FactoryId = 1;
    }
}
