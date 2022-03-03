using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldHospital
{
    internal class Exam
    {
        public Person Head { get; set; }
        public Person Tail { get; set; }

        public Exam()
        {
            Head = null;
            Tail = null;
        }

        public void Push(Person person)
        {
            if (VoidExam())
            {
                Head = Tail = person;
            }
            else
            {
                Tail.Next = person;
                Tail = person;
            }
        }

        public void PrintPerson()
        {
            if (VoidExam())
            {
                return;
            }
            else
            {
                Person person = Head;
                do
                {
                    Console.WriteLine(person.ToString());
                    person = person.Next;

                } while (person != null);
            }

        }

        public Person LoadPerson()
        {
            Person temp;

            if (VoidExam())
                temp = null;
            else
            {
                temp = Head;
                Head = Head.Next;
            }

            if (Head == null)
                Tail = null;

            return temp;
        }

        public bool VoidExam()
        {
            if ((Head == null) && (Tail == null))
                return true;
            else
                return false;
        }


    }
}
