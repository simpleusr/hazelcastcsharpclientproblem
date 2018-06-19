using Hazelcast.IO.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HazelcastDotNetProblem
{
    public class TopicData : IPortable, ISerializable
    {
        public String Data { get; set; }

        public int GetClassId()
        {
            return 1;
        }

        public int GetFactoryId()
        {
            return 1;
        }

        public void ReadPortable(IPortableReader reader)
        {
            if (reader.ReadBoolean("_has__data"))
            {
                Data = reader.ReadUTF("data");
            }



        }

        public void WritePortable(IPortableWriter writer)
        {
            bool hasData = (Data != null);
            if (hasData)
            {
                writer.WriteUTF("data", Data);
            }
            writer.WriteBoolean("_has__data", hasData);


        }


        public override String ToString()
        {
            return "TopicData [Data=" + Data + "]";
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
