using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class DoctorManager
    {
        private Doctor[] doctors = new Doctor[100]; // fixed size array
        private int count = 0;

        // add new doctor
        public void Add(Doctor d)
        {
            if (count >= doctors.Length)
            {
                Console.WriteLine("Array is full!");
                return;
            }

            doctors[count] = d;
            count++;
        }

        // show all doctors
        public void ShowAll()
        {
            if (count == 0)
            {
                Console.WriteLine("No doctors yet.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(doctors[i]);
            }
        }

        // find by speciality (partial match, case-insensitive)
        public Doctor FindBySpeciality(string speciality)
        {
            for (int i = 0; i < count; i++)
            {
                if (doctors[i].Specialty.ToLower().Contains(speciality.ToLower()))
                    return doctors[i];
            }

            return null;
        }

        // remove by id
        public bool Remove(int id)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
            {
                if (doctors[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return false;

            for (int i = index; i < count - 1; i++)
            {
                doctors[i] = doctors[i + 1];
            }

            doctors[count - 1] = null;
            count--;

            return true;
        }

        // find by id, for AppointmentManager
        public Doctor FindById(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (doctors[i].Id == id)
                    return doctors[i];
            }

            return null;
        }

        // stats with unique specialities
        public void Statistics()
        {
            if (count == 0)
            {
                Console.WriteLine("No doctors yet.");
                return;
            }

            List<string> uniqueSpecs = new List<string>();

            for (int i = 0; i < count; i++)
            {
                string spec = doctors[i].Specialty;

                if (!uniqueSpecs.Contains(spec))
                    uniqueSpecs.Add(spec);
            }

            Console.WriteLine("Total doctors: " + count);
            Console.WriteLine("Unique specialities: " + uniqueSpecs.Count);

            foreach (var s in uniqueSpecs)
            {
                Console.WriteLine("- " + s);
            }
        }

        public int Count => count;
    }
}