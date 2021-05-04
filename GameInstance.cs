using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pendu
{
    public class GameInstance
    {
        public List<char> Guesses { get; }

        public List<char> Misses { get; }

        public List<Word> Words { get; }

        public Word WordToGuess { get; }

        private int maxErrors { get; set; }

        private bool isWin { get; set; }

        private Random rnd;

        private string currentWordGuessed;

        
        public GameInstance(int maxErrors = 10)
        {
            rnd = new Random();
            this.maxErrors = maxErrors;

            Words = new List<Word>
            {
                new Word("Programmation"),
                new Word("Boucle"),
                new Word("String"),
                new Word("Pendu"),
                new Word("Informatique")
            };

            Guesses = new List<char>();
            Misses = new List<char>();

            WordToGuess = Words[rnd.Next(0, Words.Count)];

            Console.WriteLine("Le mot à trouver {0} lettres", WordToGuess.Length);
            currentWordGuessed = PrintWordToGuess();
        }

        
        public GameInstance(List<Word> words, int maxErrors)
        {
            rnd = new Random();

            this.maxErrors = maxErrors;

            Words = words;

            Guesses = new List<char>();
            Misses = new List<char>();

            WordToGuess = Words[rnd.Next(0, Words.Count)];

            Console.WriteLine("Le mot à deviner contient {0} lettres", WordToGuess.Length);
            currentWordGuessed = PrintWordToGuess();
        }

        
        public void Play()
        {
            while (!isWin)
            {
                Console.WriteLine("Donnez moi une lettre :");

                char letter = char.ToUpper(Console.ReadKey(true).KeyChar);

                int letterIndex = WordToGuess.GetIndexOf(letter);

                Console.WriteLine();

                if (letterIndex != -1)
                {
                    Console.WriteLine("Vous avez trouvé la lettre : {0}", letter);
                    Guesses.Add(letter);
                }
                else
                {
                    Console.WriteLine("La lettre {0} n'est pas dans le mot à deviner !", letter);
                    Misses.Add(letter);
                }

                Console.WriteLine($"Erreurs ({Misses.Count}) : {string.Join(", ", Misses)}");

                currentWordGuessed = PrintWordToGuess();

                if (currentWordGuessed.IndexOf('_') == -1)
                {
                    isWin = true;
                    Console.WriteLine("Gagné !");
                    Console.ReadKey();
                }

                if (Misses.Count >= maxErrors)
                {
                    Console.WriteLine("Perdu !");
                    Console.ReadKey();
                    break;
                }
            }
        }

        
        private string PrintWordToGuess()
        {
            string currentWordGuessed = "";

            for (int i = 0; i < WordToGuess.Length; i++)
            {
                if (Guesses.Contains(WordToGuess.Text[i]))
                {
                    currentWordGuessed += WordToGuess.Text[i];
                }
                else
                {
                    currentWordGuessed += "_";
                }
            }

            Console.WriteLine(currentWordGuessed);
            Console.WriteLine();

            return currentWordGuessed;
        }
    }
}
