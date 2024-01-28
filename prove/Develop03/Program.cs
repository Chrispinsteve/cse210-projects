using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main()
    {
        // Create a scripture
        Scripture john316 = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
        // Display the complete scripture
        john316.Display();
        // Continue hiding words until all words are hidden or the user types quit
        while (!john316.AllWordsHidden())
        {
            // Prompt the user to press enter or type quit
            Console.WriteLine("Press enter to continue or type 'quit' to end.");
            string input = Console.ReadLine();
            if (input.ToLower().Equals("quit"))
            {
                Console.WriteLine("Program ended. Press any key to exit.");
                Console.ReadKey();
                return;
            }
            // Hide a few random words
            john316.HideRandomWords();
            // Clear the console screen and display the modified scripture
            Console.Clear();
            john316.Display();
        }
        Console.WriteLine("All words are hidden. Program ended. Press any key to exit.");
        Console.ReadKey();
    }
}
class Scripture
{
    private string _reference;
    private string _text;
    private List<Word> _words;
    public Scripture(string reference, string text)
    {
        _reference = reference;
        _text = text;
        InitializeWords();
    }
    private void InitializeWords()
    {
        // -------------------Split the text into words and create Word objects
        string[] wordArray = _text.Split(' ');
        _words = new List<Word>();
        foreach (string word in wordArray)
        {
            _words.Add(new Word(word));
        }
    }
    public void Display()
    {
        Console.WriteLine($"{_reference}:");
        foreach (Word word in _words)
        {
            Console.Write(word.IsHidden ? "----- " : $"{word.Value} ");
        }
        Console.WriteLine();
    }
    public bool AllWordsHidden()
    {
        // ------------------Check words hidden
        return _words.All(word => word.IsHidden);
    }
    public void HideRandomWords()
    {
        // ------------------random number 20% of total words)
        int wordsToHide = (int)(_words.Count * 0.2 * new Random().NextDouble());
        // Get a list of indices for words that are not already hidden
        List<int> visibleWordIndices = Enumerable.Range(0, _words.Count).Where(i => !_words[i].IsHidden).ToList();
        // -------------------Randomly select words to hide
        for (int i = 0; i < wordsToHide && visibleWordIndices.Count > 0; i++)
        {
            int randomIndex = new Random().Next(visibleWordIndices.Count);
            _words[visibleWordIndices[randomIndex]].IsHidden = true;
            visibleWordIndices.RemoveAt(randomIndex);
        }
    }
}
class Word
{
    public string Value { get; }
    public bool IsHidden { get; set; }
    public Word(string value)
    {
        Value = value;
        IsHidden = false;
    }
}
