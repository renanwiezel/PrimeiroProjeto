using System;

namespace StudiesGameSpace
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine("Welcome to the Door Management System!");
            Console.WriteLine("Please set an initial password for the door:");
            string password = Console.ReadLine();
            Door door = new Door(password, Door.StateOfDoor.Closed);
            DoorManagement doorManagement = new DoorManagement();
            doorManagement.Run(door);
        }
    }
    
    class DoorManagement
    {
        public void Run(Door door)
        {
            while (true)
            {
                Console.WriteLine($"Door actual status: {door.CurrentState}");
                ShowMenu();
                int choice = GetValidInt("Please choose an option...", 1, 6);
                switch (choice)
                {
                    case 1: 
                        OpenDoor(door); 
                        break;
                    case 2:
                        CloseDoor(door); 
                        break;
                    case 3:
                        LockTheDoor(door); 
                        break;
                    case 4:
                        Console.WriteLine("Enter the password to unlock the door:");
                        string unlockPassword = Console.ReadLine();
                        UnlockDoor(door, unlockPassword); 
                        ClearConsole();
                        break;
                    case 5:
                        Console.WriteLine("Enter the old password:");
                        string oldPassword = Console.ReadLine();
                        Console.WriteLine("Enter the new password:");
                        string newPassword = Console.ReadLine();
                        ChangePassword(door, oldPassword, newPassword); 
                        ClearConsole();
                        break;
                    case 6:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        ClearConsole();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        ClearConsole();
                        break;
                }
            }
        }
        private void ShowMenu()
        {
            Console.WriteLine("1. Open Door");
            Console.WriteLine("2. Close Door");
            Console.WriteLine("3. Lock Door");
            Console.WriteLine("4. Unlock Door");
            Console.WriteLine("5. Change Password");
            Console.WriteLine("6. Exit");
        }

        public void ClearConsole()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        public void OpenDoor(Door door)
        {
            if (door.CurrentState == Door.StateOfDoor.Closed && door.CurrentState != Door.StateOfDoor.Locked)
            {
                door.CurrentState = Door.StateOfDoor.Open;
                Console.WriteLine("The door is now open.");
            }
            else if (door.CurrentState == Door.StateOfDoor.Locked)
            {
                Console.WriteLine("The door cannot be opened because it is locked.");
            }
            else 
            {
                Console.WriteLine("The door is already open.");
            }
            ClearConsole();
        }

        public void CloseDoor(Door door)
        {
            if (door.CurrentState == Door.StateOfDoor.Open)
            {
                door.CurrentState = Door.StateOfDoor.Closed;
                Console.WriteLine("The door is now closed.");
            }
            else if (door.CurrentState == Door.StateOfDoor.Closed)
            {
                Console.WriteLine("The door is already closed.");
            }
            else
            {
                Console.WriteLine("The door is locked and cannot be closed.");
            }
            ClearConsole();
        }

        public void LockTheDoor (Door door)
        {
            if (door.CurrentState == Door.StateOfDoor.Closed)
            {
                door.CurrentState = Door.StateOfDoor.Locked;
                Console.WriteLine("The door is now locked.");
            }
            else if (door.CurrentState == Door.StateOfDoor.Open)
            {
                Console.WriteLine("The door cannot be locked because it is open.");
            }
            else
            {
                Console.WriteLine("The door is already locked.");
            }
            ClearConsole();
        }

        public void UnlockDoor(Door door, string password)
        {
        if (door.CurrentState == Door.StateOfDoor.Locked)
            {
            if (password == door.Password)
            {
            door.CurrentState = Door.StateOfDoor.Closed;
            Console.WriteLine("The door is now unlocked.");
            }
        else
        {
            Console.WriteLine("Incorrect password. The door remains locked.");
        }
        }
        else
        {
        Console.WriteLine("The door is not locked. Unlocking is not necessary.");
        }
        ClearConsole();
        }
        public void ChangePassword (Door door, string oldPassword, string newPassword)
        {
            if (oldPassword == door.Password)
            {
                door.Password = newPassword;
                Console.WriteLine("Password changed successfully.");
            }
            else
            {
                Console.WriteLine("Incorrect old password. Password change failed.");
            }
        }
        public static int GetValidInt(string prompt, int min = 0, int max = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Please enter a valid number between {min} and {max}.");
            }
        }
    }
    class Door
    {
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public Door(string password, StateOfDoor state)
        {
            _password = password;
            CurrentState = state;
        }

        public enum StateOfDoor
        {
            Closed,
            Open,
            Locked,
        }

        public StateOfDoor CurrentState { get; set; }
    }
}
