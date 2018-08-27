using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Linq;

namespace dbXtract
{
   


    class IndexRecord
    {
        public Byte[] indexBytes;
       public Boolean useValue;
        public Byte[] value;
        public Int64 szVal; 
        public IndexRecord(BinaryReader b)
        {
            indexBytes = new Byte[4];
            indexBytes = b.ReadBytes(4);
            if (indexBytes[0] >= 80)
            {
                useValue = true;
            }

            Byte[] subBytes = { indexBytes[1], indexBytes[2], indexBytes[3], 0 };

            value = new Byte[] { 69, 69, 69, 69 };
            value = subBytes.Reverse().ToArray();
            szVal = BitConverter.ToInt32((subBytes.ToArray()),0);
           // Console.WriteLine(szVal + "!");
        }

        public void searchKey(byte index)
        {

        }
    

        public void grabValueFromData2(IndexedInfo par, Int64 end)
        {
            if (useValue)
           {
                Byte[] subBytes = { indexBytes[1], indexBytes[2], indexBytes[3] };
              // value = subBytes.ToArray();
               return;
            }


            List<Byte> bList = new List<byte>();

            for (int j = par.bodyProg; par.endpointsIndex < par.endpoints.Count && j < par.endpoints[par.endpointsIndex]; j++)
            {
                try
                {
                    bList.Add(par.body[j]);
                }
                catch (ArgumentOutOfRangeException ae)
                {
                 //  Console.WriteLine(j + " !" + par.endpoints[par.endpointsIndex]);
                }
                
                par.bodyProg++;
            }

            par.endpointsIndex++;

            value = bList.ToArray();


        }



        public void grabValueFromData(IndexedInfo par, Int64 end)
        {
            if (useValue)
            {
                Byte[] subBytes = { indexBytes[1], indexBytes[2], indexBytes[3] };
                value = subBytes.ToArray();
            }

            else
            {

                if (end == -999)
                    end = par.body.Length - par.bodyProg;
                try
                {
                    value = new Byte[10];
                }
                catch(Exception eee)
                {}
                int i; int j=0;
                for ( i = par.bodyProg; i < par.body.Length; j++, i++)
                {
                   // Console.WriteLine( i+" Bounds of arr " + value.Length);
                    try
                    {
                        value[j] = par.body[i];
                    }
                    catch (Exception eeee)
                    {
                        continue;
                    }

                    }

                par.bodyProg += i;
            }

        }



            

    }
}
