using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Lab07
{
    public struct PatientTable
    {
        //Size, number of occupied cells, cells[], and load percent based on loadsize/size
        int size;
        int loadsize;
        double loadfactor;
        public Cell[] cells;
        public struct Cell
        {
            public Patient key;
            public PatientValue value;
            public bool tombstone;
        }
        public PatientTable(int size)
        {
            this.size = size;
            loadsize = 0;
            loadfactor = 0;
            cells = new Cell[size];
        }
        public int FindPat(Patient key)
        {
            int hashkey = GetHash(key), index, i = 0;
            while (true)
            {
                index = GetIndex(hashkey + (int)(Math.Pow(i, 2)));
                if (cells[index].key.firstName == key.firstName &&
                    cells[index].key.lastName == key.lastName)
                {
                    WriteLine($"Patient found:\n#{cells[index].value.patientsID}," +
                                $" {cells[index].key.firstName} {cells[index].key.lastName}." +
                                $" Doctor: {cells[index].value.familyDoctor}. Adress: {cells[index].value.adress}");
                    break;
                }
                else if (cells[index].key.firstName == null && cells[index].tombstone == false)
                {
                    WriteLine("Patient not found.");
                    index = -1; break;  
                }               //-1 = doesn't exist
                i++;
            }

            return index;
        }
        public void InsertPat(Patient key, PatientValue value, DoctorTable doctorList, ref int ID)
        {
            if (FindPat(key) == -1)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                loadfactor = (loadsize * 1.0) / (size * 1.0);
                if (loadfactor >= 0.5)
                    Rehash(doctorList, ref ID);
                int hashkey = GetHash(key), index, i = 0;
                while (true)
                {
                    index = GetIndex(hashkey + (int)(Math.Pow(i, 2))); //Quadratic probing
                    if (cells[index].key.firstName == null)
                    {
                        cells[index].key = key; cells[index].value = value; cells[index].tombstone = false;
                        int docID = doctorList.FindDoc(cells[index].value.familyDoctor);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        doctorList.cells[docID].doctor.patients.Add(key);
                        loadsize++;
                        ID++;
                        WriteLine("Patient inserted.");
                        break;
                    }
                    i++;
                }
            }
            else
                WriteLine("Patient already exists in table.");

        }
        public void Remove(Patient key, DoctorTable doctorList)
        {
            int i = FindPat(key);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();
            if (i != -1)
            {
                int docID = doctorList.FindDoc(cells[i].value.familyDoctor);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                doctorList.cells[docID].doctor.patients.Remove(cells[i].key);
                cells[i].key = default;
                cells[i].value = default;
                cells[i].tombstone = true;
                loadsize--;
                WriteLine("Patient removed.");
            }
        } 
        public void Clear(ref DoctorTable doctorList)
        {
            if (loadsize == 0)
            { WriteLine("Table is already empty."); return; }
            else
            {
                WriteLine("Emptying...");
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].key.firstName != null)
                    {
                        Remove(cells[i].key, doctorList);
                    }
                    
                }
                doctorList.ClearDoc();
                loadsize = 0;
                WriteLine("Table is now empty.");
            }
        }
        private void Rehash(DoctorTable doctorList, ref int ID)
        {
            //Buffer cells[], increase the size, Insert() every object again
            WriteLine("Load factor > 0.5, Rehashing...");
            size = NewPrime(size * 2);
            loadsize = 0;

            Cell[] buffcells = cells;
            cells = new Cell[size * 2];
            foreach (DoctorTable.Cell cell in doctorList.cells)
                if (cell.doctor.familyDoctor != null)
                  cell.doctor.patients.Clear();
            for (int i = 0; i < buffcells.Length; i++)
                if (buffcells[i].key.firstName != null)
                { Write("/"); InsertPat(buffcells[i].key, buffcells[i].value, doctorList, ref ID); }
            WriteLine("Rehashed succesfully.");
        }
        private int GetHash(Patient key)
        {
            int hashprime = 101;
            int hash = 37;
            hash = (hashprime * hash) + StringToInt(key.firstName);
            hash = (hashprime * hash) + StringToInt(key.lastName);
            return hash;
        }
        private int StringToInt(string str)
        {
            int res = 0;
            for (int i = 0; i< str.Length; i++)
                res += str[i];
            return res;
        }
        private int GetIndex(int hash)
        { return (hash % (size - 1)); }
        public void Print()
        {
            if (loadsize == 0)
                WriteLine("Table is empty.");
            else
            {
                List<string> sorted = new List<string>();
                for (int i = 0; i < cells.Length; i++)
                    if (cells[i].key.firstName != null && cells[i].key.firstName != "")
                        sorted.Add($"#{cells[i].value.patientsID}, {cells[i].key.firstName} {cells[i].key.lastName}");
                sorted.Sort();
                foreach (string s in sorted)
                    WriteLine(s);
                WriteLine();
            }
        }
        private int NewPrime(int old)
        {
            for (int i = old; i >= 2; i--)
                if (IsPrime(i))
                    return i;
            return -1;
        }
        private bool IsPrime(int value)
        {
            if (value <= 1)
                return false;
            else if (value % 2 == 0)
                return value == 2;

            int count = (int)(Math.Sqrt(value)+0.5);

            for (int i = 3; i <= count; i += 2)
                if (value % i == 0)
                    return false;

            return true;
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
    public struct Patient
    {
        public string firstName;
        public string lastName;
        public Patient(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
    public struct PatientValue
    {
        public string patientsID;
        public string familyDoctor;
        public string adress;
        public PatientValue(string patientsID, string familyDoctor, string adress)
        {
            this.patientsID = patientsID;
            this.familyDoctor = familyDoctor;
            this.adress = adress;
        }
    }
}

