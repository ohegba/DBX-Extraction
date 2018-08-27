using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace dbXtract
{
    class MessageInfo
    {
        public MessageNode mainVein;

       public  Int32 index;
        Int32 flags;

        static String dateToGoodDTS(DateTime d)
        {
            return d.Year + "-" + d.Month +"-"+ d.Day;
        }

      public  Int64 sendTime;

        Int32 nLines;
        Int32 msgPointer;

       public String subjectLine;

        Int32 folderTime;

        String messageID;

        String fullSubjectLine;

        String senderMailName;

        String repliedTo;

        String messageThreadNum;

        String server;

        String senderName;

        String senderMailAddress;

        Byte Unknown;

        Int32 PRIORITY;

        Int32 msgLength;

        Int64 recvTime;

        public DateTime dtf;

        String recvName;
        String recvAddress;

        Byte Unknown2;

        Int32 sometimesUsed;

        Byte Unknown3;

        Int32 sometimesUsed2;
        Int32 sometimesUsed3;

        String OEAcctName;

        String RegKey;

        long objPlace;

        public void readIn(BinaryReader b)
        {
            objPlace = b.BaseStream.Position;
            IndexedInfo idex = new IndexedInfo(b);


          

            //Console.WriteLine("Prado you "+ idex.objectMarker + ":" + idex.lengthOfBody);

          //  foreach (Byte key in idex.recordStore.Keys)
          //  {
            //    Console.WriteLine(key + " KEY");
           // }


           // for (byte i = 0; i < 30; i++)
            //{
             //   Console.WriteLine(i + ": " + idex.getValueAsHexStr(i));
           //     Console.WriteLine(i + ": " + idex.getValueAsString(i));
           // }

            senderMailAddress = idex.getValueAsString(14);
            recvAddress = idex.getValueAsString(19);
            subjectLine = idex.getValueAsString(8);
            dtf = DateTime.FromFileTimeUtc(BitConverter.ToInt64(idex.getValueAsArray(2), 0));


          //  Console.WriteLine();

            try
            {
                if (idex.recordStore.ContainsKey(132))
                    msgPointer = BitConverter.ToInt32(idex.getValueAsArray(132).Reverse().ToArray(), 0);
                else
                {
                    msgPointer = BitConverter.ToInt32(idex.getValueAsArray(4), 0);
                //    Console.WriteLine("URGENT " + BitConverter.ToString(idex.getValueAsArray(4)) + "-" + BitConverter.ToInt32(idex.getValueAsArray(4),0).ToString());
                }
            }
            catch (Exception eeeee)
            { }

          
         //   Console.WriteLine("OBJPOINT " + objPlace);
         //   Console.WriteLine("FROM " + idex.getValueAsString(14));
         //   Console.WriteLine("TO " + idex.getValueAsString(19));
         //   Console.WriteLine("SUBJECT " + idex.getValueAsString(8));




                //  Console.WriteLine("POINTER " + msgPointer.ToString());

               // mainVein = new MessageNode();

            /*
               
            */


               // Console.WriteLine(mainVein.runningText);




            



           /* try
            //{
                
                    
                    //      BitConverter.ToString(idex.getValueAsArray(132)));
                byte[] brah = { 0, 1, 0x5c, 0x44};
                brah = brah.Reverse().ToArray();
                Console.WriteLine(BitConverter.ToInt32(brah,0).ToString());
           }
            catch (Exception eeee)
            {
            }
            idex.findKey(0);
            */

            Program.messages.Add(this);
            Program.mCount++;

        }


        public void readRest(BinaryReader b)
        {

           
/*
            if (msgPointer != 0)
            {

                mainVein = new MessageNode(true);

                Int64 oldPos = b.BaseStream.Position;

                b.BaseStream.Seek(msgPointer, SeekOrigin.Begin);

                mainVein.readIn(this, b);

                b.BaseStream.Seek(oldPos, SeekOrigin.Begin);
            }
 */
            mainVein = new MessageNode(true);

            mainVein.runningText = MessageNode.blazeTrail(msgPointer, b);

            Program.mWriteCount++;


            string illegal = "(" + dateToGoodDTS(dtf) + "," + Program.mWriteCount + ") " + subjectLine;
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            illegal = r.Replace(illegal, "");
           File.WriteAllText("mail/" + illegal + ".eml", mainVein.runningText);
           File.SetCreationTime("mail/" + illegal + ".eml", dtf);
           File.SetLastWriteTime("mail/" + illegal + ".eml", dtf);

        }




    }
}
