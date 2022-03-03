using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldHospital
{
    internal class Hospitalization
    {
        public int Beds { get; set; }
        public Person Head { get; set; }
        public Person Tail { get; set; }
        public Person HeadBed { get; set; }
        public Person TailBed { get; set; }


        public Hospitalization(int beds)
        {
            Beds = beds;
            Head = null;
            Tail = null;
            HeadBed = null;
            TailBed = null;
        }

        /*public override string ToString()
        {
            return "\n O total de camas é: " + Beds;
        }*/

        public void Push(Person person)
        {
            if (Beds > 0)
            {
                PushBed(person);
            }
            else
            {
                if (VoidLine())
                {
                    Head = Tail = person;
                }
                else
                {
                    Tail.Next = person;
                    Tail = person;
                }

                Console.WriteLine("\n xxxx Nao temos leitos disponiveis. O paciente aguardara na fila de espera de leitos.");
                Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                Console.ReadKey();
            }
        }

        public void PushBed(Person person)
        {
            if (VoidLineBeds())
            {
                HeadBed = TailBed = person;
                Beds--;
            }
            else
            {
                TailBed.Next = person;
                TailBed = person;
                Beds--;
            }
        }

        /*        public void Print()
                {
                    Person temp = HeadBed;

                    do
                    {
                        Console.WriteLine(temp.ToString());
                        temp = temp.Next;
                    } while (temp != null);
                }*/

        public int QuantityBed()
        {
            Person person = HeadBed;
            int count = 0;

            if (person == null)
            {
                count = 0;
            }
            else
            {
                do
                {
                    count++;
                    person = person.Next;
                } while (person != null);
            }

            return count;
        }

        public int QuantityLine()
        {
            Person person = Head;
            int count = 0;

            if (person == null)
            {
                count = 0;
            }
            else
            {
                do
                {
                    count++;
                    person = person.Next;
                } while (person != null);
            }

            return count;
        }

        public Person LoadPerson()
        {
            Person person = null;

            if (VoidLine())
            {
                person = null;
            }
            else
            {
                person = Head;
                Head = Head.Next;
            }
            if (Head == null)
                Tail = null;

            return person;
        }

        public void LoadBed(Person person)
        {
            Person personPrincipal = HeadBed.Next, personPosition = HeadBed;

            if (string.Compare(person.Cpf, HeadBed.Cpf) == 0)
            {
                HeadBed = HeadBed.Next;
                Beds++;
            }
            else
            {
                do
                {
                    if (string.Compare(personPrincipal.Cpf, person.Cpf) == 0)
                    {
                        personPosition.Next = personPrincipal.Next;
                        Beds++;

                        if (personPosition.Next == null)
                        {
                            Tail = personPosition;
                        }
                        personPrincipal = null;
                    }
                    else
                    {
                        personPosition = personPrincipal;
                        personPrincipal = personPrincipal.Next;
                    }

                } while (personPrincipal != null);
            }
        }

        public bool VoidLineBeds()
        {
            if ((HeadBed == null) && (TailBed == null))
                return true;
            else
                return false;
        }

        public bool VoidLine()
        {
            if ((Head == null) && (Tail == null))
                return true;
            else
                return false;
        }
    }
}
