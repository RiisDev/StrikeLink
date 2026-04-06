# LoginSecureService

Namespace: StrikeLink.Services.WebService

Provides functionality to retrieve a secure login token for authentication on supported operating systems.

```csharp
public class LoginSecureService : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [LoginSecureService](./strikelink/services/webservice/loginsecureservice.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

On Windows, administrator privileges are required to retrieve the secure login token. On Linux, no
 special privileges are needed. The service is not supported on other operating systems.

## Properties

### **LoginSecureToken**

Gets or sets the secure token used for authentication during login operations.

```csharp
public string LoginSecureToken { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **LoginSecureService()**

Initializes a new instance of the LoginSecureService class and retrieves a secure login token for the current
 operating system.

```csharp
public LoginSecureService()
```

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown if the current process does not have administrator privileges on Windows, or if the operating system is not
 supported.

**Remarks:**

On Windows, the constructor requires the process to be running with administrator privileges to
 retrieve the secure login token. On Linux, no special privileges are required. The constructor will throw an
 exception if called on an unsupported operating system.

## Methods

### **Dispose()**

```csharp
public void Dispose()
```

### **Dispose(Boolean)**

Releases the unmanaged resources used by the object and, optionally, releases the managed resources.

```csharp
protected void Dispose(bool disposing)
```

#### Parameters

`disposing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true to release both managed and unmanaged resources; false to release only unmanaged resources.

**Remarks:**

This method is called by public Dispose methods and the finalizer. When disposing is true, this
 method disposes all managed and unmanaged resources; when false, only unmanaged resources are released. Override
 this method to release resources specific to the derived class.
