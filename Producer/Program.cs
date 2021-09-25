using System;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            KafkaProducer kafkaProducer = new KafkaProducer();
            Console.WriteLine("write Your Message Here : ");
            while (true)
            {
                string msg = Console.ReadLine();
                await kafkaProducer.Write("TestTopic", msg);

            }
        }
    }
}
