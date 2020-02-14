using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjektZaliczeniowyFinale
{
    [Serializable]
    public sealed class UsersDatabase : Database<User>
    {
        private Dictionary<User, byte[]> _usersPasswordsHash;

        private static UsersDatabase _instance;

        public List<User> Users { get => _content; set => _content = value; }

        [XmlIgnore]
        public Dictionary<User, byte[]> UsersPasswordsHash { get => _usersPasswordsHash; set => _usersPasswordsHash = value; }

        public UsersDatabase() : base()
        {
            _usersPasswordsHash = new Dictionary<User, byte[]>();
        }

        public void Initialize()
        {
            Admin admin = new Admin("Marek", "Barszcz");
            _usersPasswordsHash.Add(admin, Authentication.GetPasswordHash("1234"));
            _content.Add(admin);
            this.Serialize();
        }

        private User GetUserById(string userId)
        {
            return _content.Find(x => x.UserId == userId);
        }

        public static UsersDatabase GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UsersDatabase();
                }
                return _instance;
            }
        }

        public static User LogIn(string userId, string password)
        {
            UsersDatabase uDB = UsersDatabase.GetInstance;
            if (!uDB.Deserialize())
                uDB.Initialize();
            User u = null;

            foreach(User user in uDB._usersPasswordsHash.Keys)
            {
                if (user.UserId == userId)
                {
                    u = user;
                    break;
                }
            }

            if(u != null)
            {
                if(Authentication.CompareHashes(uDB._usersPasswordsHash[u], Authentication.GetPasswordHash(password)))
                {
                    MoviesDatabase.GetInstance.Deserialize();
                    CinemasDatabase.GetInstance.Deserialize();


                    Console.WriteLine($"Zalogowano pomyślnie: {u.UserId} {u.Name} {u.Surname}");
                    return uDB._content.Find(x => x.UserId == u.UserId);//u
                }
                
                Console.WriteLine("Błędne hasło");
            }

            Console.WriteLine("Błędny login lub hasło");
            return null;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(this.GetType().Name);
            foreach (User user in _content)
            {
                stringBuilder.AppendLine(user.ToString());
            }

            return stringBuilder.ToString();
        }

        public override bool Create(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Creates new user in database
             *  
             *  Parameters:
             *      caller: object that calls method
             *      parameters: array of exactly 4 objects where: [name, surrname, password, isAdmin]
             *      
             *  Returns;
             *      true: if new user was created
             *      false: if not allowed to add
            */
            if (caller is Admin && parameters.Length == 4)
            {
                User tmp = null;
                if ((bool)parameters[3])
                {
                    tmp = new Admin((string)parameters[0], (string)parameters[1]);
                }
                else
                {
                    tmp = new User((string)parameters[0], (string)parameters[1]);
                }
                Users.Add(tmp);
                _usersPasswordsHash.Add(tmp, Authentication.GetPasswordHash((string)parameters[2]));
                Console.WriteLine("Dodano użytkownika");
                return true;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }   
        }

        public override List<User> Read(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Returns list of users that match given userIds
             *      
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of n objects where: [userId0, userId1, ... , userIdn]
             *      
             *  Returns:
             *      List<User>: if user is an admin
             *      null: if user is not an admin
             *   
             */
            if (caller is Admin)
            {
                int count = 0;
                List<User> retVal = new List<User>();
                foreach(string s in (string[])parameters[0])
                {
                    foreach(User user in _content)
                    {
                        if(s != "all")
                        {
                            if (user.UserId == s)
                            {
                                retVal.Add(user);
                                count++;
                            }
                        }
                        else
                        {
                            retVal.Add(user);
                            count++;
                        }
                    }
                }
                Console.WriteLine($"Zwrócono listę {count} użytkowników");
                return retVal;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return null;
            }
        }

        public override bool Update(object caller, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Deletes user with given userIds
             *  
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of objects where: [userId0, userId1, ... , userIdn]
             *      
             *  Returns:
             *      true: if successfully deleted at least one user
             *      false: if not allowed to delete or no user found
             */
            if (caller is Admin)
            {
                int count = 0;
                foreach(User user in Read(caller, parameters))
                {
                    if((User)caller != user)
                    {
                        _content.Remove(user);
                        _usersPasswordsHash.Remove(user);
                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Nie możesz usunąć siebie!");
                    }
                }

                if (count > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

        public override bool Serialize()
        {
            bool success = true;

            string path = $@"{Directory.GetCurrentDirectory()}\passwords.bin";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                binaryFormatter.Serialize(fs, _usersPasswordsHash);
                try
                {
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.GetType()} {e.Message}");
                    success = false;
                }
                finally
                {
                    fs.Close();
                }
            }

            return base.Serialize() && success;
        }

        public override bool Deserialize()
        {
            bool success = true;
            string path = $@"{Directory.GetCurrentDirectory()}\passwords.bin";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                try
                {
                    _usersPasswordsHash = (Dictionary<User,byte[]>)binaryFormatter.Deserialize(fs);
                    User.UserCount = _usersPasswordsHash.Count;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.GetType()} {e.Message}");
                    success = false;
                }
                finally
                {
                    fs.Close();
                }
            }

                return base.Deserialize() && success;
        }
    }
}
