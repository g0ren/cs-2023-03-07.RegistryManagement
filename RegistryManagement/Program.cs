using System.Text;
using System.Xml.Linq;

namespace RegistryManagement
{
    class Program
    {
        public static void Main()
        {
            RegistryManager registryManager = new RegistryManager();
            Console.WriteLine("Welcome to console registry manager!\n");
            Console.WriteLine("Warning: some actions may cause harm to your" +
                "system and require Administrator privileges");
            while (true)
            {
                Console.WriteLine($"Selected key is {registryManager.SelectedKeyName}");
                Console.WriteLine("Enter command");
                Console.WriteLine("1 - Select a key");
                Console.WriteLine("2 - List all values of selected key in format \"name: value\"");
                Console.WriteLine("3 - Create or edit a value in selected key");
                Console.WriteLine("4 - Create a new subkey in selected key");
                Console.WriteLine("5 - Delete a subkey in selected key with all its children");
                Console.WriteLine("0 - Exit");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "0":
                        registryManager.Exit();
                        return;
                    case "1":
                        registryManager.NavigationLoop();
                        break;
                    case "2":
                        registryManager.GetAllValues();
                        break;
                    case "3":
                        Console.WriteLine("Enter the name of the value you'd like to edit. " +
                            "If the value does not exist it will be created");
                        string valueName = Console.ReadLine();
                        Console.WriteLine($"Enter the value of {valueName}");
                        string valueValue = Console.ReadLine();
                        registryManager.CreateOrEditValue(valueName,valueValue);
                        break;
                    case "4":
                        Console.WriteLine("Enter new key's name");
                        registryManager.CreateSubKey(Console.ReadLine());
                        break;
                    case "5":
                        Console.WriteLine("Enter the name of the subkey to delete");
                        registryManager.DeleteSubKey(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("Unknown command!");
                        break;
                }
            }
        }
    }
}