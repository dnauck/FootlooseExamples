using System;

namespace FootlooseExamples.PubSub.Events
{
    [Serializable]
    public class TextMessage
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}
