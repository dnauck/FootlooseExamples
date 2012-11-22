using System;
using FootlooseExamples.PubSub.Events;

namespace FootlooseExamples.PubSub.Subscriber
{
    public class TextMessageHandler : Footloose.PublishSubscribe.IHandleEventsOfType<TextMessage>
    {
        public void HandleEvent(TextMessage @event)
        {
            Console.WriteLine(Environment.NewLine + "Received event '{0}: {1}'..." + Environment.NewLine,
                                      @event.Timestamp.ToShortTimeString(),
                                      @event.Message);
        }
    }
}
