# Console Hangman Game - 90 Minute Challenge

## Overview
Build a simple Hangman word guessing game using C# console application.

## Game Rules
- Player guesses letters to reveal a hidden word
- Correct guesses show the letter in the word
- Wrong guesses add to a counter (6 wrong guesses = game over)
- Win by guessing the complete word before running out of guesses

## Requirements - Minimum Viable Product (MVP)

### 1. Word Selection
- Create a simple array/list with exactly 10 words
- Use common 4-8 letter words: `["cat", "dog", "fish", "bird", "tree", "book", "game", "play", "work", "food"]`
- Pick one word randomly when the game starts

### 2. Display the Game State
Show this information each turn:
```
Word: c _ t
Guessed letters: a, e, i, c, t
Wrong guesses: 2 out of 6
```

### 3. Get Player Input
- Ask player to enter one letter
- Convert to lowercase
- Basic validation: reject if not a single letter

### 4. Process the Guess
- If letter is in the word → reveal all instances of that letter
- If letter is not in the word → add 1 to wrong guess counter
- Track all guessed letters to show the player

### 5. Check Win/Lose
- **Win**: All letters in the word have been revealed
- **Lose**: 6 wrong guesses reached
- Display appropriate message and end the game

### 6. Guess Tracking
Just show the wrong guess count:
```
Wrong guesses: 0 out of 6
```

## Sample Game Flow
```
=== HANGMAN GAME ===
Word: _ _ _
Guessed letters: 
Wrong guesses: 0 out of 6

Enter a letter: a
Not in the word!

Word: _ _ _
Guessed letters: a
Wrong guesses: 1 out of 6

Enter a letter: c
Good guess!

Word: c _ _
Guessed letters: a, c
Wrong guesses: 1 out of 6

Enter a letter: t
Good guess!

Word: c _ t
Guessed letters: a, c, t
Wrong guesses: 1 out of 6

Enter a letter: a
You already guessed that letter!

Enter a letter: o
Not in the word!

Word: c _ t
Guessed letters: a, c, t, o
Wrong guesses: 2 out of 6
```

## Success Criteria
✅ Game picks a random word and shows blanks  
✅ Player can enter letters  
✅ Correct guesses reveal letters in word  
✅ Wrong guesses increment counter  
✅ Game ends with win or lose message  
✅ No crashes with normal input  

## Tips for Success
1. **Start simple** - get basic input/output working first
2. **Test frequently** - run the program after each small change
3. **Don't overthink** - focus on the core requirements only
4. **Help each other** - pair program if someone gets stuck

## What NOT to Worry About
- Complex ASCII art hangman drawings
- Multiple rounds/play again
- Score tracking
- File I/O or persistence
- Advanced error handling
- Categories or difficulty levels

---

**Goal: Working hangman game in 90 minutes. Keep it simple and functional!**