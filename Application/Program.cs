using Implementations;

using Interfaces;

var collection = new MyKeyedCollection
{
    { "James", "Kirk" },
    { "Jean-Luc", "Picard" }
};

PrintMyCollection(collection);
PrintMyReadOnlyCollection(collection);

PrintMyList(collection);
PrintMyReadOnlyList(collection);

PrintMyDictionary(collection);
PrintMyReadOnlyDictionary(collection);

static void PrintMyCollection(IMyCollection<string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyCollection<string>");
    Console.WriteLine(items.Count);

    foreach (var item in items)
        Console.WriteLine(item);
}

static void PrintMyReadOnlyCollection(IMyReadOnlyCollection<string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyReadOnlyCollection<string>");
    Console.WriteLine(items.Count);

    foreach (var item in items)
    {
        if (items.Contains(item))
            Console.WriteLine(item);
    }
}

static void PrintMyList(IMyList<string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyList<string>");
    Console.WriteLine(items.Count);

    for (int i = 0; i < items.Count; i++)
    {
        if (items.Contains(items[i]))
            Console.WriteLine(items[i]);
    }
}

static void PrintMyReadOnlyList(IMyReadOnlyList<string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyReadOnlyList<string>");
    Console.WriteLine(items.Count);

    for (int i = 0; i < items.Count; i++)
    {
        if (items.Contains(items[i]))
            Console.WriteLine(items[i]);
    }
}

static void PrintMyDictionary(IMyDictionary<string, string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyDictionary<string, string>");
    Console.WriteLine(items.Count);

    foreach (var first in items.Keys)
        Console.WriteLine($"{first} {items[first]}");
}

static void PrintMyReadOnlyDictionary(IMyReadOnlyDictionary<string, string> items)
{
    Console.WriteLine();
    Console.WriteLine("IMyReadOnlyDictionary<string, string>");
    Console.WriteLine(items.Count);

    foreach (var first in items.Keys)
        Console.WriteLine($"{first} {items[first]}");
}