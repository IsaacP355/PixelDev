# Arcade using C++

We have made a C++ program that is a Arcade like program that gives the option to play between Blackjack, Rock Paper Scissors, or TicTacToe.

## Authors

- [@NoBugsHappyLife](https://github.com/NoBugsHappyLife)


- [@IsaacP355](https://github.com/IsaacP355)
## Features

- Expandability
- Game Selection
- Replayability


## How to Run Code

Prerequisites:
Make sure you have a C++ compiler installed on your machine.

Running the Code:

    1. Clone this repository to your local machine.

        git clone https://github.com/IsaacP355/cmps3350-lab05.git

    2. Navigate to the project directory.

        cd lab05

    3. Compile the code using your C++ compiler.

        g++ arcade_final.cpp -o arcade

    4. Run the compiled executable.

        ./arcade

Game Selection:

    Upon running the executable, you will be prompted to choose a game:

    1. Blackjack
    2. Rock Paper Scissors
    3. Tic Tac Toe
    4. Exit the Arcade

Enter the number corresponding to the game you want to play and follow the on-screen instructions.

Enjoy the games in the C++ Arcade!
## How users can get started with the project

Starting a project like this is accessible for individuals at any coding proficiency level. Begin by creating a simple initial interface, and gradually incorporate one game at a time. This iterative approach allows for a learning curve, making it easy to enhance your coding skills progressively.
## Blackjack Game Overview


Deck Setup:

- Standard 52-card deck initialization with shuffled cards.

Dealing Cards:

- Two cards dealt to the player and dealer at the start.

Player's Turn:

- Choose to "Hit" or "Stand" to build a hand without exceeding 21.

Dealer's Turn:

- Dealer reveals hidden card and draws until reaching 17.
Determining the Winner:

- Compare total hand values; closest to 21 without busting wins.
- Achieving a "Blackjack" (Ace + 10) results in an instant win.

User Interaction:

- Input decisions with 'H' for Hit, 'S' for Stand.
Game Flow:

- Rounds continue until a winner is decided or the player exits.
Return to Arcade:

- After each round, the player returns to the Arcade menu.




## Rock Paper Scissors Game Overview

Game Setup:

- Specify the number of rounds to play against a computer opponent.

Player's Choices:

- Input choices (1 for Rock, 2 for Paper, 3 for Scissors) for each round.

Opponent's Move:

- Computer generates a random move (Rock, Paper, or Scissors).

Outcome Determination:

- Compare player and computer moves for each round.
- Display ASCII art representations of choices for added visual appeal.

Scoring:

- Track player wins, computer wins, and ties throughout the rounds.

Game Summary:

- Display a summary after the specified number of rounds.
- Declare the overall winner based on total wins.

User Interaction:

- Navigate using the Arcade menu, selecting option 3 for Rock Paper Scissors.

Return to Arcade:

- After completing rounds, return to the Arcade menu with a brief delay.

## Tic Tac Toe Game Overview

Game Board:

- Display a 3x3 board for players to mark spots.

Player Turns:

- Alternating turns for Player 1 (O) and Player 2 (X).
- Choose an available spot on the board.

Winning Conditions:

- Check for horizontal, vertical, or diagonal matches to declare a winner.

Tie Detection:

- Recognize a tie when the board is full without a winner.

User Interaction:

- Choose Tic Tac Toe from the Arcade menu (option 2) to play.

Game Outcome:

- Display the winner or a tie at the end of each game.

Return to Arcade:

- Return to the Arcade menu after completing a game with a short delay.
## Features To Expand Upon Blackjack

Game Rules:

- Explain in detail the rules of Blackjack, including how card values are calculated, the objective of the game, and the significance of achieving a "Blackjack."
Strategy Tips:

- Offer basic strategy tips for players, such as when to Hit or Stand in different situations.

Scoring System:

- Clarify how the scoring system works, how wins are counted, and how ties are handled.
##  Features To Expand Upon Rock Paper Scissors

Overall Statistics Tracking:

- If our game keeps track of statistics (e.g., player wins, computer wins, ties), provide information on how players can view these statistics.
## Features To Expand Upon Tic Tac Toe
Game Mechanics:

- Elaborate on the rules of Tic Tac Toe, including how players take turns, win conditions, and the mechanics of marking spots on the board.
Strategies:

- Offer some basic strategies for playing Tic Tac Toe, emphasizing common winning patterns and how players can block opponents.
## Summary
- This is our collaborative C++ Arcade project featuring Blackjack, Rock Paper Scissors, and Tic Tac Toe made by Isaac Pitts and Harman Bal.

Contributors:

Level Design and Environmental Art: @IsaacP355 (Isaac Pitts ipitts@csub.edu)

User Interface Design and Sound Effects: @AleZaca (Alejandra Zacarias Villafan azacarias-villafan@csub.edu)

Core Game Mechanics: @lrojo1 (Lucia Rojo lrojo1@csub.edu)

User Authentication and Database: @NoBugsHappyLife (Harman Bal hbal1@csub.edu)
