using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace dbXtract
{
    class MessageNode
    {
        Int32 objectMarker;
        Int32 bodyLength;
        Int32 textLength;
        Int32 nextNode;

        MessageInfo parentMessage;

        bool iAmLegend;
        Int32 legendObject;

        int depth = 0;

        const int maxDepth = 99;


        public String text;
        public String runningText;
        public int runningLength;

        public MessageNode(bool legendary)
        {
            iAmLegend = legendary;

            if (iAmLegend)
                legendObject = objectMarker;
        }

        public static String blazeTrail(Int32 pointer, BinaryReader b)
        {
            StringBuilder sb = new StringBuilder();

            int obj = 0;
            int blen = 0;
            int tlen = 0;
            int next = 0;
            byte[] buf;

            Int32 pointerToGoTo = pointer;
            while (pointerToGoTo != 0)
            {

                b.BaseStream.Seek(pointerToGoTo, SeekOrigin.Begin);

                obj = b.ReadInt32();
                blen = b.ReadInt32();
                tlen = b.ReadInt32();
                next = b.ReadInt32();

                buf = b.ReadBytes(tlen);

                sb.Append(Encoding.ASCII.GetString(buf));

                pointerToGoTo = next;

            }

            return sb.ToString();
        }

        public void readIn(MessageInfo parent, BinaryReader b)
        {

      
           

            parentMessage = parent;

           // if (iAmLegend)
            //    Console.WriteLine(parentMessage.subjectLine);

            if (parentMessage == null)
            {
                Console.WriteLine("orphan");
                return;
            }

            depth++;

            objectMarker = b.ReadInt32();
            bodyLength = b.ReadInt32();
            textLength = b.ReadInt32();
            nextNode = b.ReadInt32();

            //Console.WriteLine();
           // Console.WriteLine("Depth: " + depth);
          //  Console.WriteLine("OBJMARC: " + objectMarker);
          //  Console.WriteLine("BODLEN: " + bodyLength);
          //  Console.WriteLine("TEXTLEN: " + textLength);
          //  Console.WriteLine("NEXTNODE: " + nextNode);

            if (textLength < 1)
                return;

            /*if (depth > 999)
            {
                Program.badMessages.Add(parent);
                Console.WriteLine("BADDIE " + parentMessage.subjectLine);
                return;
            }*/

            List<Byte> charList = new List<Byte>();

            for (int i = 0; i < textLength; i++)
            {
                Byte c = b.ReadByte();
                charList.Add(c);
            }

            text = Encoding.ASCII.GetString(charList.ToArray());

            runningText = text;
            runningLength = textLength;

            if (nextNode == 0)
            {
                return;
            }

            if (nextNode != 0)
            {
                Int64 oldPos = b.BaseStream.Position;
                b.BaseStream.Seek(nextNode, SeekOrigin.Begin);
                MessageNode nextNodeStruct = new MessageNode(false);
                
                nextNodeStruct.legendObject = legendObject;
                nextNodeStruct.depth = depth;
                nextNodeStruct.parentMessage = parentMessage;
                nextNodeStruct.readIn(parentMessage, b);
                
                runningText += nextNodeStruct.runningText;
                runningLength += nextNodeStruct.runningLength;
                b.BaseStream.Seek(oldPos, SeekOrigin.Begin);

               // if (iAmLegend)
               //     Console.WriteLine(runningText);

            }

            

          //  Console.WriteLine(objectMarker + " : " + runningLength);


        }

    }
}
