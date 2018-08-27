using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace dbXtract
{
    class DBXHeader
    {
        Int32 magic;
        Int32 messVfold;
        // 5 4's
        Int32[] unknownTwenty;
        Int32 fileInfo_Length;
        
        Int32 lastVar_Pointer;
        Int32 lastVar_Length;
        Int32 lastVar_Used;

        Int32 lastTree_Pointer;
        Int32 lastTree_Length;
        Int32 lastTree_Used;

        Int32 lastMsg_Pointer;
        Int32 lastMsg_Length;
        Int32 lastMsg_Used;

        Int32 root_delMessList;
        Int32 root_delTreeList;

        Int32 usedSpace;
        Int32 reusableSpace;

        Int32 lastTreeEntryIndex;

        Int32 _1000;
        
        //5
        Int32[] _5foldignore;

        Int32 firstMid;
        Int32 _2000;

        Int32 messConds;
        Int32 foldConds;

       public Int32 entriesTree39;
        Int32 entriesTree3a;
        Int32 entriesTree3b;

        public Int32 messInfoTree;
        Int32 vigilantInfoTree;

        Int32 activeSubfolders;

        Int32 _1000_b;
        Int32 _2000_b;

        Int32 usedSpaceForIndexedInfo;
        Int32 usedSpaceForConditions;

        Int32 usedSpaceForFolderListObjects;
        Int32 usedSpaceForTreeObjects;
        Int32 usedSpaceForMessageObjects;

        public DBXHeader()
        {
            unknownTwenty = new Int32[5];
            _5foldignore = new Int32[5];

        }

        public void readIn(BinaryReader b)
        {
            magic = b.ReadInt32();
            messVfold = b.ReadInt32();

            Console.WriteLine("MAGIC STRING (SHOULD BE: cf ad 12 fe)");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(magic)));

            Console.WriteLine();

            Console.WriteLine("FOLDER IFF (SHOULD BE: c5 fd 74 6f)");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(messVfold)));

            for (int i = 0; i < 5; i++)
                unknownTwenty[i] = b.ReadInt32();

            Console.WriteLine("\nUNKNOWN");

            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(unknownTwenty[0])));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(unknownTwenty[1])));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(unknownTwenty[2])));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(unknownTwenty[3])));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(unknownTwenty[4])));
         

            fileInfo_Length = b.ReadInt32();

            b.ReadInt32();

            Console.WriteLine("\nFile Info Length");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(fileInfo_Length)));

            lastVar_Pointer = b.ReadInt32();
            lastVar_Length = b.ReadInt32();
            lastVar_Used = b.ReadInt32();

            Console.WriteLine("\nLast Var Pointer");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastVar_Pointer)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastVar_Length)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastVar_Used)));

            lastTree_Pointer = b.ReadInt32();
            lastTree_Length = b.ReadInt32();
            lastTree_Used = b.ReadInt32();

            Console.WriteLine("\nLast Tree Pointer");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastTree_Pointer)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastTree_Length)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastTree_Used)));

            lastMsg_Pointer = b.ReadInt32();
            lastMsg_Length = b.ReadInt32();
            lastMsg_Used = b.ReadInt32();

            Console.WriteLine("\nLast Msg Pointer");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastMsg_Pointer)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastMsg_Length)));
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastMsg_Used)));

            
            root_delMessList = b.ReadInt32();
            Console.WriteLine("\nRoot Del Mess Tree");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(root_delMessList)));

     
            root_delTreeList = b.ReadInt32();
            Console.WriteLine("\nRoot Mess Tree");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(root_delTreeList)));


            // Useless void space.
            b.ReadInt32();


            usedSpace = b.ReadInt32();
            Console.WriteLine("\nUsed Space");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(usedSpace)));
           
            reusableSpace = b.ReadInt32();
            Console.WriteLine("\nReusable Space");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(reusableSpace)));
          
            lastTreeEntryIndex = b.ReadInt32();
            Console.WriteLine("\nLast Tree Entry Index");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(lastTreeEntryIndex)));

            b.ReadInt32();
         
            _1000 = b.ReadInt32();
            Console.WriteLine("\nWorth 1000");
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(_1000)));

            b.ReadInt32(); b.ReadInt32();
            b.ReadInt32(); b.ReadInt32();
            b.ReadInt32();

            firstMid = b.ReadInt32();
            _2000 = b.ReadInt32();

            messConds = b.ReadInt32(); 
            foldConds = b.ReadInt32();

            b.ReadBytes(56);
            

           entriesTree39 = b.ReadInt32();
           Console.WriteLine("\nMessages");
           Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(entriesTree39)));

           entriesTree3a = b.ReadInt32(); 
           entriesTree3b = b.ReadInt32();

           b.ReadBytes(20);

        


          messInfoTree = b.ReadInt32();

          vigilantInfoTree = b.ReadInt32();

          activeSubfolders = b.ReadInt32();

          b.ReadBytes(24);

         
          _1000_b = b.ReadInt32();
          _2000_b = b.ReadInt32();

          b.ReadBytes(364);
          usedSpaceForIndexedInfo = b.ReadInt32();
          usedSpaceForConditions = b.ReadInt32();

          b.ReadInt32();
          usedSpaceForFolderListObjects = b.ReadInt32();
          usedSpaceForTreeObjects = b.ReadInt32();
          usedSpaceForMessageObjects = b.ReadInt32();

            Console.WriteLine("MESSAGES TREE TO BE FOUND AT " + messInfoTree);
            Console.WriteLine(BitConverter.ToString(BitConverter.GetBytes(messInfoTree)));

          

        }



    }
}
