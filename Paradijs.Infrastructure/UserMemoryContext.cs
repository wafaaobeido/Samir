using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    class UserMemoryContext : IUser
    {
        private List<User> Users = new List<User>();
        public bool Checkaccount(User user)
        {
            foreach (User m in Users)
            {
                if (m.Email == user.Email && m.Password == user.Password)
                {
                    return true;
                }
            }
            throw new User.UserNietGevondenException();
        }

        public bool IsActivationCodeExists(User user)
        {
            foreach (User m in Users)
            {
                if (m.ActivationCode == user.ActivationCode)
                {
                    return true;
                }
            }
            throw new User.UserNietGevondenException();
        }

        public bool IsEmailExists(User user)
        {
            foreach (User m in Users)
            {
                if (m.Email == user.Email)
                {
                    return true;
                }
            }
            throw new User.UserNietGevondenException();
        }

        public bool IsValidation(User user)
        {
            foreach (User m in Users)
            {
                if (m.Email == user.Email && m.Password == user.Password)
                {
                    return user.IsEmailVerified = true; 
                }
                else
                {
                    return false;
                }
            }
            throw new User.UserNietGevondenException();
        }

        public User LogIn(User user)
        {
            return user;
        }

        public List<User> AllUsers()
        {
            return Users;
        }

        public User GeefUserrMetId(int id)
        {
            foreach (User m in Users)
            {
                if (m.Id == id)
                {
                    return m;
                }
            }
            throw new User.UserNietGevondenException();
        }
        public User AddUser(User user)
        {
            foreach (User m in Users)
            {
                // De User-klasse bepaalt wat twee Users "gelijk"
                // aan elkaar maakt. Het is corrector om hier een implementatie
                // van de Equals-methode voor te geven, maar daar zitten wat
                // meer details omheen; daarnaast geeft dit een voorbeeld van
                // een implementatie van IComparable.
                if (m.CompareTo(user) == 0)
                {
                    user = null;
                    return user;
                }
            }

            // Geen medewerkers gevonden die gelijk zijn aan deze: toevoegen.
            Users.Add(user);
            return user;
        }


        public void DeleteUser(int id)
        {
            int aantal = Users.Count();

             aantal = aantal - 1;
        }

        public void EditUser()
        {
            throw new NotImplementedException();
        }

    }
}
