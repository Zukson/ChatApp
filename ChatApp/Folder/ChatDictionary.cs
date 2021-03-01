using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Folder
{
    public class ChatDictionary
    {
        private readonly Dictionary<string,string>_cons;


        public ChatDictionary()
        {
            _cons = new Dictionary<string, string>();
        }



        public void Add(string username,string connection)
        {
            _cons.Add(username, connection);
        }

        public void Remove(string username)
        {
            _cons.Remove(username);
        }


        public string  Get(string username)
        {
             if(_cons.TryGetValue(username,out string connectionId))
            {
                return null;
            }

            return connectionId;

        }

    }
}
