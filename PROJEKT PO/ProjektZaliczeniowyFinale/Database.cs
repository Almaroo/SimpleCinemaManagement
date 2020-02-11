using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace ProjektZaliczeniowyFinale
{
    [XmlInclude(typeof(Cinema))]
    [XmlInclude(typeof(Movie))]
    [XmlInclude(typeof(ScreeningRoom))]
    [XmlInclude(typeof(Showing))]
    [XmlInclude(typeof(Showing3D))]
    [XmlInclude(typeof(Admin))]
    [XmlInclude(typeof(User))]
    [XmlInclude(typeof(UsersDatabase))]
    [XmlInclude(typeof(CinemasDatabase))]
    [XmlInclude(typeof(MoviesDatabase))]
    
    public abstract class Database<T>
    {
        protected List<T> _content;

        public Database()
        {
            _content = new List<T>();
        }

        public abstract bool Create(object caller, params object[] parameters);
        public abstract List<T> Read(object caller, params object[] parameters);
        public abstract bool Update(object caller, params object[] parameters);
        public abstract bool Delete (object caller, params object[] parameters);
        public virtual bool Serialize()
        {
            bool success = true;
            string path = $@"{Directory.GetCurrentDirectory()}\{this.GetType().Name}.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database<T>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                xmlSerializer.Serialize(fs, this);
                /*try
                {
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.GetType()} {e.Message}");
                    success = false;
                }
                finally
                {
                    fs.Close();
                }*/
            }
            return success;
        }

        public virtual bool Deserialize()
        {
            if (File.Exists($@"{Directory.GetCurrentDirectory()}\{this.GetType().Name}.xml"))
            {
                bool success = true;
                string path = $@"{Directory.GetCurrentDirectory()}\{this.GetType().Name}.xml";
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Database<T>));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    try
                    {
                        Database<T> deserializedDatabase = (Database<T>)xmlSerializer.Deserialize(fs);
                        _content = deserializedDatabase._content;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.GetType()} {e.Message}");
                        success = false;
                    }
                    finally
                    {
                        fs.Close();
                    }
                }

                return success;
            }
            else
                return false;
        }
    }
}
