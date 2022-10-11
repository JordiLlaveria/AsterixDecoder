using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class AsterixFile
    {
        List<CAT10> CAT10list = new List<CAT10>();
        List<CAT21> CAT21list = new List<CAT21>();
        public AsterixFile(string path)
        {
            byte[] messages = File.ReadAllBytes(path);

            //Vamos a intentar separar la información
            int i = 0;
            int lenmessage;
            while (i < messages.Length)
            {
                lenmessage = messages[i + 1] + messages[i + 2];
                byte[] arraymessage = new byte[lenmessage];
                for (int j = 0; j < lenmessage; j++)
                {
                    arraymessage[j] = messages[i];
                    i++;
                }
                //Array.Copy(messages, i, arraymessage, 0, lenmessage);
                //i = i + lenmessage - 1;
                if (arraymessage[0] == 10)
                {
                    CAT10 cat10 = new CAT10(arraymessage);
                    CAT10list.Add(cat10);
                }
                else
                {
                    CAT21 cat21 = new CAT21(arraymessage);
                    CAT21list.Add(cat21);
                }
            }
            Console.WriteLine("Hola");
        }

    }
}
