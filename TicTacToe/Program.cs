// See https://aka.ms/new-console-template for more information
//tictactoe
string[] grid = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }; //String array called grid with the size of up to 9
bool isPlayer1Turn = true;
int numTurns = 0;

while (!CheckVictory(grid) && numTurns != 9)
{
    PrintGrid(grid);

    if (isPlayer1Turn)
    {
        Console.WriteLine("Your turn!");
        string choice = Console.ReadLine();

        while (!grid.Contains(choice) || grid[Convert.ToInt32(choice) - 1] == "X" || grid[Convert.ToInt32(choice) - 1] == "O") //Check if the position is already taken or not, if it is, stops the player
        {
            PrintGrid(grid);
            Console.WriteLine("Position already taken. Please choose another position number.");
            choice = Console.ReadLine();
        }

        int gridIndex = Convert.ToInt32(choice) - 1; //Convert the choice to a valid grid index as the grid index starts with 0 then 1 etc
        grid[gridIndex] = "X";  //Player makes a move
        numTurns++;  
    }
    else
    {
        Console.WriteLine("AI's turn!");
        int aiMove = GetBestMove(grid); //Get AI's best move using minimax
        grid[aiMove] = "Y";  //AI makes a move
        numTurns++;  
    }

    isPlayer1Turn = !isPlayer1Turn; //Switches the turn to the other player or AI
}

PrintGrid(grid);

if (CheckVictory(grid))
{
    if (isPlayer1Turn)
    {
        Console.WriteLine("AI wins!");
    }
    else
    {
        Console.WriteLine("You win!");
    }
}
else
{
    Console.WriteLine("Tied!");
}
static bool CheckVictory(string[] grid) //Check if player will win
{
    bool row1 = grid[0] == grid[1] && grid[1] == grid[2];
    bool row2 = grid[3] == grid[4] && grid[4] == grid[5];
    bool row3 = grid[6] == grid[7] && grid[7] == grid[8];
    bool col1 = grid[0] == grid[3] && grid[3] == grid[6];
    bool col2 = grid[1] == grid[4] && grid[4] == grid[7];
    bool col3 = grid[2] == grid[5] && grid[5] == grid[8];
    bool diagDown = grid[0] == grid[4] && grid[4] == grid[8];
    bool diagUp = grid[6] == grid[4] && grid[4] == grid[2];

    return row1 || row2 || row3 || col1 || col2 || col3 || diagDown || diagUp;
}
static int GetBestMove(string[] grid)
{
    int bestScore = int.MinValue;
    int bestMove = -1;

    for (int i = 0; i < 9; i++) //Try each possible move for the AI
    {
        if (grid[i] != "X" && grid[i] != "Y") //If the spot is empty
        {
            grid[i] = "Y";  //AI will make a move
            int score = Minimax(grid, false);  //Retrieve the score for this move that has been made
            grid[i] = (i + 1).ToString(); //Undo the move

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = i;
            }
        }
    }

    return bestMove;
}

static int Minimax(string[] grid, bool isMaximizing)
{
    if (CheckVictory(grid))
    {
        return isMaximizing ? -1 : 1;  //AI wins (+1), Player wins (-1)
    }

    if (grid.All(x => x == "X" || x == "Y")) //If the grid is full it ends as tie
    {
        return 0; 
    }

    int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

    for (int i = 0; i < 9; i++)
    {
        if (grid[i] != "X" && grid[i] != "Y") //If the spot is empty
        {
            grid[i] = isMaximizing ? "Y" : "X"; //Make a move for AI or Player
            int score = Minimax(grid, !isMaximizing);// Recurse
            grid[i] = (i + 1).ToString(); //Undo the move

            if (isMaximizing)
            {
                bestScore = Math.Max(bestScore, score); //Maximize AI score
            }
            else
            {
                bestScore = Math.Min(bestScore, score); //Minimize Player score
            }
        }
    }

    return bestScore;
}
static void PrintGrid(string[] grid)
{
    for (int i = 0; i < 3; i++) //For loop to run less than three times
    {
        for (int j = 0; j < 3; j++)
        {
            Console.Write(grid[i * 3 + j] + "|");
        }
        Console.WriteLine();
        Console.WriteLine("------");
    }
}