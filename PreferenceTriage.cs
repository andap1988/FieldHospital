﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldHospital
{
    internal class PreferenceTriage
    {
        Person Head { get; set; }
        Person Tail { get; set; }

        public PreferenceTriage()
        {
            Head = null;
            Tail = null;
        }

        public void Push(Person person)
        {
            if (VoidList())
            {
                Head = Tail = person;
            }
            else
            {
                Tail.Next = person;
                Tail = person;
            }
        }


        public int Quantity()
        {
            Person temp = Head;
            int quantity = 0;

            if (VoidList())
                quantity = 0;
            else
            {
                do
                {
                    temp = temp.Next;
                    quantity++;
                } while (temp != null);

            }
            
            return quantity;
        }

        public Person LoadPerson()
        {
            Person temp;

            if (VoidList())
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

        public bool VoidList()
        {
            if ((Head == null) && (Tail == null))
                return true;
            else
                return false;
        }
    }
}
