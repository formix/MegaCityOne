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

The previous snippet shall display your current user name. For example:
`FORMIX-PC\formix`.

Now, lets be a little fancier and lets see in which Windows groups your 
user is. Your result may differ than mine but should be quite similar.
Open a command line prompt and write the following command: `net localgroup`.

```
Aliases for \\FORMIX-PC

-------------------------------------------------------------------------------
*__vmware__
*Administrators
*Auditors
*Backup Operators
*Cryptographic Operators
*Distributed COM Users
*Editors
*Event Log Readers
*Guests
*HelpLibraryUpdaters
*IIS_IUSRS
*Network Configuration Operators
*Performance Log Users
*Performance Monitor Users
*Power Users
*Remote Desktop Users
*Replicator
*Users
*VisualSVN Replication Partners
*VisualSVN Repository Supervisors
*VisualSVN Server Admins
The command completed successfully.
```

# Contact

