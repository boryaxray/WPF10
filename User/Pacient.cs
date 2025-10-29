using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WPF8_PRACT.User;

namespace WPF8_PRACT
{
    public class Pacient : INotifyPropertyChanged
    {
        private string _pacientId;
        public string PacientId
        {
            get => _pacientId;
            set
            {
                if (_pacientId != value)
                {
                    _pacientId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(AgeStatus));
                }
            }
        }

        private string _lastName = "";
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _middleName = "";
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _birthday = DateTime.Now;
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(AgeStatus));
                }
            }
        }


        private string _phoneNumber = "";
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<AppointmentStory> _appointmentStories = new List<AppointmentStory>();
        public List<AppointmentStory> AppointmentStories
        {
            get => _appointmentStories;
            set
            {
                if (_appointmentStories != value)
                {
                    _appointmentStories = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DaysSinceLastAppointment));
                }
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var birthDate = Birthday;
                var age = today.Year - birthDate.Year;
                if (birthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        public string AgeStatus
        {
            get => Age >= 18 ? "Совершеннолетний" : "Несовершеннолетний";
        }

        public string DaysSinceLastAppointment
        {
            get
            {
                if (AppointmentStories == null || !AppointmentStories.Any())
                    return "Первый прием в клинике";

                var lastAppointment = AppointmentStories
                    .OrderByDescending(a => a.Date)
                    .FirstOrDefault();

                if (lastAppointment == null)
                    return "Первый прием в клинике";

                var lastDate = lastAppointment.Date;
                var days = (DateTime.Now - lastDate).Days;

                return days == 0 ? "Сегодня" : $"{days} дней назад";
            }
        }

        public void SaveToFile()
        {
            string fileName = $"P_{PacientId}.json";
            string filePath = System.IO.Path.Combine("Pacients", fileName);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(this, options);
            System.IO.File.WriteAllText(filePath, json);
        }

        public static Pacient LoadFromFile(string pacientId)
        {
            string fileName = $"P_{pacientId}.json";
            string filePath = System.IO.Path.Combine("Pacients", fileName);

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Pacient>(json);
            }
            return null;
        }

        public static string GeneratePacientId()
        {
            var files = System.IO.Directory.GetFiles("Pacients", "P_*.json");
            var existingIds = files.Select(file =>
                System.IO.Path.GetFileNameWithoutExtension(file).Substring(2));

            Random random = new Random();
            string newId;
            do
            {
                newId = random.Next(1, 100000).ToString();
            } while (existingIds.Contains(newId));

            return newId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



