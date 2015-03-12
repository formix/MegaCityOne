
# MegaCityOne


## AbstractJudge

Basic implementation of a Judge.


### Advise(law, arguments)

The advise method to be implemented by the specialized Judges.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The name of a Law to be interpreted by the Judge. |
| arguments | *System.Object[]*<br>Optional arguments given to the Judge to interpret a Law properly. |


#### Returns

True if the Law is respected, False otherwise.


### Enforce(law, arguments)

Standard Law enforcement inplementation. This implementation will advise the given Law with the provided optional arguments. If the Law advises falsely, throws a LawgiverException. If it advises truthy then this method does nothing.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The name of a Law to be interpreted by the Judge. |
| arguments | *System.Object[]*<br>Optional arguments given to the Judge to interpret a Law properly. |

*LawgiverException:* Thrown when the given Law advise falsely with the optional arguments.


### Principal

Gets or sets the principal currently under scrutiny of this Judge. After the Judge instanciation, the scrutinized principal corresponds to the principal found in System.Threading.Thread.CurrentPrincipal. This is the Judge main target. During the course of action, you can bring another IPrincipal under scrutiny by setting this value. To take the Judge attention back to the main target, sets this property back to null.


#### Remarks

Setting a value to this property do not override System.Threading.Thread.CurrentPrincipal.


## Judge

Defining a security engine basic interface.


### Advise(law, arguments)

The Judge gives an adivce regarding a law taking in account some optional arguments for the current Principal.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The law to be advised. |
| arguments | *System.Object[]*<br>Any system state that could help the Judge to give a relevant advice regarding the law in question. |


#### Returns

True if the advised law is respected for the current Principal, given optional arguments. False otherwise.


### Enforce(law, arguments)

The Judge is in a situation where he have to give a Life or Death sentence regarding a law, given optional arguments provided, for the current Principal.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The law to be enforced. |
| arguments | *System.Object[]*<br>Any system state that could help the Judge to give a relevant sentence regarding the law in question. |

*LawgiverException:*  Inherited from System.Security.SecurityException. This exceptions is thrown at the face of current Principal if he is breaking the given law.


### Principal

The principal assigned to the current Judge.


## JudgeAnderson

JudgeAnderson runs an internal JavaScript interpreter to execute laws that have been defined in a Javacript file on the server.


### Constructor

Initializes a new instance of the javascript law engine.


### AddObject(name, target)

Adds an object to the JavaScript engine.

| Name | Description |
| ---- | ----------- |
| name | *System.String*<br>How the object shall be called in the script. |
| target | *System.Object*<br>The object reference to be added. |

### Advise(law, arguments)

Gives an advice based on a law defined in a JavaScript file. See MegaCityOne.Judge.Advise for more details.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The law to be advised. |
| arguments | *System.Object[]*<br>Any system state that could help the Judge to give a relevant advice regarding the law in question. |


#### Returns

True if the advised law is respected for the current Principal, given optional arguments. False otherwise.


### IsInitialized

Tells if JudgeAnderson internal script engine is ready to process Laws. Returns true after a Law file have been loaded. Returns false otherwise.


### Load(file)

Loads a law script from the specified file.

| Name | Description |
| ---- | ----------- |
| file | *System.IO.FileInfo*<br>The file containing the javascript law to be loaded in the current principal. |

### Load(reader)

Loads a JavaScript law file definition.

| Name | Description |
| ---- | ----------- |
| reader | *System.IO.TextReader*<br>The reader containing the JavaScript code.  |

### Load(script)

Loads the law script received.

| Name | Description |
| ---- | ----------- |
| script | *System.String*<br>The javascript laws applied to the current principal. |

### Message

Event launched when the "message(text)" function is called from the JavaScript.


### OnMessage(e)

Raise the Message event.

| Name | Description |
| ---- | ----------- |
| e | *MegaCityOne.MessageEventArgs*<br>The event arguments |

## JudgeDredd

JudgeDredd is a Judge that uses Laws defined as lambda expressions.


### Constructor

Instanciates JudgeDredd.


### Advise(law, arguments)

Dredd checks with it's AI and tells if the given law is repsected or not, given the provided arguments, for the current Principal.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The law to be advised. |
| arguments | *System.Object[]*<br>Any system state that could help the Judge to give a relevant advice regarding the law in question. |


#### Returns

True if the advised law is respected for the current Principal, given optional arguments. False otherwise.


### Laws

Laws contained in Dredd's embarked artificial intelligence.


### Load

Search for a JusticeDepartment implementation in the calling library.


### Load(justiceDepartment)

Loads a set of laws obtained from the given JusticeDepartment.

| Name | Description |
| ---- | ----------- |
| justiceDepartment | *MegaCityOne.JusticeDepartment*<br>The JusticeDepartment containing the laws to embark in Dredd's AI. |

### Load(System.Collections.Generic.IDictionary{System.String,MegaCityOne.Law})

Loads a new set of Laws in Dredd's helmet computer. The New Laws do not invalidate existing ones systematically. Instead, they are added to Dredd's embarked artificial intelligence. Note that a new Law having the same name as a pre-existing Law will override it.

| Name | Description |
| ---- | ----------- |
| newLaws | *Unknown type*<br>A set of new laws to add to Dredd's embarked AI |

### Load(library)

Seeks all JusticeDeparment from the library and loads the Laws they contains in Dredd's AI.

| Name | Description |
| ---- | ----------- |
| library | *System.Reflection.Assembly*<br>The library to search for JusticeDepartment  |

### Load(path)

Search in the given path a library that may contains one or more JusticeDepartment. If found, adds the laws obtained to Dredd's AI.

| Name | Description |
| ---- | ----------- |
| path | *System.String*<br>The file path to the library containing the JusticeDepartment. |

## JusticeDepartment

A JusticeDepartment instance must have a default constructor.


### GetLaws

Returns a dictionary containing Laws to be advised by JudgeDredd.


#### Returns

A Law dictionary.


## Law

Delegate used ot define a Law useable by JudgeDredd.

| Name | Description |
| ---- | ----------- |
| principal | *Unknown type*<br>The targetted principal for the given Law  |
| arguments | *System.Object[]*<br>An array of arguments given to the Law. Can be an empty array and content depends on the Given Law implementation. |


#### Returns

True if the Law is respected, false otherwise.


## LawgiverException

Exception launched by a Judge while Enforcing a Law.


### Constructor

Instanciate a basic LawgivedException.


### Constructor(info, context)

Instanciate a LawgiverException in a deserialization context.

| Name | Description |
| ---- | ----------- |
| info | *System.Runtime.Serialization.SerializationInfo*<br>The serialization information. |
| context | *System.Runtime.Serialization.StreamingContext*<br>The streaming context |

### Constructor(message)

Instanciate a LawgiverException with the given message.

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>The exception message. |

### Constructor(message, inner)

Instanciate a Lawgiver exception with the given message and inner Exception.

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br> |
| inner | *System.Exception*<br> |

## MessageDelegate

Delegate for JsMessage passing.

| Name | Description |
| ---- | ----------- |
| source | *Unknown type*<br>The source object of the message. |
| e | *MegaCityOne.MessageEventArgs*<br>The JsMessageEventArgs instance |

## MessageEventArgs

An event argument containing a message sent by the Javascript function "message".


### Constructor

Instanciate a MessageEventArgs.


### Constructor(text)

Initializes a new instance of JsMessageEventArgs.

| Name | Description |
| ---- | ----------- |
| text | *System.String*<br>The message text received from the internal Javascript engine. |

### Text

Gets the message content.


### ToString

Returns a string representation of the current object.


#### Returns

a string representation of the current object.


## Mvc.Dispatcher

The dispatcher is responsible to check if a Judge is available for the call. If no Judge is available, a Judge will be summoned. Dispatched Judges must be returned to the pool by using the Dispatcher.Return method. Otherwise, the Dispatch method will summon a new Judge on each call. The Dispatcher as a JudgePool. This class is a singleton and cannot be instanciated. You must use the static member Dispatcher.Current to use an instance of this class.


### Current

The static dispatcher instance for the current application.


### Dispatch

Thread safe. Calling the dispatch method can trigger the Summon event if there is no Judge available in the pool. If this is the case, it is assumed that a Summon event handler will create a Judge and asign it to the SummonEventArgs.Respondent property. Otherwise, return an existing Judge from the pool.


#### Returns

A Judge available to answer the call.


### OnSummon(e)

Method used to fire a Summon event.

| Name | Description |
| ---- | ----------- |
| e | *MegaCityOne.Mvc.SummonEventArgs*<br>The event arguments. |

### Returns(judge)

Thread safe. Returns a dispatched judge to the pool. This method do not accept a judge that have not been dispatched by the current instance of the dispatcher.

| Name | Description |
| ---- | ----------- |
| judge | *MegaCityOne.Judge*<br>The judge that answered a previous call to Dispatch. |

### Summon

Event fired when there is no Judge available for the current thread id. The event handler is expected to create a Judge, provide it with laws and attach it the the event args.


## Mvc.JudgeAuthorizeAttribute

This attribute leverage MegaCityOne's Judge security for MVC applications. The rule to be advised is mandatory.


### Constructor(rule)

Creates an instance of a JudgeAuthorizeAttribute.

| Name | Description |
| ---- | ----------- |
| rule | *System.String*<br>The rule to be advised during the MVC authorize process. |

### OnAuthorization(filterContext)

This method executes authorization based on the Judge returned by the MegaCityOne.Mvc.Dispatcher.

| Name | Description |
| ---- | ----------- |
| filterContext | *System.Web.Mvc.AuthorizationContext*<br>The authorization context. |

### Rule

The rule to be advised by the Judge upon authorization request.


## Mvc.JudgeHelper

This static Judge is intended to be used as a Razor helper. It gets an available Judge from the Dispatcher and It can only advise.


### Advise(law, arguments)

Static method to be used inside a Razor rendered web page.

| Name | Description |
| ---- | ----------- |
| law | *System.String*<br>The law to be advized |
| arguments | *System.Object[]*<br>Optional arguments to hel the Judge give an advice. |


#### Returns




## Mvc.SummonDelegate

This delegate is used to define a Summon event.

| Name | Description |
| ---- | ----------- |
| source | *Unknown type*<br>The source of the event. |
| e | *MegaCityOne.Mvc.SummonEventArgs*<br>The Judge summon event args. |

## Mvc.SummonEventArgs

Event arguments for a Dispatcher.Summon event.


### Constructor

Creates an instance of SummonEventArgs.


### Respondent

The Judge who answers the summoning from the Dispatcher.


