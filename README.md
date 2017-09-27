# net-mock

A .NET network service mock framework, inspired by [moq](https://github.com/moq) syntax, providing the ability to mock REST and web socket APIs from within unit tests.

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
