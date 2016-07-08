using BetSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    public class User : IUserable
    {
        private string userName;
        private string firstName;
        private string lastName;
        private string ssn;
        private decimal balance;
        private Gender gender;
        private string password;
        private string backUpCode;
        private string address;
        private DateTime birthDay;

        public User(string userName, string firstName, string lastName,
            string ssn, decimal balance, Gender gender, string passWord,
            string backUpCode, string address, DateTime birthDay)
        {
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Ssn = ssn;
            this.Balance = 0m;
            this.Gender = gender;
            this.PassWord = passWord;
            this.BackUpCode = backUpCode;
            this.Address = address;
            this.BirthDay = birthDay.Date;
            this.Balance = balance;
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.UserNamePattern))
                {
                    throw new ArgumentException("The username is not in the correct format.");
                }

                this.userName = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.NamePattern))
                {
                    throw new ArgumentException("The name is not in the correct format.");
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.NamePattern))
                {
                    throw new ArgumentException("The name is not in the correct format.");
                }

                this.lastName = value;

            }
        }


        public string Ssn
        {
            get
            {
                return this.ssn;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.SsnPattern))
                {
                    throw new ArgumentException("The SSN is not in the correct format.");
                }

                this.ssn = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this.balance;
            }

            private set
            {
                if (value < 0 && value > Validation.MaxDeposit)
                {
                    throw new ArgumentException("This ammount can not be set.");
                }

                this.balance = value;
            }
        }

        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            private set
            {
                this.gender = value;
            }

        }


        public string PassWord
        {
            get
            {
                return this.password;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.PassPattern))
                {
                    throw new ArgumentException("The password is not in the correct format.");
                }

                this.password = value;
            }
        }

        public string BackUpCode
        {
            get
            {
                return this.backUpCode;
            }

            private set
            {
                if (!Validation.IsValid(value, Validation.PassPattern))
                {
                    throw new ArgumentException("The code is not in tha correct format.");
                }

                this.backUpCode = value;
            }
        }


        public string Address
        {
            get
            {
                return this.address;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Please insert valid addres.");
                }
                this.address = value;
            }
        }


        public DateTime BirthDay
        {
            get
            {
                return this.birthDay.Date;
            }
            set
            {
                if (((DateTime.Now - value).TotalDays) / Validation.DaysInYear < Validation.MinYearsForRegistration)
                {
                    throw new ArgumentException("You must be over 18 to bet.");
                }

                this.birthDay = value.Date;
            }
        }

        //public void Bet(decimal ammount)
        //{
        //    this.Balance -= ammount;

        //    //implement where does the money go after
        //}


        public void Withdraw(decimal ammount)
        {
            this.Balance -= ammount;
        }

        public void Deposit(decimal ammount)
        {
            this.Balance += ammount;
        }






    }
}