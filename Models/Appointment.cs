using System;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
{
    private static int _nextId = 1;

    private int _durationMinutes;
    private bool _isPaid;
    private decimal _discountMultiplier = 1m;

    public int Id { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }

    public AppointmentStatus Status { get; private set; }
    public string Notes { get; private set; }

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;

    public bool IsPaid => _isPaid;

    public bool IsCancelled => Status == AppointmentStatus.Cancelled;
    public string CancellationReason => IsCancelled ? Notes : "";

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = "";
        Id = _nextId++;
    }

    // Обрано варіант А: множник знижки живе в базовому класі Appointment.
    // Чому: одна властивість працює для всіх типів одразу (і для майбутніх також),
    // підкласи не змінюються — їхній base.GetCost() уже містить знижку, тож
    // VIP-терміновий = (база × 0.8) × 1.5, VIP-консультація = (база × 0.8) × 1.3.
    // Нові підкласи (VIP, дитячий, пенсійний тариф) не потрібні — це Open/Closed.
    public decimal DiscountMultiplier
    {
        get => _discountMultiplier;
        set
        {
            if (value <= 0m || value > 1m)
                throw new ArgumentOutOfRangeException(nameof(DiscountMultiplier), "Множник знижки має бути в межах (0; 1].");

            _discountMultiplier = value;
        }
    }

    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m * DiscountMultiplier;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public void MarkPaid()
    {
        if (IsCancelled)
            return;

        _isPaid = true;
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Cancelled;
        if (reason.Length > 0)
            Notes = reason;

        return true;
    }


    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Completed;
        return true;
    }

    public override string ToString()
    {
        string result = "[" + Id + "] " + GetDescription() + " | Пацієнт #" + PatientId + " → Лікар #" + DoctorId +
            " | " + ScheduledAt.ToString("dd.MM.yyyy HH:mm") + "–" + EndsAt.ToString("HH:mm") +
            " | " + Status;

        if (Notes.Length > 0)
            result += " | " + Notes;

        result += " | " + GetCost().ToString("F2") + " грн";

        return result;
    }
}