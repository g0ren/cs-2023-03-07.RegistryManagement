using Microsoft.Win32;

namespace RegistryManagement
{
    class RegistryManager
    {
        RegistryKey?[] keys = new RegistryKey?[]
        {
            null,
            Registry.ClassesRoot,
            Registry.CurrentUser,
            Registry.LocalMachine,
            Registry.Users
        };

        RegistryKey? selectedKey = null;
        //StringBuilder selectedKeyNameBuilder = new StringBuilder();
        public string SelectedKeyName
        {
            get 
            { 
                if(selectedKey == null)
                {
                    return "None";
                }
                else 
                {
                    return selectedKey.Name;
                }
            }
        }

        public void Exit()
        {
            if (selectedKey != null) 
            { 
                selectedKey.Close(); 
            }    
            selectedKey = null;
        }

        public RegistryKey? Navigation()
        {
            Console.WriteLine("Select key");
            for(int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == null)
                {
                    Console.WriteLine($"{i} Exit");
                }
                else
                {
                    Console.WriteLine($"{i} {keys[i].Name}");
                }
            }
            int selection = Convert.ToInt32(Console.ReadLine());
            return keys[selection];
        }

        public RegistryKey? Navigation(RegistryKey? rk)
        {

            List<string?> subkeys = new List<string?> { null };
            foreach(string name in rk.GetSubKeyNames())
            {
                subkeys.Add(name);
            }
            Console.WriteLine("Select key");
            for (int i = 0; i < subkeys.Count; i++)
            {
                if (subkeys[i] == null)
                {
                    Console.WriteLine($"{i} Exit");
                }
                else
                {
                    Console.WriteLine($"{i} {subkeys[i]}");
                }
            }
            int selection = Convert.ToInt32(Console.ReadLine());
            var keyname = subkeys[selection];
            RegistryKey key;
            try
            {
                key = rk.OpenSubKey(keyname);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return key;
        }

        public void NavigationLoop()
        {
            Exit();
            RegistryKey? currentKey = Navigation();
            if (currentKey != null)
            {
                selectedKey = currentKey;
            }
            while (true)
            {
                if(currentKey == null)
                {
                    return;
                }
                currentKey = Navigation(currentKey);
                if (currentKey != null)
                    selectedKey = currentKey;
            }
        }

        public void GetAllValues()
        {
            if (selectedKey == null) 
            { 
                return; 
            }
                
           foreach(var name in selectedKey.GetValueNames())
            {
                if (name == null)
                {
                    continue;
                }
                var value = selectedKey.GetValue(name);
                Console.WriteLine($"{name}: {value}");
            }
        }

        public void CreateOrEditValue(string valueName, string valueValue)
        {
            selectedKey.SetValue(valueName, valueValue);
        }

        public void CreateSubKey(string name)
        {
            selectedKey.CreateSubKey(name);
        }

        public void DeleteSubKey(string name)
        {
            selectedKey.DeleteSubKeyTree(name);
        }
    }
}