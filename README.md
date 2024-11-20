## Flurl Test for sync and async on old version and new version

---

we use .net framework 4.6.2 with flurl 2.4.2 and 4.0.2

---

### Flurl 2.4.2

#### Async
![flurl](./docs/old_async.png)

#### Async with `ConfigureAwait(false)`
![flurl](./docs/old_async_configure_await.png)

#### Sync 
![flurl](./docs/old_sync.png)

#### Sync with `ConfigureAwait(false)`
![flurl](./docs/old_sync_configure_await.png)

---

### Flurl 4.0.2

#### Async
![flurl](./docs/new_async.png)

#### Async with `ConfigureAwait(false)`
![flurl](./docs/new_async_configure_await.png)

#### Sync
![flurl](./docs/new_sync.png)

#### Sync with `ConfigureAwait(false)`
![flurl](./docs/new_sync_configure_await.png)

---

### Result

On both new and old version if we use sync like bellow code we get time out.  
Also `WithTimeout` not work on this code


```csharp
return _baseHttpAddress
        .WithHeaders(headers)
        .WithTimeout(TimeSpan.FromSeconds(TimeOutOnSecond))
        .AppendPathSegment(url)
        .SetQueryParams(query)
        .GetJsonAsync<TResponse>()
        .GetAwaiter().GetResult();
```

If add `ConfigureAwait(false)` problem will be solved.  

```csharp
return _baseHttpAddress
        .WithHeaders(headers)
        .WithTimeout(TimeSpan.FromSeconds(TimeOutOnSecond))
        .AppendPathSegment(url)
        .SetQueryParams(query)
        .GetJsonAsync<TResponse>()
        .ConfigureAwait(false).GetAwaiter().GetResult();
```