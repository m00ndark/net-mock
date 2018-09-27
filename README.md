# net-mock

NetMock is a .NET network service mock framework, inspired by [Moq](https://github.com/moq) syntax, providing the ability to mock REST and web socket APIs from within a test framework of choice.

## Examples

A set of examples to show usage and possibilities are part of the solution, available [here](/src/Tests/NetMock.Tests/RestMockTest.cs).

## Status

The following table shows the implementation status of currenly planned features.

### Service mock
Component | Feature | Status
--- | --- | ---
Configuration | `ActivationStrategy`&nbsp;`{ Manual, AutomaticOnCreation }` (default is `AutomaticOnCreation`) | &#10003;
&nbsp; | `PrintReceivedRequestsOnTearDown` (default is `false`) | &#10003;
Mock creation | `CreateRestMock(int port, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateRestMock(string basePath, int port, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(string basePath, int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(int port, X509Certificate2 certificate, MockBehavior mockBehavior)` | &#10003;
&nbsp; | `CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate, MockBehavior mockBehavior)` | &#10003;

### REST mock
Component | Feature | Status
--- | --- | ---
HTTP methods | `Get `, `Post `, `Put `, `Delete `, `Head `, `Options `, `Trace `, `Connect` | &#10003;
Configuration | `StaticHeaders` | &#10003;
&nbsp; | `DefaultResponseStatusCode` (default is `NotImplemented`) | &#10003;
&nbsp; | `UndefinedQueryParameterHandling`&nbsp;`{ Ignore, Fail }` (default is `Fail`) | &#10003;
&nbsp; | `UndefinedHeaderHandling`&nbsp;`{ Ignore, Fail }` (default is `Ignore`) | &#10003;
&nbsp; | `MockBehavior`&nbsp;`{ Loose, Strict }` (default is `Loose`) | &#10003;
&nbsp; | `InterpretBodyAsJson` (default is `true`) | &#10003;
Request setup | `Setup(Method method, string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupGet(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupPost(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupPut(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupDelete(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupHead(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupOptions(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupTrace(string path, params IMatch[] matches)` | &#10003;
&nbsp; | `SetupConnect(string path, params IMatch[] matches)` | &#10003;
Request verification | `Verify(Method method, string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyGet(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyPost(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyPut(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyDelete(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyHead(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyOptions(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyTrace(string path, params IMatch[] matches, Times times)` | &#10003;
&nbsp; | `VerifyConnect(string path, params IMatch[] matches, Times times)` | &#10003;
Response setup | `Returns(object body)` | &#10003;
&nbsp; | `Returns(int statusCode, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(object body, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns(int statusCode, object body, params AttachedHeader[] headers)` | &#10003;
&nbsp; | `Returns<T1>(Func<T1, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1>(int statusCode, Func<T1, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2>(Func<T1, T2, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2>(int statusCode, Func<T1, T2, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3>(Func<T1, T2, T3, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3>(int statusCode, Func<T1, T2, T3, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3, T4>(int statusCode, Func<T1, T2, T3, T4, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, object> bodyProvider, params AttachedHeader[] headers)` |
&nbsp; | `Returns<T1, T2, T3, T4, T5>(int statusCode, Func<T1, T2, T3, T4, T5, object> bodyProvider, params AttachedHeader[] headers)` |
Parameter matching | `Parameter.Is(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.Is(string name, Func<string, bool> condition)` | &#10003;
&nbsp; | `Parameter.IsNot(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.IsAny(string name)` | &#10003;
&nbsp; | `Parameter.IsAny<TValue>(string name)` | &#10003;
&nbsp; | `Parameter.StartsWith(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.EndsWith(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.Contains(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.NotContains(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.StartsWithWord(string name, string word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.EndsWithWord(string name, string word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.ContainsWord(string name, string word, CompareCase compareCase)` | &#10003;
&nbsp; | `Parameter.NotContainsWord(string name, string word, CompareCase compareCase)` | &#10003;
Header matching | `Header.Is(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.Is(string name, Func<string, bool> condition)` | &#10003;
&nbsp; | `Header.IsNot(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.IsSet(string name)` | &#10003;
&nbsp; | `Header.IsNotSet(string name)` | &#10003;
&nbsp; | `Header.Contains(string name, string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Header.NotContains(string name, string value, CompareCase compareCase)` | &#10003;
Body matching | `Body.Is(object value)` | &#10003;
&nbsp; | `Body.Is(string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.Is(Func<string, bool> condition)` | &#10003;
&nbsp; | `Body.Is<TValue>(Func<TValue, bool> condition)` |
&nbsp; | `Body.IsNot(object value)` | &#10003;
&nbsp; | `Body.IsNot(string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.IsEmpty()` | &#10003;
&nbsp; | `Body.IsNotEmpty()` | &#10003;
&nbsp; | `Body.Contains(string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.NotContains(string value, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.ContainsWord(string word, CompareCase compareCase)` | &#10003;
&nbsp; | `Body.NotContainsWord(string word, CompareCase compareCase)` | &#10003;
Verification hit count matching | `Times.Never` | &#10003;
&nbsp; | `Times.Once` | &#10003;
&nbsp; | `Times.Twice` | &#10003;
&nbsp; | `Times.AtLeastOnce` |
&nbsp; | `Times.AtMostOnce` |
&nbsp; | `Times.Exactly(int callCount)` | &#10003;
&nbsp; | `Times.AtLeast(int callCount)` |
&nbsp; | `Times.AtMost(int callCount)` |
&nbsp; | `Times.Between(int callCountFrom, int callCountTo)` |

<!--
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
&nbsp; | `` | &#10003;
-->

### Web socket mock

No implementation started.

## Inspiration

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: false);
restMock.Setup(Method.Get, "/alive").Returns(new AliveMessage());
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Get, "/alive", Times.Once);

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: new User() { UserId = "abc" })
        .Returns(new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc" });
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Post, "/user", Times.Once)

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = 'abc' }")
        .Returns("{ 'id' = '698fa58a-8e9c-4607-b6c3-b5da19751a04', 'userId' = 'abc' }");
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = 'xyz' }")
        .Returns("{ 'id' = '607a755d-3ded-4a42-9a96-03d9ecfd31da', 'userId' = 'xyz' }");
serviceMock.Activate();

// act
...

// assert
restMock.Verify(Method.Post, "/user", Times.Twice);
restMock.Verify(Method.Post, "/user", body: "{ 'userId' = 'abc' }", Times.Once);
restMock.Verify(Method.Post, "/user", body: "{ 'userId' = 'xyz' }", Times.Once);

// teardown
serviceMock.Teardown();
```

```csharp
// arrange
int getUserCallCount = 0;
string encryptedConnectionString = "...";
ServiceMock serviceMock = new ServiceMock();
serviceMock.InstallCerts(certFile);
RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, useSSL: true);
restMock.Setup(Method.Post, "/user", body: "{ 'userId' = '' }")
        .ReturnsError(400);
restMock.Setup(Method.Post, "/user", body: "{ 'userDirectory' = '' }")
        .ReturnsError(400, body: "User directory missing, what were you thinking?");
restMock.Setup(Method.Get, "/user/{id}", Parameter.Is("id", "698fa58a-8e9c-4607-b6c3-b5da19751a04"))
        .Returns(new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc" });
restMock.Setup(Method.Get, "/user")
        .Callback(() => { getUserCallCount++; });
restMock.Setup(Method.Get, "/user/detail?filter={filter}&orderby={orderby}",
               Parameter.Is("filter", "userDirectory eq 'acme'", CompareCase.Insensitive),
               Parameter.EndsWithWord("orderby", "desc", CompareCase.Insensitive))
        .Returns(new[]
  {
     new User() { ID = new Guid("607a755d-3ded-4a42-9a96-03d9ecfd31da"), UserId = "xyz", UserDirectory = "ACME" },
     new User() { ID = new Guid("698fa58a-8e9c-4607-b6c3-b5da19751a04"), UserId = "abc", UserDirectory = "ACME" }
  });
restMock.Setup(Method.Get, "/product?filter={filter}&orderby={orderby}",
               Parameter.Is("filter", filter => filter.Split(' ').Any(word => word.ToLower() == "and")),
               Parameter.IsAny("orderby"))
        .ReturnsError(500);
restMock.Setup(Method.Get, "/secure/item",
               Header.Is("X-Acme-User", "UserDirectory=INTERNAL; UserId=sa", CompareCase.Insensitive))
        .Returns(new[] { new DataConnection() { Connectionstring = Decrypt(encryptedConnectionString) } },
                 Header.Attach("Cache-Control", "private"));
restMock.Setup(Method.Get, "/secure/item",
               Header.IsNot("X-Acme-User", "UserDirectory=INTERNAL; UserId=sa", CompareCase.Insensitive),
               Header.IsNotSet("X-Acme-Security"))
        .Returns(new[] { new DataConnection() { Connectionstring = encryptedConnectionString } });
serviceMock.Activate();

// act
...

// assert
Assert.AreEqual(5, getUserCallCount);
restMock.Verify(Method.Get, "/user/detail?filter={filter}&orderby={orderby}", Times.Never,
                Parameter.IsAny("filter"),
                Parameter.ContainsWord("orderby", "userDirectory", CompareCase.Insensitive));

// teardown
serviceMock.Teardown();
```
