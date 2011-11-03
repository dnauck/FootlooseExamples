Footloose XMPP Example

1.) Description

This example shows how to configure and create a client and service
using the XMPP(Jabber) transport channel with the Footloose fluent configuration API.
It also shows how to get information, such as the presence, about known service endpoints
and uses automatic service discovery to determine the correct uri of the service to call a method on.

Required: 2 XMPP accounts that have presence subscriptions to each other.


2.) How to build?

Use the provided "build-all.bat" in the Examples root directory or open the
"FootlooseExamples.Xmpp.sln" solution file with VS2010 and run
Build -> Build Solution in the VS2010 menu.



3.) How to run?

You'll find the binaries of the client in "Build\FootlooseExamples.Xmpp\Client".
Run the "FootlooseExamples.Xmpp.Client.exe".

The binaries of the service are located in "Build\FootlooseExamples.Xmpp\Service".
Run the "FootlooseExamples.Xmpp.Service.exe".

Both are WindowsForms applications. Enter the correct login information and click on "Connect".


4.) Further information

Footloose is released under the MIT/X11 license.

The latest release and documentation can be found on
the Footloose project website:

http://www.footloose-project.de/