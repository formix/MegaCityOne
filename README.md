# MegaCityOne
Windows Advanced Authorization System

[![Join the chat at https://gitter.im/formix/MegaCityOne](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/formix/MegaCityOne?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

* This is not an authentication library.
* This project uses Judge Dredd metaphors to implement autorizations.
* This library integrates:
    * seamlessly with Windows Standard Security;
    * prefectly with ASP.NET security;
    * without issue with anything that sets an IPrincipal to Thread.CurrentPrincipal;
    * with some work for anything else that is not Windows Standard Security.

# Usage

_While I'm editing this documentation, please consult the source code for 
detailed information, especially test cases in the MegacityOne.Tests
project. I paid special attention to document both functions and tests._

## Know who you are...

The first thing you can do with MegaCityOne is to know who you are:

```c#
Judge judge = new JudgeDredd();
Console.WriteLine(judge.Principal.Identity.Name);
```

# Contact

