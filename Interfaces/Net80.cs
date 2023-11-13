using System.Diagnostics.CodeAnalysis;

#if NET8_0

namespace Interfaces;

public interface IMyReadOnlyCollection<out T> : IEnumerable<T>
{
    int Count { get; }
}

public interface IMyReadOnlyList<out T> : IMyReadOnlyCollection<T>
{
    T this[int index] { get; }
}

public interface IMyReadOnlyDictionary<TKey, TValue> : IMyReadOnlyCollection<KeyValuePair<TKey, TValue>>
{
    bool ContainsKey(TKey key);
    bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);

    TValue this[TKey key] { get; }
    IEnumerable<TKey> Keys { get; }
    IEnumerable<TValue> Values { get; }
}

public interface IMyReadOnlySet<T> : IMyReadOnlyCollection<T>
{
    bool Contains(T item);
    bool IsProperSubsetOf(IEnumerable<T> other);
    bool IsProperSupersetOf(IEnumerable<T> other);
    bool IsSubsetOf(IEnumerable<T> other);
    bool IsSupersetOf(IEnumerable<T> other);
    bool Overlaps(IEnumerable<T> other);
    bool SetEquals(IEnumerable<T> other);
}

//------------------

public interface IMyCollection<T> : IMyReadOnlyCollection<T>
{
    int IMyReadOnlyCollection<T>.Count => Count;

    new int Count { get; }
    bool IsReadOnly { get; }

    void Add(T item);
    void Clear();
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    bool Remove(T item);
}

public interface IMyList<T> : IMyCollection<T>, IMyReadOnlyList<T>
{
    T IMyReadOnlyList<T>.this[int index] => this[index];

    new T this[int index] { get; set; }

    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

public interface IMyDictionary<TKey, TValue> : IMyCollection<KeyValuePair<TKey, TValue>>,
    IMyReadOnlyDictionary<TKey, TValue>
{
    bool IMyReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key) => ContainsKey(key);
    bool IMyReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => TryGetValue(key, out value);

    TValue IMyReadOnlyDictionary<TKey, TValue>.this[TKey key] => this[key];
    IEnumerable<TKey> IMyReadOnlyDictionary<TKey, TValue>.Keys => Keys;
    IEnumerable<TValue> IMyReadOnlyDictionary<TKey, TValue>.Values => Values;

    new TValue this[TKey key] { get; set; }
    new ICollection<TKey> Keys { get; }
    new ICollection<TValue> Values { get; }

    new bool ContainsKey(TKey key);
    void Add(TKey key, TValue value);
    bool Remove(TKey key);
    new bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
}

public interface IMySet<T> : IMyCollection<T>, IMyReadOnlySet<T>
{
    bool IMyReadOnlySet<T>.IsProperSubsetOf(IEnumerable<T> other) => IsProperSubsetOf(other);
    bool IMyReadOnlySet<T>.IsProperSupersetOf(IEnumerable<T> other) => IsProperSupersetOf(other);
    bool IMyReadOnlySet<T>.IsSubsetOf(IEnumerable<T> other) => IsSubsetOf(other);
    bool IMyReadOnlySet<T>.IsSupersetOf(IEnumerable<T> other) => IsSupersetOf(other);
    bool IMyReadOnlySet<T>.Overlaps(IEnumerable<T> other) => Overlaps(other);
    bool IMyReadOnlySet<T>.SetEquals(IEnumerable<T> other) => SetEquals(other);

    new bool Add(T item);

    void UnionWith(IEnumerable<T> other);
    void IntersectWith(IEnumerable<T> other);
    void ExceptWith(IEnumerable<T> other);
    void SymmetricExceptWith(IEnumerable<T> other);
    new bool IsSubsetOf(IEnumerable<T> other);
    new bool IsSupersetOf(IEnumerable<T> other);
    new bool IsProperSupersetOf(IEnumerable<T> other);
    new bool IsProperSubsetOf(IEnumerable<T> other);
    new bool Overlaps(IEnumerable<T> other);
    new bool SetEquals(IEnumerable<T> other);
}

#endif