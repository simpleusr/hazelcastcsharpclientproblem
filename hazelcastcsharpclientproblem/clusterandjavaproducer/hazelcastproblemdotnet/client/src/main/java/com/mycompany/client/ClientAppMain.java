package com.mycompany.client;

import java.util.concurrent.CountDownLatch;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.jdbc.DataSourceAutoConfiguration;
import org.springframework.boot.autoconfigure.jdbc.DataSourceTransactionManagerAutoConfiguration;
import org.springframework.boot.autoconfigure.orm.jpa.HibernateJpaAutoConfiguration;
import org.springframework.context.ConfigurableApplicationContext;

import com.mycompany.client.service.TopicSender;

@SpringBootApplication
@EnableAutoConfiguration(exclude = { DataSourceAutoConfiguration.class, DataSourceTransactionManagerAutoConfiguration.class, HibernateJpaAutoConfiguration.class })
public class ClientAppMain {

    public static void main(String[] args) throws Exception {

        ConfigurableApplicationContext context = SpringApplication.run(ClientAppMain.class, args);
        
        TopicSender topicSender = context.getBean(TopicSender.class);

        topicSender.publishTopicData();


        CountDownLatch closeLatch = context.getBean("closeLatch", CountDownLatch.class);
        closeLatch.await();

    }

 

}
