using System;
using System.Collections;
using System.Linq;
using static System.Console;

namespace Lab07
{
    class Program
    {
        static int ID = 0; //ID + 41800
        static PatientTable pats = new PatientTable(23);
        static DoctorTable docs = new DoctorTable(5);
        static void Main()
        {
            Write("Use control example? y/n: "); string control = ReadLine();
            switch (control)
            {
                case "y": { ControlMethod(); break; }
                case "n": break;
                default: { WriteLine("Incorrect input!"); break; }
            }
            while (true)
            {
                WriteLine("\n---------------------\nChoose option (type 1-8):\n1. Add Patient;" +
                    "\n2. Remove Patient;\n3. Add Doctor;\n4. Find Patient;\n5. Find Doctor (with patients);" +
                    "\n6. Print Table;\n7. Clear Table;\n8. Control test;\n9. Exit.");
                string choice = ReadLine();
                string info = "";
                switch (choice)
                {
                    case "1":
                        {
                            WriteLine("------------\nInsertion\n" +
                                "Insert Patient's name, surname, doctor, adress, \ne.g. Marylin, Sue, Nadim Kosarov, Leynois st.: ");
                            info = ReadLine();
                            InsertPatient(info);
                            break;
                        }
                    case "2":
                        {
                            WriteLine("------------\nDeletion\n" +
                                "Insert Patient's name and surname (divide with comma)\ne.g. Marylin, Sue: ");
                            info = ReadLine();
                            RemovePatient(info);
                            break;
                        }
                    case "3":
                        {
                            WriteLine("------------\nInsertion\n" +
                                "Insert Doctor's name and surname\ne.g. Nadim Kosarov: ");
                            info = ReadLine();
                            InsertDoctor(info);
                            break;
                        }
                    case "4":
                        {
                            WriteLine("------------\nSearch\n" +
                                "Insert Patient's name and surname (divide with comma)\ne.g. Marylin, Sue: ");
                            info = ReadLine();
                            FindPatient(info);
                            break;
                        }
                    case "5":
                        {
                            WriteLine("------------\nSearch\n" +
                                "Insert Doctor's name and surname\ne.g. Nadim Kosarov:");
                            info = ReadLine();
                            FindFamDocPats(info);
                            break;
                        }
                    case "6":
                        {
                            WriteLine("------------\nPrint\n" +
                              "Print patients or doctors? (p/d):");
                            info = ReadLine();
                            switch (info)
                            {
                                case "p": WriteLine("Patients:"); pats.Print(); break;
                                case "d": WriteLine("Doctors:"); docs.Print(); break;
                                default: WriteLine("Incorrect input."); break;
                            }
                            break;
                        }
                    case "7":
                        {
                            WriteLine("------------\nCleaning\n");
                                pats.Clear(ref docs);
                            break;
                        }
                    case "8": ControlMethod(); break;
                    case "9":
                        return;
                    default:
                        { WriteLine("Incorrect input."); break; }
                }
            }
        }


        static void ControlMethod()
        {
            string[] firstname = new string[13] { "Benny", "Richard", "Hanna", "Alex", "John", "Peter", "David", "Clara", "Olha", "Lex", "Gabe", "Masha", "Catya" };
            string[] lastname = new string[13] { "Lee", "Hycks", "Anderson", "Klein", "Payne", "Morrison", "Price", "Stanton", "Holland", "Walker", "Campbell", "Ray", "Ray" };
            string[] adress = new string[13] {"Fox Willows","St Margaret's South", "St Margaret's South", "St Margaret's South", "St Margaret's South",
                                               "Ventnor North", "Ventnor North", "Ventnor North", "Wuthering Heights",
                                               "Wuthering Heights", "Fox Willows", "Fox Willows","Fox Willows" };
            string[] doctor = new string[3] { "Nadim Kosarov", "Johanna Anderson", "Ethan Denmaro" };

            WriteLine("\t----CONTROL----");

            WriteLine("---------------\nInserting doctors...");
            for (int i = 0; i < 3; i++)
                InsertDoctor(doctor[i]);

            WriteLine("---------------\nInserting patients...");
            Random rnd = new Random();
            for (int i = 0; i < 13; i++)
                InsertPatient(firstname[i] + "," + lastname[i] + "," + doctor[rnd.Next(0, 3)] + "," + adress[i]);
            

            WriteLine("---------------\nRemoving Benny Lee (patient)...");
            RemovePatient(firstname[0] + "," + lastname[0]);

            WriteLine("---------------\nFinding  Alex Klein (patient)...");
            FindPatient(firstname[1] + "," + lastname[1]);

            WriteLine("---------------\nFinding non-existent patient (Benny Lee)...");
            FindPatient(firstname[0] + "," + lastname[0]);

            WriteLine("---------------\nFinding Dr. Nadim Kosarov (findFamilyDoctorPatients)...");
            FindFamDocPats(doctor[0]);

            WriteLine("---------------\nPrinting tables...\nPatients:");
            pats.Print(); WriteLine("Doctors:"); docs.Print();

            WriteLine("----CONTROL END----");
        }

        static void InsertPatient(string info)
        {
            string[] toinsert = info.Split(',');
            toinsert[0] = toinsert[0].Trim(); toinsert[1] = toinsert[1].Trim();
            toinsert[2] = toinsert[2].Trim(); toinsert[3] = toinsert[3].Trim();
            
            if (toinsert.Length == 4 && NamesValidation(toinsert[0]) && NamesValidation(toinsert[1]) )
            {
                Patient key = new Patient(toinsert[0], toinsert[1]);
                PatientValue value = new PatientValue((ID + 41800).ToString(), toinsert[2], toinsert[3]);

                int docID = docs.FindDoc(toinsert[2]);

                if (docID != -1 && docs.cells[docID].doctor.patients.Count < 5) //Exists and patients < 5
                {
                    pats.InsertPat(key, value,docs, ref ID);
                }
                else 
                {
                    WriteLine("Doctor does not exist or already has 5 patients. Looking for another one...");
                    string available = docs.AvailableDoctor();
                    if (available != null)
                    {
                        value.familyDoctor = available;
                        pats.InsertPat(key, value, docs, ref ID); 
                    }
                    else
                    {
                        Write("No available doctors found. Want to add new one? (patient will be added automatically) y/n: ");
                        string check = ReadLine();
                        switch (check)
                        {
                            case "y":
                                {
                                    WriteLine("Enter name and surname of doctor: ");
                                    info = ReadLine();
                                    InsertDoctor(info);
                                    value.familyDoctor = info;
                                    pats.InsertPat(key, value, docs, ref ID);
                                    break;
                                }
                            case "n":
                                break;
                            default:
                                WriteLine("Incorrect input."); break;
                        }
                    }
                }
            }
            else
                WriteLine("Name is not valid.");
        }
        static void RemovePatient(string info)
        {
            string[] toinsert = info.Split(',');
            if (toinsert.Length == 2)
            {
                toinsert[0] = toinsert[0].Trim(); toinsert[1] = toinsert[1].Trim();
                pats.Remove(new Patient(toinsert[0], toinsert[1]), docs);
            }
            else
                Write("Enter name correctly (divide with comma).");
        }
        static void InsertDoctor(string info)
        {
            info = info.Trim();
            if (NamesValidation(info))
                docs.InsertDoc(new Doctor(info));
            else
                WriteLine("Incorrect name. Use only letters.");
        }
        static void FindPatient(string info)
        {
            string[] toinsert = info.Split(',');
            if (toinsert.Length == 2)
            {
                toinsert[0] = toinsert[0].Trim(); toinsert[1] = toinsert[1].Trim();
                Patient key = new Patient(toinsert[0], toinsert[1]);
                pats.FindPat(key);
            }
            else
                Write("Enter name correctly (divide with comma).");
        }
        static void FindFamDocPats(string key)
        {
            docs.FindFamilyDoctorPatients(key);
        }
        static bool NamesValidation(string info)
        { return info.All(c => Char.IsLetter(c) || c == ' '); }
    }
}
