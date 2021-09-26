using Confluent.Kafka;
using System;

namespace consumer
{
   


    public class ConsumeResultEventArgs : EventArgs
    {
        public ConsumeResult<Ignore, string> Result { get; set; }

        public delegate void MessageRecived(Object sender, ConsumeResultEventArgs e);


    }
}
