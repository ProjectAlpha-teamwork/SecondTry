using BetSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users;

namespace Users
{
    public static class Login
    {

        public static User SignIn(string userName, string password)
        {
            var user = FindUser(userName).Split(' ');


            if (user[0] == "Not found")
            {
                throw new ArgumentException("Wrong username.");
            }

            if (user[7] != password)
            {
                throw new ArgumentException("Wrong password");
            }

            Gender gender;

            if (user[5].ToLower() == "other")
            {
                gender = Gender.Other;
            }
            else if (user[5].ToLower() == "female")
            {
                gender = Gender.Female;
            }
            else
            {
                gender = Gender.Male;
            }

            var username = user[0];
            var firstName = user[1];
            var lastName = user[2];
            var ssn = user[3];
            var balance = decimal.Parse(user[4]);
            var pass = user[6];
            var backUp = user[7];
            var address = user[8];
            var birth = user[9];




            var newUser = new User(username, firstName, lastName, ssn, balance, gender, pass, backUp, address, DateTime.Parse(birth));

            return newUser;


        }

        private static string FindUser(string username)
        {
            var reader = new StreamReader("Users.txt");

            var line = reader.ReadLine();


            while (line != null)
            {
                if (line.Contains(username))
                {
                    return line;
                }
            }

            return "Not found";
        }



    }
}