using System;
// ------ Mouse parking
//
//
//
namespace HangmanCA
{
    class Program
    {
        public static void Main(string[] args)
        {
            string acceptedChar = "abcdefghijklmnopqrstuvwxyz";
            var wordList = new List<string> { "apple", "pie", "can", "taste", "great", "sunday", "hangman" };
            var random = new Random();
            string chosenWord = wordList[random.Next(wordList.Count)];
            var guessesSoFar = new HashSet<char>();
            
            string guessWord = string.Concat(Enumerable.Repeat("_", chosenWord.Length));
            
            int wrongGuesses = 0;
            int maxGuesses = 5 + (chosenWord.Length / 3);

            // indicate application startup
            Console.WriteLine("Starting Hangman Game...");
            // Game setup and initlialization
            Console.WriteLine("Welcome to Hangman!");

            while (wrongGuesses < maxGuesses && guessWord.Contains('_'))
            {
                Console.WriteLine("\nCurrent Word: " + string.Join(" ", guessWord));
                Console.WriteLine("\nGuessed letters: " + string.Join(", ", guessesSoFar.ToArray()));
                Console.WriteLine($"Wrong guesses: {wrongGuesses} out of {maxGuesses}\n");
                
                var input = Console.ReadLine();
                if(string.IsNullOrEmpty(input) || input.Length != 1) {
                    Console.WriteLine("\nPlease enter an accepted character!");
                    continue;
                }
                
                char guessedLetter = input.ToLower()[0];
            
                // Error checking
                if(!acceptedChar.Contains(guessedLetter)) {
                    Console.WriteLine("\nPlease enter an accepted character!");
                    continue;
                }
                if(Char.IsWhiteSpace(guessedLetter)) {
                    Console.WriteLine("\nPlease do not enter a white space");
                    continue;
                }
                // Letter is already guessed
                if (guessesSoFar.Contains(guessedLetter)) {
                    Console.WriteLine("\nYou already guessed that letter!");
                }
                
                // Add guess letters
                guessesSoFar.Add(guessedLetter);
                
                if(chosenWord.Contains(guessedLetter) ){
                    Console.WriteLine("\nCorrect!");
                    
                    // Change guess word for loop 
                    for(int i = 0; i < chosenWord.Length; i++) {
                        if(chosenWord[i] == guessedLetter) {
                            guessWord = guessWord.Substring(0, i) + guessedLetter + guessWord.Substring(i + 1);
                        }
                    }
                }
                else{
                    
                    Console.WriteLine("\nIncorrect");
                    wrongGuesses++;
                }
            }
            if(guessWord.Contains('_') || wrongGuesses == maxGuesses) {
                Console.WriteLine($"\nBad luck 💢 :(\nThe correct word was... {chosenWord}!\n");
            } else {
                Console.WriteLine($"\nYou did it! :D 🎉\nThe guessed word was {chosenWord}!\n");
            }

            // indicate application shutdown
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine("Exiting Hangman Game...");
        }
    }
}
