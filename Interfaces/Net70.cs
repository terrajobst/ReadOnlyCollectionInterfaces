using System.Diagnostics.CodeAnalysis;

#if NET7_0

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

public interface IMyCollection<T> : IEnumerable<T>
{
    int Count { get; }
    bool IsReadOnly { get; }

    void Add(T item);
    void Clear();
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    bool Remove(T item);
}

public interface IMyList<T> : IMyCollection<T>
{
    T this[int index] { get; set; }

    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

public interface IMyDictionary<TKey, TValue> : IMyCollection<KeyValuePair<TKey, TValue>>
{
    TValue this[TKey key] { get; set; }
    ICollection<TKey> Keys { get; }
    ICollection<TValue> Values { get; }

    bool ContainsKey(TKey key);
    void Add(TKey key, TValue value);
    bool Remove(TKey key);
    bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
}

public interface IMySet<T> : IMyCollection<T>
{
    new bool Add(T item);

    void UnionWith(IEnumerable<T> other);
    void IntersectWith(IEnumerable<T> other);
    void ExceptWith(IEnumerable<T> other);
    void SymmetricExceptWith(IEnumerable<T> other);
    bool IsSubsetOf(IEnumerable<T> other);
    bool IsSupersetOf(IEnumerable<T> other);
    bool IsProperSupersetOf(IEnumerable<T> other);
    bool IsProperSubsetOf(IEnumerable<T> other);
    bool Overlaps(IEnumerable<T> other);
    bool SetEquals(IEnumerable<T> other);
}

#endif