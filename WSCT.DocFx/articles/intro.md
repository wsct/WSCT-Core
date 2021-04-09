# A basic example

```csharp
// Connect to PC/SC
var context = new CardContext();
context.Establish();

// Get installed readers
context.ListReaders("");
var allReaders = context.Readers;

// Connect to an ISO7816-4 card in the last reader found
var rawChannel = new CardChannel(context, context.Readers.Last());
var channel = new CardChannelIso7816(rawChannel);
channel.Connect(ShareMode.Exclusive, Protocol.Any);

// Build a SELECT command
var capdu = new SelectCommand(
	SelectCommand.SelectionMode.SelectDFName,
	SelectCommand.FileOccurrence.FirstOrOnly,
	SelectCommand.FileControlInformation.ReturnFci, 
	"A0 00 00  01 51".FromHexa()
	);

// Send it in a CRP an get the response back
var crp = new CommandResponsePair(capdu);
crp.Transmit(channel);
var rapdu = crp.RApdu;

// Unpower the card
channel.Disconnect(Disposition.UnpowerCard);

// Disconnect from PC/SC
context.Release();
```