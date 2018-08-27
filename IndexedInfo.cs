using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace dbXtract
{
    class IndexedInfo
    {
        public Int32 objectMarker;
      public   Int32 lengthOfBody;
        Int16 lengthOfObj;
        Byte Entries;
        Byte Changes;
        public Int32 bodyProg;
        public Byte[] body;

      public  IndexRecord[] records;

      public Dictionary<Byte, Byte[]> recordStore;

      public List<Int64> endpoints;
      public int endpointsIndex;

      public void countRecords()
      {
          endpoints = new List<long>();
          foreach (IndexRecord i in records)
          {
              if (!i.useValue)
              {
                 
                  if (i.szVal > 0)
                      endpoints.Add(i.indexBytes[1]+0);
                  
              }
          }
          endpoints.Add(body.Length);
      }

      public String getValueAsString(byte index)
      {
          byte[] ret;
          recordStore.TryGetValue(index, out ret);
          try
          {
              return new String(Encoding.ASCII.GetChars(ret));
          }
          catch (Exception eee)
          {
              return "ERROR";
          }
          }

      public Int32 getValueAsInt32(byte index)
      {
          byte[] ret;
          
          recordStore.TryGetValue(index, out ret);

          if (ret == null)
              return -1;
          return BitConverter.ToInt32(ret,0);
      }

      public String getValueAsHexStr(byte index)
      {
          byte[] ret;

          recordStore.TryGetValue(index, out ret);

          if (ret == null)
              return "ERRORES";

          return BitConverter.ToString(ret);
      }

      public byte[] getValueAsArray(byte index)
      {
          byte[] ret;

          recordStore.TryGetValue(index, out ret);

          if (ret == null)
              return null;

          return ret;
      }

      public void printIndices()
      {
          int i = 0;
          foreach (Byte[] ib in recordStore.Values)
          {
              Console.WriteLine( ": " + new String(Encoding.ASCII.GetChars(ib)));
              i++;
          }
         
      }

      public void printRIndices()
      {
        
          foreach (IndexRecord r in records)
          {
              Console.WriteLine(r.indexBytes[0] + ": " + new String(Encoding.ASCII.GetChars(r.value)));
              
          }

      }


      public void findKey(Byte b)
      {
          Console.WriteLine("FNNF3834834u83");
          foreach (IndexRecord ir in records)
          {
                  if (ir.indexBytes[0] == 132)
                  Console.WriteLine("FIND"+BitConverter.ToString(ir.value));
          }

      }

       public IndexedInfo(BinaryReader b)
        {
            endpointsIndex = 0;
            objectMarker = b.ReadInt32();
            lengthOfBody = b.ReadInt32();
            lengthOfObj = b.ReadInt16();

            Entries = b.ReadByte();
            Changes = b.ReadByte();
            bodyProg = 0;

            records = new IndexRecord[Entries];

            for (int i = 0; i < Entries; i++)
            {
                IndexRecord record = new IndexRecord(b);
                records[i] = record;
            }

    
           

           int rest = lengthOfBody - (4 * Entries);

           body = b.ReadBytes(rest);

           countRecords();

           foreach (Int64 II in endpoints)
           {
              // Console.WriteLine("ENDPOINT:" + II);
           }

           for (int j = 0; j < Entries; j++)
           {
               records[j].grabValueFromData2(this, 999);

           }

            


           recordStore = new Dictionary<Byte, Byte[]>();
           foreach (IndexRecord stored in records)
           {
               byte index = stored.indexBytes[0];
             //  if (index >= 80) index-=80;
               recordStore.Add(index, stored.value);

           }

         //   {
          //      if (j < (Entries - 1))
        //            records[j].grabValueFromData(this, records[j + 1].szVal);
       //         else
             //       records[j].grabValueFromData(this, -999);
        //    }
           // */


        }

    }
}
