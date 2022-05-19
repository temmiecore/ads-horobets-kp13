using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Lab07
{
    public struct DoctorTable
    {
        int size;
        int loadsize;
        double loadfactor;
        public Cell[] cells;
        public struct Cell
        {
            public Doctor doctor;
            public bool tombstone;
        }

        public DoctorTable(int size)
        {
            this.size = size;
            loadsize = 0;
            loadfactor = 0;
            cells = new Cell[size];
        }
        public int FindDoc(string key)
        {
            int hashkey = GetHash(key);
            int index;
            int i = 0;
            while (true)
            {
                index = GetIndex(hashkey + i);
                if (cells[index].doctor.familyDoctor == key)
                {
                    WriteLine($"Doctor found: {cells[index].doctor.familyDoctor}");
                    break;
                }
                else if (cells[index].doctor.familyDoctor == null && cells[index].tombstone == false)
                {
                    WriteLine("Doctor not found.");
                    index = -1; break;
                }           //-1 = doesn't exist
                i++;
            }
            return index;
        }
        public void InsertDoc(Doctor doc)
        {
            if (FindDoc(doc.familyDoctor) == -1)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                loadfactor = (loadsize * 1.0) / (size * 1.0);
                if (loadfactor >= 0.6)
                    Rehash();
                int hashkey = GetHash(doc.familyDoctor), index, i = 0;
                while (true)
                {
                    index = GetIndex(hashkey + i); //Linear probing
                    if (cells[index].doctor.familyDoctor == null)
                    {
                        cells[index].doctor.familyDoctor = doc.familyDoctor; cells[index].doctor.patients = doc.patients;
                        cells[index].tombstone = false;
                        loadsize++;
                        WriteLine("Doctor inserted.");
                        break;
                    }
                    i++;

                }
            }
            else
                WriteLine("This doctor already exists in table.");
        }
        public void ClearDoc()
        {
            if (loadsize == 0)
            { WriteLine("Table is already empty."); return; }
            else
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    cells[i].doctor = default;
                    cells[i].tombstone = false;
                }
                loadsize = 0;
                WriteLine("Table cleared.");
            }
        }
        private void Rehash()
        {
            size = NewPrime(size * 2);
            loadsize = 0;
            WriteLine("Load factor > 0.6. Rehashing...");
            Cell[] buffcells = cells;
            cells = new Cell[size];
            for (int i = 0; i < buffcells.Length; i++)
                if (buffcells[i].doctor.familyDoctor != null)
                    InsertDoc(buffcells[i].doctor);
            WriteLine("Table rehashed.");
        }
        private int GetHash(string key)
        {
            int hashprime = 101;
            int hash = 37;
            hash = (hashprime * hash) + StringToInt(key);
            return hash;
        }
        private int StringToInt(string str)
        {
            int res = 0;
            for (int i = 0; i < str.Length; i++)
                res += str[i];
            return res;
        }
        private int GetIndex(int hash)
        { return (hash % (size - 1)); }
        public List<Patient> FindFamilyDoctorPatients(string key)
        {
            int i = FindDoc(key);
            if (i != -1)
            {
                WriteLine($"List of patients of {cells[i].doctor.familyDoctor}:");
                if (cells[i].doctor.patients.Count == 0)
                    WriteLine("No patients.");
                else
                {
                    foreach (Patient p in cells[i].doctor.patients)
                        WriteLine($"{p.firstName} {p.lastName}");
                }
                return cells[i].doctor.patients;
            }
            return null;
        }
        public void Print()
        {
            if (loadsize == 0)
                WriteLine("Table is empty.");
            else
            {
                for (int i = 0; i < cells.Length; i++)
                    if (cells[i].doctor.familyDoctor != "" && cells[i].doctor.familyDoctor != null)
                        Write(cells[i].doctor.familyDoctor + ";\n");
                WriteLine();
            }
        }
        public string AvailableDoctor()
        {
            for (int i = 0; i < cells.Length; i++)
                if (cells[i].doctor.patients != null && cells[i].doctor.patients.Count < 5)
                    return cells[i].doctor.familyDoctor;
            return null;
        }
        private int NewPrime(int old)
        {
            for (int i = old; i >= 2; --i)
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

            int n = (int)(Math.Sqrt(value) + 0.5);

            for (int i = 3; i <= n; i += 2)
                if (value % i == 0)
                    return false;

            return true;
        }
    }
    public struct Doctor
    {
        public string familyDoctor;
        public List<Patient> patients;
        public Doctor(string familyDoctor)
        {
            this.familyDoctor = familyDoctor;
            patients = new List<Patient>();
        }
    }

}
