using System;
using System.Collections.Generic;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait for Get Messages  ....");
            KafkaComsumer kafka = new KafkaComsumer("TestTopic");
            kafka.ReadMessage();
            Console.ReadLine();
        }
    }
}
