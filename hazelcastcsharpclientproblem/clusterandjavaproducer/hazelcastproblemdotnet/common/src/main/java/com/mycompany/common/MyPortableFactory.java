package com.mycompany.common;

import com.hazelcast.nio.serialization.Portable;
import com.hazelcast.nio.serialization.PortableFactory;

public class MyPortableFactory implements PortableFactory {

    /**
     * Create an instance of a Portable matching the provided classId.
     */
    @Override
    public Portable create(int classId) {
        if (classId == 1) {
            return new TopicData();
        }  else {
            throw new IllegalArgumentException(classId + " unsupported classId");
        }

    }

}
