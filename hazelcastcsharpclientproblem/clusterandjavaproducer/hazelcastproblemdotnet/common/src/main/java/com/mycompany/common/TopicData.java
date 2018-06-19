package com.mycompany.common;

import java.io.IOException;

import com.hazelcast.nio.serialization.Portable;
import com.hazelcast.nio.serialization.PortableReader;
import com.hazelcast.nio.serialization.PortableWriter;

public class TopicData implements Portable {
    
    private String           data;

    public TopicData() {

    }

    public TopicData(String data) {
        this.data = data;
    }

    @Override
    public int getFactoryId() {
        return 1;
    }

    @Override
    public int getClassId() {
        return 1;
    }
    
    @Override
    public String toString() {
        return "TopicData [data=" + data + "]";
    }

    @Override
    public void writePortable(PortableWriter writer) throws IOException {
        boolean hasData = (data != null);
        if (hasData) {
            writer.writeUTF("data", data);
        }
        
        writer.writeBoolean("_has__data", hasData);
        
    }

    @Override
    public void readPortable(PortableReader reader) throws IOException {
        if (reader.readBoolean("_has__data")) {
            data = reader.readUTF("data");
        }
        
    }

}
