# WSCT Core documentation.

## What is WSCT?
WSCT is a framework written in C# initialy used to allow any .net software to connect to [PC/SC][] smart card readers on windows through [`winscard.dll`][] library.
WSCT stands for WinSCard Tools.

It has evolved to allow any smart card reader to be used, even virtual smart cards.

The project is hosted on [Github](https://github.com/wsct/WSCT-Core).

Developed by S.Vernois @ [ENSICAEN][] / [GREYC][] since 2006 with the help of some students for proof of concepts.

## The projects

### WSCT
Defines the core abstractions allowing the access to readers and smart cards.

#### WSCT.Core namespace
Main abstractions:
* [`Core.ICardContext`][]
* [`Core.ICardChannel`][]
* `Core.ICardContextObservable` (extends `ICardContext` by adding activity observation through events)
* `Core.ICardChannelObservable` (extends `ICardChannel` by adding activity observation through events)

Decorators allowing the observation of an existing context or channel:
* `Core.CardContextObservable`
* `Core.CardChannelObservable`

#### WSCT.Stack namespace
Defines a layer architecture allows interception and modification of any ICard* method.
* `Stack.ICardContextLayer`
* `Stack.ICardChannelLayer`

Stack of layer implementation:
* `Stack.CardContextStack` (implements `ICardContext`)
* `Stack.CardChannelStack` (implements `ICardChannel`)

#### WSCT.ISO7816 namespace
Defines ISO/IEC 7816-4 command & response APDU:
* `ISO7816.CommandAPDU`
* `ISO7816.ResponseAPDU`
* `ISO7816.CommandResponsePair`

### WSCT.Helpers
Defines some helper methods that may be used by any WSCT project.

### WSCT.Wrapper

### WSCT.Wrapper.Desktop namespace
Defines an implementation of WSCT abstractions that can adapt to any desktop OS (MacOS, Linux, Windows):
* `Wrapper.Desktop.Core.CardContextCore` (implements `ICardContext`)
* `Wrapper.Desktop.Core.CardChannelCore` (implements `ICardChannel`)
* `Wrapper.Desktop.Core.CardContext` (implements `ICardContextObservable`)
* `Wrapper.Desktop.Core.CardChannel` (implements `ICardChannelObservable`)

Example:
```csharp
using WSCT.Wrapper.Desktop.Core;

var context = new CardContext();
// ...
var cardChannel = new CardChannel(context, "Some reader name");
// ...
```

#### WSCT.MacOSX
Defines the implementation of WSCT abstraction for PC/SC service on MacOS.

#### WSCT.PCSCLite32
Defines the implementation of WSCT abstraction for PC/SC service on 32 bits linux OS.

#### WSCT.PCSCLite64
Defines the implementation of WSCT abstraction for PC/SC service on 64 bits linux OS.

#### WSCT.WinSCard
Defines the implementation of WSCT abstraction for PC/SC service on Windows OS.

[PC/SC]: https://pcscworkgroup.com/
[`Core.ICardContext`]: xref:WSCT.Core.ICardContext
[`Core.ICardChannel`]: xref:WSCT.Core.ICardChannel
[`winscard.dll`]: https://docs.microsoft.com/en-us/windows/win32/api/winscard/
[ENSICAEN]: https://www.ensicaen.fr/
[GREYC]: https://www.greyc.fr/en/equipes/safe-2/
