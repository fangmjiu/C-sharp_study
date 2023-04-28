using System;
using System.DirectoryServices;

namespace Create {
    public class CreateWinUser {
        static void Main(String[] args) {
            if (args.Length != 2) {
                Console.WriteLine("please input username and password");
                Console.WriteLine("Usage:add.exe <username> <password>");
            } else {
                String username = args[0];
                String password = args[1];
                try {
                    DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    DirectoryEntry NewUser = AD.Children.Add(username, "user");
                    NewUser.Invoke("SetPassword", new object[] {password});
                    NewUser.Invoke("Put", new object[] {"Description", ""});
                    NewUser.CommitChanges();
                    DirectoryEntry grp = AD.Children.Find("Administrators", "group");
                    if (grp != null) {grp.Invoke("Add", new object[] {NewUser.Path.ToString()});}
                    Console.WriteLine("Account Created Successfully");
                    Console.ReadLine();
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
