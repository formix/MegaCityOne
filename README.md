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

Now, lets be a little fancier and find out in which Windows groups your 
user is in. Your result may differ than mine but should be quite similar.
Open a command line prompt and write the following command: `net localgroup`.

```
Aliases for \\FORMIX-PC

-------------------------------------------------------------------------------
*Administrators
*Backup Operators
*Cryptographic Operators
*Distributed COM Users
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

## "I am The Law..."

_In the following examples, please change "FORMIX-PC" and "formix" for values
relevant to your current Windows Principal/Computer/Domain names._

As stated earlier, MegaCityOne uses the Judge Dredd's universe metaphor to 
bring authorization to your applications. Judges (there is actually two of 
them implemented in MegaCityOne) will Advise or Enforce laws that you will
define differently depending on the Judge you are using. Our first example
uses JudgeDredd:

```c#
Judge judge = new JudgeDredd();
judge.Laws.Add("CanWithdrawMoney", 
    (principal, arguments) => principal.IsInRole("FORMIX-PC\\Users"));
```

Now, JudgeDredd have its first law to work with. Note that Windows 
Security roles and user names are always prefixed by the computer name 
(local logon) or the domain name (domain logon). This is not the case if 
you use ASP.NET security or a GenericPrincipal implementation.

The previous Lambda expression contains an "arguments" parameter that is
discussed further later. Lets forget it for now and have a look on how to 
put JudgeDredd to work:

```c#
bool canWithdraw = judge.Advise("CanWithdrawMoney");
Console.WriteLine("Does {0} can withdraw from a bank account: {1}", 
    judge.Principal.Identity.Name, canWithdraw);
```

The previous snippet shall display: 
```
Does FORMIX-PC\formix can withdraw from a bank account: True
```

Since the current user is in the "FORMIX-PC\Users" group, he will be allowed
to withdraw money from an account. This use case is exactly the same as a
system that needs to adjust it's user interface depending on the current 
user rights. Show/hide or enable/disable buttons, tabs, menu items etc. The 
Advise method is a way to play nice with the Judge. If he says no, then don't
give your user the temptation to press on the button, otherwise...

## "...Sentence: Death"

In Metro-City One, citizens often becomes perps and will try to play a trick 
on you, sneak in and steal your property, kill somebody or click an 
unauthorized button from behind. This is why all Judge can be instructed to
Enforce a law:

```c#
judge.Enforce("CanWithdrawMoney");
```

There is two outcome from now on: either you are in your good right and
everything is fine... or your are a prep and you will receive a salve of 
exploding bullets in your freaking hacker face, thanks to that good old
LawgiverException. The said LawgiverException (a specialized SecurityException
btw) containing your crime description and will kill your thread (unless caught
somewhere else on the stack). You got what you deserved, bum. There is no iso
chamber in my MegaCityOne implementation. Death sentences prevail.



