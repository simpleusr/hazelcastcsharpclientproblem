package com.mycompany.client.service;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.InitializingBean;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.core.ITopic;
import com.mycompany.common.TopicData;

@Service
public class TopicSender implements InitializingBean {
    
    private static final Logger logger = LoggerFactory.getLogger(TopicSender.class);
    
    private long startIndex;

    @Autowired
    private HazelcastInstance hazelcastClient;

    @Value("${elementCount}")
    private int               elementCount;
    
    @Value("${delay}")
    private long               delay;
    
    @Value("${topicName}")
    private String topicName;

    public void publishTopicData() {
        
        while (true) {
            ITopic<TopicData> hzlTopic = getTopic();
            
            startIndex++;
            
            for (int i = 0; i < elementCount; i++) {
                
                long dataVal = startIndex + i;
                String key = "data_"+dataVal;
                TopicData topicData = new TopicData(key);
                hzlTopic.publish(topicData );
                
                logger.info(String.format("published topic data : %s " , topicData));
            }
            
            try {
                Thread.sleep(delay);
            } catch (InterruptedException e) {
                logger.error("Error in thread sleep", e);
            }
            
        }

        

    }
    
    private ITopic<TopicData> getTopic() {
        return hazelcastClient.getTopic(topicName);
    }

    @Override
    public void afterPropertiesSet() throws Exception {
        startIndex = System.currentTimeMillis();
        
    }

}
