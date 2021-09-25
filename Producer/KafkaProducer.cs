using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Producer
{

    public class KafkaProducer
    {

        public KafkaProducer()
        {

        }


        public ProducerConfig Config()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            return config;
        }

        public async Task Write(string Topic,string Message)
        {
            using var producer = new ProducerBuilder<Null, string>(Config()).Build();
            await producer.ProduceAsync(Topic, new Message<Null, string> {Value = Message });
        }



        public static void handler(DeliveryReport<string, string> task)
        {
            Console.WriteLine($"Wrote to offset: {task.Offset}");

        }


    }

}
