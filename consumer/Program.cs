using System;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait for Get Messages  ....");
            KafkaComsumer kafka = new KafkaComsumer("TestTopic");
            kafka.MessageRecived += Kafka_MessageRecived;
            Console.ReadLine();
        }

        private static void Kafka_MessageRecived(object sender, ConsumeResultEventArgs e)
        {
            Console.WriteLine(e.Result.Message.Value);
        }
    }
}
