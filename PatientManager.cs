using System;

namespace ConsoleApp1
{
    public class PatientManager
    {
        private Patient[] patients = new Patient[100]; // fixed size array
        private int count = 0;


        public Patient this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    return null;

                return patients[index];
            }
        }
        // add new patient
        public void Add(Patient p)
        {
            if (count >= patients.Length)
            {
                Console.WriteLine("Array is full!");
                return;
            }

            patients[count] = p;
            count++;
        }

        // show all patients
        public void ShowAll()
        {
            if (count == 0)
            {
                Console.WriteLine("No patients yet.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(patients[i]);
            }
        }

        // find by name (partial match, case-insensitive)
        public Patient FindByName(string name)
        {
            for (int i = 0; i < count; i++)
            {
                if (patients[i].FullName.ToLower().Contains(name.ToLower()))
                    return patients[i];
            }

            return null;
        }

        // remove by id
        public bool Remove(int id)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
            {
                if (patients[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return false;

            // shift elements left
            for (int i = index; i < count - 1; i++)
            {
                patients[i] = patients[i + 1];
            }

            patients[count - 1] = null;
            count--;

            return true;
        }
        public bool TryFindById(int id, out Patient patient)
        {
            patient = FindById(id);
            return patient != null;
        }

        public Patient[] FindByBloodType(BloodType bloodType)
        {
            int n = 0;
            for (int i = 0; i < count; i++)
                if (patients[i].BloodType == bloodType)
                    n++;

            Patient[] result = new Patient[n];
            int idx = 0;

            for (int i = 0; i < count; i++)
            {
                if (patients[i].BloodType == bloodType)
                {
                    result[idx] = patients[i];
                    idx++;
                }
            }

            return result;
        }

        // stats
        public void Statistics()
        {
            if (count == 0)
            {
                Console.WriteLine("No patients yet.");
                return;
            }

            int children = 0, adults = 0, seniors = 0;
            double totalAge = 0;

            for (int i = 0; i < count; i++)
            {
                totalAge += patients[i].Age;

                string cat = patients[i].Classify();
                if (cat == "Child") children++;
                else if (cat == "Adult") adults++;
                else seniors++;
            }

            Console.WriteLine("Total patients: " + count);
            Console.WriteLine("Average age: " + (totalAge / count).ToString("F1"));
            Console.WriteLine("Children: " + children);
            Console.WriteLine("Adults: " + adults);
            Console.WriteLine("Seniors: " + seniors);
        }

        // find by id, for AppointmentManager
        public Patient FindById(int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (patients[i].Id == id)
                    return patients[i];
            }

            return null;
        }

        public int Count => count;
    }
}