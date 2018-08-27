using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.ComponentModel;

namespace dbXtract
{
    class Program
    {
        public static int mCount;
        public static int treeEntryCount;
        public static int treeNodeCount;
        public static int mWriteCount;
        
        public static List<MessageInfo> messages;
        public static List<MessageInfo> badMessages;


        static void Main(string[] args)
        {

            badMessages = new List<MessageInfo>();

            using (BinaryReader b = new BinaryReader(File.Open("InboxX2.dbx", FileMode.Open)))
            {

                DBXHeader head = new DBXHeader();
                head.readIn(b);

                messages = new List<MessageInfo>(head.entriesTree39);

                b.BaseStream.Seek(head.messInfoTree, SeekOrigin.Begin);

                TreeNode root = new TreeNode();

                root.readIn(b);

            //    Console.WriteLine(root.childe.entries[0].message.index);

              /*  int pos = 0;
   
                int length = (int)b.BaseStream.Length;
                while (pos < 4)
                {
                    int v = b.ReadInt32();

                    byte[] magic = BitConverter.GetBytes(v);

                    String s = BitConverter.ToString(magic);

                    Console.WriteLine(v);
                    
                    pos += sizeof(int);
                }
               */

                int accu = 0;

                foreach (MessageInfo msg in messages)
                {

                    if (accu > 100)
                    {
                        GC.Collect();
                        accu = 0;
                        Console.WriteLine("Called the Collectors.");
                    }

                    accu++;

               // MessageInfo msg = messages[0];
                    msg.readRest(b);
                    Console.WriteLine("Wrote " + mWriteCount + " of " + mCount);
                    msg.mainVein = null;
                    //GC.Collect();
              //  Console.Clear();


               }


                foreach (MessageInfo badMsg in badMessages)
                {
                    Console.WriteLine("BadMsg" + " " + badMsg.subjectLine);
                }


            }
            Console.WriteLine("MESSAGINFOS");
            Console.WriteLine(mCount);
            Console.WriteLine("ENTRIES");
            Console.WriteLine(treeEntryCount);
            Console.WriteLine("NODE");
            Console.WriteLine(treeNodeCount);

            Console.ReadKey();
            
        }
    }
}
