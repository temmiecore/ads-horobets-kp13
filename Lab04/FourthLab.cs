using System;

namespace FourthLab
{
    class DList
    {
        public class DLNode
        {
            public int data;
            public DLNode next;
            public DLNode prev;
            public DLNode(int data)
            {
                this.data = data;
                next = null;
                prev = null;
            }
        }
        public DLNode head;
        public DLNode tail;
        public int size;
        public DList()
        {
            head = null;
            tail = null;
            size = 0;
        }
        public void PrintList()
        {
            DLNode current = head;
            if (size > 0)
            {
                while (current != null)
                {
                    Console.Write(current.data + " ");
                    current = current.next;
                }
                Console.WriteLine();
            }
            else
                Console.WriteLine("List is empty.");
        }
        public void AddFirst(int data)
        {
            DLNode added = new DLNode(data);
            if (size == 0)
            {
                head = added;
                tail = head;
                size++;
            }
            else
            {
                added.next = head;
                head.prev = added;
                head = added;
                size++;
            }
        }
        public void AddLast(int data)
        {
            DLNode added = new DLNode(data);
            if (size == 0)
            {
                head = added;
                tail = head;
                size++;
            }
            else
            {
                added.prev = tail;
                tail.next = added;
                tail = added;
                size++;
            }
        }
        public void AddAtPosition(int data, int pos)
        {
            DLNode added = new DLNode(data);
            if (size == 0)
            {
                head = added;
                tail = head;
                size++;
            }
            else if (pos > size)
                AddLast(data);
            else
            {
                DLNode current = head;
                for (int i = 0; i < pos - 2; i++)
                    current = current.next;
                added.next = current.next;
                added.prev = current;
                current.next = added;
                added.next.prev = added;
                size++;
            }
        }
        public void DeleteFirst()
        {
            if (size == 0)
                Console.WriteLine("Nothing to delete!");
            else if (size == 1)
            {
                head = null;
                tail = null;
                size--;
            }
            else 
            {
                head = head.next;
                head.prev = null;
                size--;
            }
        }
        public void DeleteLast()
        {
            if (size == 0)
                Console.WriteLine("Nothing to delete!");
            else if(size == 1)
            {
                head = null;
                tail = null;
                size--;
            }
            else
            {
                DLNode newtail = tail.prev;
                newtail.next = null;
                tail = newtail;
                size--;
            }
        }
        public void DeleteAtPosition(int pos)
        {
            if (size == 0)
                Console.WriteLine("Nothing to delete!");
            else if(size == 1)
            {
                head = null;
                tail = null;
                size--;
            }
            else
            {
                DLNode deleted = head;
                for (int i = 0; i < pos - 2; i++)
                    deleted = deleted.next;
                deleted.next = deleted.next.next;
                deleted.next.prev = deleted;
                size--;
            }
        }
        public void AddBeforeLast(int data)
        {
            DLNode added = new DLNode(data);
            if (size == 0)
            {
                head = added;
                tail = head;
                size++;
            }
            else
            {
                added.next = tail.prev.next;
                added.prev = tail.prev;
                tail.prev.next = added;
                tail.prev = added;
                size++;
            }
        }
    }


    class Program
    {
        static void Main()
        {
            DList list = new DList();
            bool check = true;
            Console.WriteLine("Choose an operation with List (input 1-7):");
            while (check == true)
            {
                Console.Write("1)Add First;    2)Add Last;       3)Add At Pos;  4)Delete First;" +
                              "\n5)Delete Last;  6)Delete At Pos;  7)Print List;");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  8) Labwork Task.");
                Console.ForegroundColor = ConsoleColor.White;
                check = int.TryParse(Console.ReadLine(), out int operation);

                if (check == false || operation > 8 || operation < 0)
                { Console.WriteLine("Input correct operation number!"); check = true; continue; }
                else
                {
                    switch (operation)
                    {
                        case 1:
                            {
                                Console.Write("Adding First Element! Input data: ");
                                int data = int.Parse(Console.ReadLine());
                                list.AddFirst(data);
                                list.PrintList();
                                break;

                            }
                        case 2:
                            {
                                Console.Write("Adding Last Element! Input data: ");
                                int data = int.Parse(Console.ReadLine());
                                list.AddLast(data);
                                list.PrintList();
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Adding Element... Input data: ");
                                int data = int.Parse(Console.ReadLine());
                                Console.Write("\nAnd position: ");
                                int pos = int.Parse(Console.ReadLine());
                                list.AddAtPosition(data, pos);
                                list.PrintList();
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Deleting First Element!");
                                list.DeleteFirst();
                                list.PrintList();
                                break;
                            }
                        case 5:
                            {
                                Console.WriteLine("Deleting Last Element!");
                                list.DeleteLast();
                                list.PrintList();
                                break;
                            }
                        case 6:
                            {
                                Console.Write("Deleting Element... Input position: ");
                                int pos = int.Parse(Console.ReadLine());
                                list.DeleteAtPosition(pos);
                                list.PrintList();
                                break;
                            }
                        case 7:
                            {
                                Console.WriteLine("Printing List...");
                                list.PrintList();
                                break;
                            }
                        case 8:
                            {
                                Console.Write("Input data: ");
                                int data = int.Parse(Console.ReadLine());
                                if (list.tail.data < 0)
                                    list.AddLast(data);
                                else
                                    list.AddBeforeLast(data);
                                list.PrintList();
                                break;
                            }
                    }
                }
            }
        }
    }
}
