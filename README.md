# MegaCityOne
Windows Advanced Authorization System

[![Join the chat at https://gitter.im/formix/MegaCityOne](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/formix/MegaCityOne?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

* This is not an authentication library.
* This project uses Judge Dredd metaphor to implement autorization.
* This library integrates:
    * seamlessly with Windows Standard Security;
    * prefectly with ASP.NET security;
    * without issue with anything that sets an IPrincipal to Thread.CurrentPrincipal;
    * with some work for anything else that is not Windows Standard Security.

This Project uses [Semantic Versioning](http://semver.org/).

# Usage

## Authentication

As stated in the premice, MegaCityOne has nothing to do with authentication.
You can use known libraries like 
[Owin](https://www.nuget.org/packages/Microsoft.Owin.Security.OAuth/) or 
[ASP.NET Authentication](https://msdn.microsoft.com/en-us/library/eeyk640h%28v=vs.140%29.aspx) 
to handle that.

### Web Application

Set the authenticated user's credentials in the current HttpContext or the 
current thread.

```c#
// Web applications:
System.Web.HttpContext.Current.User = new GenericPrincipal(
    new GenericIdentity("garry"),
    new string[] {"SiteAdmin", "Author", "SomeOtherUsefulGroup"});

// Or for desktop applications:
System.Threading.Thread.CurrentPrincipal = new GenericPrincipal(
    new GenericIdentity("garry"),
    new string[] {"SiteAdmin", "Author", "SomeOtherUsefulGroup"});
```

Note that by default, the running thread current user corresponds to a 
WindowsPrincipal with your Windows defined groups or IUSR_SOMETHING 
for web applications (unless using IIS Windows authentication).

## How to Judge

Judging perps and interpreting the Law require years of long lasting studies
and a lot of bloody practices in dangerous dark alleys. To help you do 
this dangerous task, MegaCityOne offers Judges that will execute your 
legislation in different ways.

### JudgeDredd

Understands Laws defined as lambda expressions.

```c#
JudgeDread judge = new JudgeDread();
dredd.Laws.Add(
    "AllowOpenChannel", 
    (principal, arguments) => principal.IsInRole("SiteAdmin"));
```

JudgeDredd can discover and load a set of laws from any library 
(i.e. assembly) having a JusticeDepartment (i.e. implementing 
the JusticeDepartment interface) defined in it.

### JudgeAnderson

Understands Laws defined in a JavaScript file.

```c#
JudgeAnderson judge = new JudgeAnderson();
anderson.Load(new FileInfo("laws.js"));
```

With Laws defined as functions returning true or false:

```javascript
//// laws.js \\\\

function AllowOpenChannel(principal) {
    return principal.IsInRole("SiteAdmin");
}
```

### Altering Application Behavior and Code Protection

For any Judge, you have the following two methods: Advise and Enforce. The
Advise method is called to display or hide user interface elements. The 
Enforce method is called to protect a section of code from illegal activity.
Enforce will throw an exception if the Law is broken.

```c#
if (judge.Advise("AllowOpenChannel"))
{
    // Display the Open Channel button...
}


// Before executing sensitive code, call Enforce

judge.Enforce("AllowOpenChannel");

// If your thread have not been killed by a LawgiverException here
// (SecurityException specialized class), you are ok to go on.

```

## Make Your Case

Sometimes, a Judge will need some arguments from you to help him distinguish
the truth and deliver Justice. Those arguments can be optionaly given to the 
Judge when you ask him an advice (Advise) or when you ask him to Enforce a 
Law.

```c#
JudgeDread judge = new JudgeDread();
dredd.Laws.Add(
    "CanSaveQuote", 
    (principal, arguments) 
        => 
        principal.IsInRole("SalesRep")) &&
        ((Quote)arguments[0]).Owner == principal.Identity.Name;


if (dredd.advise("CanSaveQuote", currentQuote))
{
    // show the save button
}
```

In this particular case, The JavaScript rule definition is way more 
convenient:

```javascript

// A JavaScript rule with an argument!

function CanSaveQuote(principal, quote) {
    return principal.IsInRole("SalesRep") && 
           quote.Owner === principal.Identity.Name
}
```

## I am The Law

MegaCityOne is easily extensible and IOC friendly. All public and protected 
Judge methods are virtual. You can extends existing Judges or create a 
brand new one from AbstractJudge or Judge interface if needs be.

# Example

[Bank account example using JudgeDredd](https://github.com/formix/MegaCityOne/blob/master/MegaCityOne.Examples/BankAccountExample.cs)

# Regarding Interfaces

Even if I come from a Java World, I tend to respect the unpleasant hungarian 
notation prefix "I" for interface names when coding .NET. But for this 
particular library, I found that the "I" prefix broke the coding storyline 
created by the JudgeDredd metaphore.

# One Last Word

> You got what you deserved, bum. There is no iso chamber in my MegaCityOne 
> implementation. Death sentences prevail.
 
