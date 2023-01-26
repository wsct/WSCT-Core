# WSCT-Core

Public repository for WSCT Core project.

Developed by S.Vernois @ [ENSICAEN](https://www.ensicaen.fr) / GREYC since 2006 with the help of some students for proof of concepts.

WSCT Core API documentation is available from this url: http://wsct.github.io/WSCT-Core

## Nuget packages

### WSCT Core  [![NuGet Badge](https://buildstats.info/nuget/WSCT.Core)](https://www.nuget.org/packages/WSCT.Core/)

This is the main API of this project.
It defines:

* Raw APDU command and response
* ISO7816-4 APDU
* Smart card reader and card access API

> Warning: this package alone doesn't give access to a concrete smart card reader. Use next package for that.

### WSCT Wrapper for Desktop [![NuGet Badge](https://buildstats.info/nuget/WSCT.Wrapper.Desktop)](https://www.nuget.org/packages/WSCT.Wrapper.Desktop/)

This package seamlessly wraps native PC/SC libraries.

**Supported OS :**

* From *Windows XP* to *Windows 11* using *.NET 6+* or *Microsoft .NET Framework 4.6.2+* or *Mono 3+*
* *Linux* 32 and 64 bits based OS using *.NET 6+* or *Mono 3+*
* *Mac OSX* using *.NET 6+* or *Mono 3+*

## Example code
  
```csharp
// Connect to PC/SC
var context = new CardContext();
context.Establish();

// Get installed readers
context.ListReaders("");
var allReaders = context.Readers;

// Connect to an ISO7816-4 card in the last reader found
var channel = new CardChannel(context, context.Readers.Last());
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

## Prototyping with some fluent extensions

Some extensions methods located in WSCT.Core.Fluent.Helpers namespace can be used to ease prototyping (fail fast fail safe while writing less code):

```csharp
CardContext? cardContext = null;
CardChannel? cardChannel = null;
try
{
  // Connect to PC/SC
  cardContext = new new CardContext();

  cardContext
    .Establish()
    .ThrowIfNotSuccess();

  cardContext
    .ListReaderGroups()
    .ThrowIfNotSuccess();

  cardContext
    .ListReaders(cardContext.Groups[0])
    .ThrowIfNotSuccess();
  
  cardChannel = new CardChannel(cardContext, cardContext.Readers[0]);
  
  new CommandAPDU("00 A4 04 00 07 F0 57 53 43 54 2E 30")
    .Transmit(cardChannel)
    .ThrowIfNotSuccess()
    .ThrowIfSWNot9000()
    .If(r => r.Sw1 == 0x61, (c, r) => Console.WriteLine($"{r.Sw2} bytes are waiting to be retrieved"));

  new CommandAPDU("00 C0 00 00 08")
    .Transmit(cardChannel)
    .ThrowIfNotSuccess()
    .ThrowIfSWNot9000();
}
finally
{
  cardChannel?
    .Disconnect(Disposition.UnpowerCard);

  cardContext?
    .Release();
}
```

## External contributors

* [@mistial-dev](https://github.com/mistial-dev): support of extended C-APDU.
