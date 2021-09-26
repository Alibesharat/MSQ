using Confluent.Kafka;
using System;
using System.Threading;

namespace consumer
{
    public class KafkaComsumer
    {
        readonly IConsumer<Ignore, string> _Consumer;
        public event EventHandler<ConsumeResultEventArgs> MessageRecived;

        public KafkaComsumer()
        {
            _Consumer = new ConsumerBuilder<Ignore, string>(Config()).Build();
        


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

       


        public void SetUp(string Topic)
        {
            _Consumer.Subscribe(Topic);
            while (true)
            {
                try
                {
                    var result = _Consumer.Consume(new CancellationToken());
                    if (result != null)
                    {
                        ConsumeResultEventArgs args = new ConsumeResultEventArgs
                        {
                            Result = result
                        };
                        MessageRecived?.Invoke(this,args);
                       
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
