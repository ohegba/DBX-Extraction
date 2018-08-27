using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Windows.Forms;

namespace dbXtract
{
    class TreeNode
    {
        public Int32 objMarker;
        Int32 unused;
        Int32 pointerToChild;
        Int32 pointerToParent;

        public Byte nodeID;
        public Byte bodyNodeEntries;

        Int16 unused2;

        Int32 stored;

        // 6 of these.
        public  TreeNodeEntry[] entries;

        public TreeNode childe;
        public TreeNode parente;

        public void readIn(BinaryReader b)
        {
            Program.treeNodeCount++;
            Console.WriteLine(Program.mCount + " messages found over " + Program.treeNodeCount + " main trunk nodes.");
       //     Console.WriteLine("OBJECT OBJECT OBJECT " + objMarker);

            objMarker = b.ReadInt32();
            unused = b.ReadInt32();
            pointerToChild = b.ReadInt32();
            pointerToParent = b.ReadInt32();

            nodeID = b.ReadByte();
            bodyNodeEntries = b.ReadByte();

            unused2 = b.ReadInt16();
            stored = b.ReadInt32();

            entries = new TreeNodeEntry[bodyNodeEntries];

         
         //      Console.WriteLine(bodyNodeEntries+"!!!!!!");
                
             

            for (int i = 0; i < bodyNodeEntries; i++)
            {
                TreeNodeEntry newEntry = new TreeNodeEntry();
                newEntry.readIn(b);

                entries[i] = newEntry;
            }

            childe = new TreeNode();
            if (pointerToChild != 0)
            {
            Int64 oldPos = b.BaseStream.Position;

            b.BaseStream.Seek(pointerToChild, SeekOrigin.Begin);
            
            childe.readIn(b);

            b.BaseStream.Seek(oldPos, SeekOrigin.Begin);
            }

            

        }
    }
}
