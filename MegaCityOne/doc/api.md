## MegaCityOne ##

# T:MegaCityOne.AbstractJudge

 Basic implementation of a Judge. 



---
# T:MegaCityOne.Judge

 Defining a security engine basic interface. 



---
##### M:MegaCityOne.Judge.Advise(System.String,System.Object[])

 The Judge gives an adivce regarding a law taking in account some optional arguments for the current Principal. 

|Name | Description |
|-----|------|
|law: |The law to be advised.|
|arguments: |Any system state that could help the Judge to give a relevant advice regarding the law in question.|
Returns: True if the advised law is respected for the current Principal, given optional arguments. False otherwise.



---
##### M:MegaCityOne.Judge.Enforce(System.String,System.Object[])

 The Judge is in a situation where he have to give a Life or Death sentence regarding a law, given optional arguments provided, for the current Principal. 

|Name | Description |
|-----|------|
|law: |The law to be enforced.|
|arguments: |Any system state that could help the Judge to give a relevant sentence regarding the law in question.|
[[T:MegaCityOne.LawgiverException|T:MegaCityOne.LawgiverException]]:  Inherited from [[|T:System.Security.SecurityException]]. This exceptions is thrown at the face of current Principal if he is breaking the given law.



---
##### P:MegaCityOne.Judge.Principal

 The principal assigned to the current Judge. 



---
##### M:MegaCityOne.AbstractJudge.Advise(System.String,System.Object[])

 The advise method to be implemented by specialized Judges. 

|Name | Description |
|-----|------|
|law: |The name of a Law to be interpreted by the Judge.|
|arguments: |Optional arguments given to the Judge to interpret a Law properly.|
Returns: True if the Law is respected, False otherwise.



---
##### M:MegaCityOne.AbstractJudge.Enforce(System.String,System.Object[])

 Standard Law enforcement inplementation. This implementation will advise the given Law with the provided optional arguments. If the Law advises falsy, throws a LawgiverException. If it advises truthy then this method does nothing. 

|Name | Description |
|-----|------|
|law: |The name of a Law to be interpreted by the Judge.|
|arguments: |Optional arguments given to the Judge to interpret a Law properly.|
[[T:MegaCityOne.LawgiverException|T:MegaCityOne.LawgiverException]]: Thrown when the given Law advise falsy with the optional arguments.



---
##### P:MegaCityOne.AbstractJudge.Principal

 Gets or sets the principal currently under scrutiny of this Judge. After the Judge instanciation, the scrutinized principal corresponds to the principal found in [[|P:System.Threading.Thread.CurrentPrincipal]]. This is the Judge main target. During the course of action, you can bring another IPrincipal under scrutiny by setting this value. To take the Judge attention back to the main target, sets this property back to null. 



>Setting a value to this property do not override [[|P:System.Threading.Thread.CurrentPrincipal]].



---
# T:MegaCityOne.JusticeDepartment

 A JusticeDepartment instance must have a default constructor. 



---
##### M:MegaCityOne.JusticeDepartment.GetLaws

 Returns a dictionary containing Laws to be advised by JudgeDredd. 

Returns: A Law dictionary.



---
# T:MegaCityOne.JudgeDredd

 JudgeDredd is a Judge that uses Laws defined as lambda expressions. 



---
##### M:MegaCityOne.JudgeDredd.#ctor

 Instanciates JudgeDredd. 



---
##### M:MegaCityOne.JudgeDredd.Load(System.Collections.Generic.IDictionary{System.String,MegaCityOne.Law})

 Loads a new set of Laws in Dredd's helmet computer. The New Laws do not invalidate existing ones systematically. Instead, they are added to Dredd's embarked artificial intelligence. Note that a new Law having the same name as a pre-existing Law will override it. 

|Name | Description |
|-----|------|
|newLaws: |A set of new laws to add to Dredd's embarked AI|


---
##### M:MegaCityOne.JudgeDredd.Load(MegaCityOne.JusticeDepartment)

 Loads a set of laws obtained from the given JusticeDepartment. 

|Name | Description |
|-----|------|
|justiceDepartment: |The JusticeDepartment containing the laws to embark in Dredd's AI.|


---
##### M:MegaCityOne.JudgeDredd.Load(System.Reflection.Assembly)

 Seeks all JusticeDeparment from the library and loads the Laws they contains in Dredd's AI. 

|Name | Description |
|-----|------|
|library: |The library to search for JusticeDepartment |


---
##### M:MegaCityOne.JudgeDredd.Load(System.String)

 Search in the given path a library that may contains one or more JusticeDepartment. If found, adds the laws obtained to Dredd's AI. 

|Name | Description |
|-----|------|
|path: |The file path to the library containing the JusticeDepartment.|


---
##### M:MegaCityOne.JudgeDredd.Load

 Search for a JusticeDepartment implementation in the calling library. 



---
##### M:MegaCityOne.JudgeDredd.Advise(System.String,System.Object[])

 Dredd checks with it's AI and tells if the given law is repsected or not, given the provided arguments, for the current Principal. 

|Name | Description |
|-----|------|
|law: |The Law to be advised|
|arguments: |Optional arguments to be evaluated.|
Returns: 



---
##### P:MegaCityOne.JudgeDredd.Laws

 Laws contained in Dredd's embarked artificial intelligence. 



---
# T:MegaCityOne.Law

 Delegate used ot define a Law useable by JudgeDredd. 

|Name | Description |
|-----|------|
|principal: |The targetted principal for the given Law |
|arguments: |An array of arguments given to the Law. Can be an empty array and content depends on the Given Law implementation.|
Returns: True if the Law is respected, false otherwise.



---
# T:MegaCityOne.MessageEventArgs

 An event argument containing a message sent by the Javascript function "message". 



---
##### M:MegaCityOne.MessageEventArgs.#ctor

 Instanciate a MessageEventArgs. 



---
##### M:MegaCityOne.MessageEventArgs.#ctor(System.String)

 Initializes a new instance of JsMessageEventArgs. 

|Name | Description |
|-----|------|
|text: |The message text received from the internal Javascript engine.|


---
##### M:MegaCityOne.MessageEventArgs.ToString

 Returns a string representation of the current object. 

Returns: a string representation of the current object.



---
##### P:MegaCityOne.MessageEventArgs.Text

 Gets the message content. 



---
# T:MegaCityOne.MessageDelegate

 Delegate for JsMessage passing. 

|Name | Description |
|-----|------|
|source: |The source object of the message.|
|e: |The JsMessageEventArgs instance|


---
# T:MegaCityOne.JudgeAnderson

 JudgeAnderson runs an internal JavaScript interpreter to execute laws that have been defined in a Javacript file on the server. 



---
##### M:MegaCityOne.JudgeAnderson.#ctor

 Initializes a new instance of the javascript law engine. 



---
##### M:MegaCityOne.JudgeAnderson.Load(System.IO.FileInfo)

 Loads a law script from the specified file. 

|Name | Description |
|-----|------|
|file: |The file containing the javascript law to be loaded in the current principal.|


---
##### M:MegaCityOne.JudgeAnderson.Load(System.IO.TextReader)

 Loads a JavaScript law file definition. 

|Name | Description |
|-----|------|
|reader: |The reader containing the JavaScript code. |


---
##### M:MegaCityOne.JudgeAnderson.Load(System.String)

 Loads the law script received. 

|Name | Description |
|-----|------|
|script: |The javascript laws applied to the current principal.|


---
##### M:MegaCityOne.JudgeAnderson.Advise(System.String,System.Object[])

 Gives an advice based on a law defined in a JavaScript file. See [[|M:MegaCityOne.Judge.Advise(System.String,System.Object[])]] for more details. 

|Name | Description |
|-----|------|
|law: ||
|arguments: ||
Returns: 



---
##### M:MegaCityOne.JudgeAnderson.AddObject(System.String,System.Object)

 Adds an object to the JavaScript engine. 

|Name | Description |
|-----|------|
|name: |How the object shall be called in the script.|
|Name | Description |
|-----|------|
|target: |The object reference to be added.|


---
##### M:MegaCityOne.JudgeAnderson.OnMessage(MegaCityOne.MessageEventArgs)

 Raise the Message event. 

|Name | Description |
|-----|------|
|e: |The event arguments|


---
##### E:MegaCityOne.JudgeAnderson.Message

 Event launched when the "message(text)" function is called from the JavaScript. 



---
##### P:MegaCityOne.JudgeAnderson.IsInitialized

 Tells if JudgeAnderson internal script engine is ready to process Laws. Returns true after a Law file have been loaded. Returns false otherwise. 



---
# T:MegaCityOne.LawgiverException

 Exception launched by a Judge while Enforcing a Law. 



---
##### M:MegaCityOne.LawgiverException.#ctor

 Instanciate a basic LawgivedException. 



---
##### M:MegaCityOne.LawgiverException.#ctor(System.String)

 Instanciate a LawgiverException with the given message. 

|Name | Description |
|-----|------|
|message: |The exception message.|


---
##### M:MegaCityOne.LawgiverException.#ctor(System.String,System.Exception)

 Instanciate a Lawgiver exception with the given message and inner Exception. 

|Name | Description |
|-----|------|
|message: ||
|Name | Description |
|-----|------|
|inner: ||


---
##### M:MegaCityOne.LawgiverException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)

 Instanciate a LawgiverException in a deserialization context. 

|Name | Description |
|-----|------|
|info: |The serialization information.|
|Name | Description |
|-----|------|
|context: |The streaming context|


---



