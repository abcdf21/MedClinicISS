using System;

namespace MedClinicISS
{
    public class PatientClass
    {
        public int PatientId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        // Конструктор класса
        public PatientClass(int patientId, string surname, string name, string patronymic, DateTime dateOfBirth, string phoneNumber)
        {
            PatientId = patientId;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }
    }
}