//Написати програму підрахунку виразу в префіксній формі.

using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Lab#9");

        Console.WriteLine("1. Task 1");
        Console.WriteLine("2. Task 2");
        Console.WriteLine("3. Task 3");
        Console.WriteLine("4. Task 4");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter the prefix expression: ");
                string prefixExpression = Console.ReadLine();
                try
                {
                    double result = CalculatePrefixExpression(prefixExpression);
                    Console.WriteLine("Result: " + result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
            case "2":
                PrintWordsByStartingLetter();
                break;
            case "3":
                PrintWordsArray();
                break;
            case "4":
                MusicCatalog catalog = new MusicCatalog();

                catalog.AddCD("CD1");
                catalog.AddSongToCD("CD1", new Song("Song1", "Artist1"));
                catalog.AddSongToCD("CD1", new Song("Song2", "Artist2"));

                catalog.AddCD("CD2");
                catalog.AddSongToCD("CD2", new Song("Song3", "Artist1"));
                catalog.AddSongToCD("CD2", new Song("Song4", "Artist2"));

                catalog.PrintCatalog();

                Console.WriteLine("\nSearching for songs by Artist1:");
                catalog.SearchByArtist("Artist1");
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }

    static double CalculatePrefixExpression(string expression)
    {
        Stack<double> stack = new Stack<double>();
        string[] tokens = expression.Split(' ');

        for (int i = tokens.Length - 1; i >= 0; i--) 
        {
            string token = tokens[i];

            if (IsOperator(token)) 
            {
                double operand1 = stack.Pop();
                double operand2 = stack.Pop();
                double result = PerformOperation(token, operand1, operand2);
                stack.Push(result);
            }
            else
            {
                stack.Push(double.Parse(token));
            }
        }

        return stack.Pop();
    }

    static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    static double PerformOperation(string op, double operand1, double operand2)
    {
        switch (op)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            case "*":
                return operand1 * operand2;
            case "/":
                return operand1 / operand2;
            default:
                throw new ArgumentException("Unknown operator: " + op);
        }
    }
    static void PrintWordsByStartingLetter()
    {
        string filePath = @"C:\Users\Админ\Desktop\Csharplab1\Lab9\input.txt";

        try
        {
            Queue<string> capitalWords = new Queue<string>(); 
            Queue<string> lowercaseWords = new Queue<string>(); 

           
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words)
                    {
                        if (char.IsUpper(word[0]))
                            capitalWords.Enqueue(word);
                        else if (char.IsLower(word[0]))
                            lowercaseWords.Enqueue(word);
                    }
                }
            }

           
            Console.WriteLine("Words starting with capital letter:");
            while (capitalWords.Count > 0)
            {
                Console.WriteLine(capitalWords.Dequeue());
            }

           
            Console.WriteLine("\nWords starting with lowercase letter:");
            while (lowercaseWords.Count > 0)
            {
                Console.WriteLine(lowercaseWords.Dequeue());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    static void PrintWordsArray()
    {
        string filePath = @"C:\Users\Админ\Desktop\Csharplab1\Lab9\input.txt";

        try
        {
            ArrayList capitalWords = new ArrayList(); 
    ArrayList lowercaseWords = new ArrayList();

            
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
{
    string line;
    while ((line = sr.ReadLine()) != null)
    {
        string[] words = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {
            if (char.IsUpper(word[0]))
                capitalWords.Add(word);
            else if (char.IsLower(word[0]))
                lowercaseWords.Add(word);
        }
    }
}


Console.WriteLine("Words starting with capital letter:");
foreach (string word in capitalWords)
{
    Console.WriteLine(word);
}


Console.WriteLine("\nWords starting with lowercase letter:");
foreach (string word in lowercaseWords)
{
    Console.WriteLine(word);
}
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }


    class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }

        public Song(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }

        public override string ToString()
        {
            return $"{Title} - {Artist}";
        }
    }

    class MusicCD : IEnumerable
    {
        public string Title { get; set; }
        private ArrayList songs;

        public MusicCD(string title)
        {
            Title = title;
            songs = new ArrayList();
        }

        public void AddSong(Song song)
        {
            songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            songs.Remove(song);
        }

        public void PrintSongs()
        {
            Console.WriteLine($"Songs in {Title}:");
            foreach (Song song in songs)
            {
                Console.WriteLine(song);
            }
        }
        public IEnumerator GetEnumerator()
        {
            return songs.GetEnumerator();
        }
    }

    class MusicCatalog
    {
        private Hashtable catalog;

        public MusicCatalog()
        {
            catalog = new Hashtable();
        }

        public void AddCD(string title)
        {
            catalog[title] = new MusicCD(title);
        }

        public void RemoveCD(string title)
        {
            catalog.Remove(title);
        }

        public void AddSongToCD(string cdTitle, Song song)
        {
            if (catalog.ContainsKey(cdTitle))
            {
                MusicCD cd = (MusicCD)catalog[cdTitle];
                cd.AddSong(song);
            }
            else
            {
                Console.WriteLine($"CD '{cdTitle}' not found.");
            }
        }

        public void RemoveSongFromCD(string cdTitle, Song song)
        {
            if (catalog.ContainsKey(cdTitle))
            {
                MusicCD cd = (MusicCD)catalog[cdTitle];
                cd.RemoveSong(song);
            }
            else
            {
                Console.WriteLine($"CD '{cdTitle}' not found.");
            }
        }

        public void PrintCatalog()
        {
            Console.WriteLine("Music Catalog:");
            foreach (MusicCD cd in catalog.Values)
            {
                cd.PrintSongs();
            }
        }

        public void PrintCD(string title)
        {
            if (catalog.ContainsKey(title))
            {
                MusicCD cd = (MusicCD)catalog[title];
                cd.PrintSongs();
            }
            else
            {
                Console.WriteLine($"CD '{title}' not found.");
            }
        }

        public void SearchByArtist(string artist)
        {
            bool found = false;
            foreach (MusicCD cd in catalog.Values)
            {
                foreach (Song song in cd)
                {
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!found)
                        {
                            Console.WriteLine($"Songs by {artist} in the catalog:");
                            found = true;
                        }
                        Console.WriteLine(song);
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"No songs by {artist} found in the catalog.");
            }
        }
    }


}
