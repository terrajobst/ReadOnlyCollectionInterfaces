# Testing implementing read-only collection interfaces

This repo tests what would happen if we were to implement the read-only
interfaces from the mutable interfaces. To simulate this, there are three
projects:

* `Interfaces`. First, this repo re-defines the existing interfaces using a `My`
  prefix. The `Interfaces` project is multi-targeted defining the interfaces
  as-is for .NET 7.0 and extending the read-only flavors from the mutable ones
  for .NET 8.0.

* `Implementations`. Then there is an `Implementations` project that only
  targets .NET 7.0, thus simulating existing user code that was built for
  today's world where the mutable interfaces aren't extending the read-only
  ones.

* `Application`. Finally, there is an `Application` project that is targeting
  .NET 8.0, thus simulating what happens when existing code is loaded and run on
  a newer runtime where the mutable interfaces extend the read-only ones.

> [!NOTE]
> We only use `net7.0` and `net8.0` for convenience of testing with the current
> .NET SDK, which is .NET 8.0. There is no goal to port this to .NET 8.0. The
> new interface shape is for .NET 9.0 and later only.

## Interfaces

```C#
interface IMyReadOnlyCollection<out T> : IEnumerable<T> {}
interface IMyReadOnlyList<out T> : IMyReadOnlyCollection<T> {}
interface IMyReadOnlyDictionary<TKey, TValue> : IMyReadOnlyCollection<KeyValuePair<TKey, TValue>> {}
interface IMyReadOnlySet<T> : IMyReadOnlyCollection<T> {}

#if NET7_0

// Current shape, mutable interfaces don't extend read-only ones
public interface IMyCollection<T> : IEnumerable<T> {}
public interface IMyList<T> : IMyCollection<T> {}
public interface IMyDictionary<TKey, TValue> : IMyCollection<KeyValuePair<TKey, TValue>> {}
public interface IMySet<T> : IMyCollection<T> {}

#elif NET8_0

// Desired shape, mutable interfaces do extend read-only ones
interface IMyCollection<T> : IMyReadOnlyCollection<T> {}
interface IMyList<T> : IMyCollection<T>, IMyReadOnlyList<T> {}
interface IMyDictionary<TKey, TValue> : IMyCollection<KeyValuePair<TKey, TValue>>, IMyReadOnlyDictionary<TKey, TValue> {}
interface IMySet<T> : IMyCollection<T>, IMyReadOnlySet<T> {}

#endif
```

## Implementations

The most complex case we could think of to create a shared diamond is a
`KeyedCollection` that implements both `IMyDictionary<,>` and `IMyList<>`:

```C#
public class MyKeyedCollection : IMyDictionary<string, string>, IMyList<string> {}
```