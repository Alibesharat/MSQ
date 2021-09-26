using Confluent.Kafka;
using System;
using System.Threading;

namespace consumer
{
    public class KafkaComsumer
    {
        readonly IConsumer<Ignore, string> _Consumer;
        public event EventHandler<ConsumeResultEventArgs> MessageRecived;
        public KafkaComsumer(string Topic)
        {
            _Consumer = new ConsumerBuilder<Ignore, string>(Config()).Build();
            _Consumer.Subscribe(Topic);
            ReadMessage();

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

        protected virtual void OnMessageRecived(ConsumeResultEventArgs e)
        {
            EventHandler<ConsumeResultEventArgs> handler = MessageRecived;
            handler?.Invoke(this, e);
        }




        private void ReadMessage()
        {
            while (true)
            {
                try
                {
                    var result = _Consumer.Consume(new CancellationToken());
                    if (result != null)
                    {
                        OnMessageRecived(new ConsumeResultEventArgs()
                        {
                            Result = result
                        });
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
