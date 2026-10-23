using System;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;

    public BillingManager(AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    private IPayable[] FilterUnpaid(Appointment[] list)
    {
        int n = 0;
        for (int i = 0; i < list.Length; i++)
            if (!list[i].IsPaid && !list[i].IsCancelled)
                n++;

        IPayable[] result = new IPayable[n];
        int idx = 0;

        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].IsPaid && !list[i].IsCancelled)
            {
                result[idx] = list[i];
                idx++;
            }
        }

        return result;
    }

    public IPayable[] GetAllUnpaid()
    {
        return FilterUnpaid(_appointments.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return FilterUnpaid(_appointments.GetByPatient(patientId));
    }

    public decimal GetTotalDebt()
    {
        IPayable[] unpaid = GetAllUnpaid();
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
            total += unpaid[i].GetCost();

        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        IPayable[] unpaid = GetUnpaidByPatient(patientId);
        decimal total = 0m;

        for (int i = 0; i < unpaid.Length; i++)
            total += unpaid[i].GetCost();

        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] all = _appointments.GetAll();

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].Id == appointmentId)
            {
                if (all[i].IsPaid || all[i].IsCancelled)
                    return false;

                all[i].MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            string cost = items[i].GetCost().ToString("F2");

            if (items[i] is Appointment a)
                Console.WriteLine(a + " | Сума: " + cost + " грн");
            else
                Console.WriteLine("#" + (i + 1) + " | Сума: " + cost + " грн");
        }
    }
}