using Interfaces;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Implementations;

public class MyKeyedCollection : IMyDictionary<string, string>, IMyList<string>
{
    private readonly Dictionary<string, string> _dictionary = new();
    private readonly List<string> _list = new();

    public string this[string key]
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }
    public string this[int index]
    {
        get => _list[index];
        set => throw new NotSupportedException();
    }

    public ICollection<string> Keys => _dictionary.Keys;

    public ICollection<string> Values => _dictionary.Values;

    public int Count => _dictionary.Count;

    public bool IsReadOnly => false;

    public void Add(string key, string value)
    {
        _dictionary.Add(key, value);
        _list.Add(key);
    }

    public void Add(KeyValuePair<string, string> item)
    {
        Add(item.Key, item.Value);
    }

    void IMyCollection<string>.Add(string item)
    {
        Add(item, string.Empty);
    }

    public void Clear()
    {
        _dictionary.Clear();
        _list.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _dictionary.Contains(item);
    }

    public bool Contains(string item)
    {
        return _dictionary.ContainsKey(item);
    }

    public bool ContainsKey(string key)
    {
        return _dictionary.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<string, string>>)_dictionary).CopyTo(array, arrayIndex);
    }

    public void CopyTo(string[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    public int IndexOf(string item)
    {
        return _list.IndexOf(item);
    }

    public void Insert(int index, string item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(string key)
    {
        var result = _dictionary.Remove(key);
        _list.Remove(key);
        return result;
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        var result = _dictionary.Remove(item.Key);
        _list.Remove(item.Key);
        return result;
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<string> IEnumerable<string>.GetEnumerator()
    {
        return _list.GetEnumerator();
    }
}