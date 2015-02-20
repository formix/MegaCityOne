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

This Project uses [Semantic Versioning](http://semver.org/).

# Usage

## Authentication

As stated in the premice, MegaCityOne has nothing to do with authentication.
You can use known libraries and standards like OAuth 
([Owin](https://www.nuget.org/packages/Microsoft.Owin.Security.OAuth/)) or 
[ASP.NET Authentication](https://msdn.microsoft.com/en-us/library/eeyk640h%28v=vs.140%29.aspx) 
existing services to handle that.

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
WindowsPrincipal for your user with your assignated groups or 
IUSR_SOMETHING for web applications (unless using IIS Windows 
authentication).

## How to Judge

Judging perps and interpreting the Law require years of long lasting studies
and a lot of bloody practices in dangerous dark alleys. To help you to do 
this dangerous task, MegaCityOne offers Judges that will do that for you in 
different ways.

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
Enforce will throw an exception if the Law is not respected.

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

[JudgeDredd example](https://github.com/formix/MegaCityOne/blob/master/MegaCityOne.Examples/BankAccountExample.cs)

## "I am The Law"

MegaCityOne is extension friendly. All public and protected Judge methods are
virtual. You can extends existing Judges easily or create a brand new one from
AbstractJudge or Judge interface if needs be.

# One Last Word

> You got what you deserved, bum. There is no iso chamber in my MegaCityOne 
> implementation. Death sentences prevail.
 
