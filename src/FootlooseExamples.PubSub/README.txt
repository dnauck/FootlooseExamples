Footloose Publish/Subscribe Example

1.) Description

This example shows how to use the PubSub feature of Footloose. You can start more then
one publisher and/or subscriber if you like. Make sure you start all application and wait
until they're connected and service discovery is ready.

Required: 2 XMPP accounts that have presence subscriptions to each other.

You can run the FootlooseExamples\Tools\Prosody\prosody.bat to start a fully configured local XMPP server.


2.) How to build?

Use the provided "build-all.bat" in the Examples root directory or open the
"FootlooseExamples.PubSub.sln" solution file with VS2012 and run
Build -> Build Solution in the VS2012 menu.



3.) How to run?

You'll find the binaries of the publisher in "Build\FootlooseExamples.PubSub\Publisher".
Run the "FootlooseExamples.PubSub.Publisher.exe".

The binaries of the subscriber are located in "Build\FootlooseExamples.PubSub\Subscriber".
Run the "FootlooseExamples.PubSub.Subscriber.exe".


4.) Further information

FootlooseExamples is released under the MIT/X11 license.

The latest release and documentation can be found on
the Footloose project website:

http://www.footloose-project.de/