using Confluent.Kafka;
using System;
using System.Threading;

namespace consumer
{
    public class KafkaComsumer 
    {
        readonly IConsumer<Ignore, string> _Consumer;
        public KafkaComsumer(string Topic)
        {
            _Consumer= new ConsumerBuilder<Ignore, string>(Config()).Build();
            _Consumer.Subscribe(Topic);
        }
        public ConsumerConfig Config()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "SourceApp",
                EnableAutoCommit = true,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000
            };
            return config;
        }


      



        public void ReadMessage()
        {
            while (true)
            {
                try
                {
                    var result = _Consumer.Consume(new CancellationToken());
                    if (result != null)
                    {
                        Console.WriteLine(result.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason} -- Isfatal={e.Error.IsFatal}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }


       
    }
}
