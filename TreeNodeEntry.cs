using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Windows.Forms;

namespace dbXtract
{
    class TreeNodeEntry
    {
        public Int32 value;
        public Int32 childNode;
        public Int32 storedValuesChild;

        public MessageInfo message;

        public void readIn(BinaryReader b)
        {
        //    Console.WriteLine("TREE NODE TREE NODE" + b.BaseStream.Position);

            value = b.ReadInt32();
            childNode = b.ReadInt32();
            storedValuesChild = b.ReadInt32();

            Program.treeEntryCount++;

           // if (value > 0)
            {
                Int64 oldPos = b.BaseStream.Position;
                b.BaseStream.Seek(value, SeekOrigin.Begin);
                message = new MessageInfo();
                message.readIn(b);
                b.BaseStream.Seek(oldPos, SeekOrigin.Begin);

                    if (childNode != 0)
                    {
                        b.BaseStream.Seek(childNode, SeekOrigin.Begin);
                        TreeNode treefree = new TreeNode();
                        treefree.readIn(b);
                        b.BaseStream.Seek(oldPos, SeekOrigin.Begin);
                    }
               

            }

        }

    }
}
