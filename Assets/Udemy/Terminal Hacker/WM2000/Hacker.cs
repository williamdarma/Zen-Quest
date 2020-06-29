
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game Data
    string[] level1Password = { "books", "aisle", "shelf", "password", "font" };
    string[] level2Password = { "kirlia", "luxio", "lucario", "gardevoir", "empoleon" };
    enum enumScreenState { MainMenu, Pasword, EndPhase};
    enumScreenState currentScreen = enumScreenState.MainMenu;
    int level;
    string password;
    // Start is called before the first frame update
    void Start()
    {
        var greeting = "Hello Ben";
        ShowMainMenu();
    }
    
    void ShowMainMenu()
    {
        currentScreen = enumScreenState.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Pres 1 for the local Library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Enter your Selection: ");
    }
    void OnUserInput(string input)
    {
        //  Terminal.WriteLine("The user type : " + input);
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == enumScreenState.MainMenu)
        {
            RunMainenu(input);
        }
        else if (currentScreen == enumScreenState.Pasword)
        {
            CheckPassword(input);
        }
    }

    private void CheckPassword(string input)
    {
        if (input == password)
        {
 
            DisplayWinScreen();
        }
        else
        {
            StartGame();
        }
    }

    private void DisplayWinScreen()
    {
        currentScreen = enumScreenState.EndPhase;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    private void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Well Done pass level " + level);
                break;
            case 2:
                Terminal.WriteLine("Well Done pass level " + level);
                break;
        }
    }

    private void RunMainenu(string input)
    {
        bool isValicLEvelNumber = (input == "1" || input == "2");
        if (isValicLEvelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
        }
    }

    private void StartGame()
    {
        currentScreen = enumScreenState.Pasword;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Please enter your password, hint : " + password.Anagram());


    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                int index = Random.Range(0, level1Password.Length);
                password = level1Password[index];
                break;
            case 2:
                int index2 = Random.Range(0, level2Password.Length);
                password = level2Password[index2];
                break;
            default:
                print("Invalid level Number");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
