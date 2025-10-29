using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WPF8_PRACT
{
    public class Doctor : INotifyPropertyChanged
    {
        private int _doctorId;
        public int DoctorId
        {
            get => _doctorId;
            set
            {
                if (_doctorId != value)
                {
                    _doctorId = value;
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

        private string _specialisation = "";
        public string Specialisation
        {
            get => _specialisation;
            set
            {
                if (_specialisation != value)
                {
                    _specialisation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SaveToFile()
        {
            if (!Directory.Exists("Doctors"))
                Directory.CreateDirectory("Doctors");

            string fileName = $"D_{DoctorId}.json";
            string filePath = Path.Combine("Doctors", fileName);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(filePath, json);
        }

        public static Doctor LoadFromFile(int doctorId)
        {
            string fileName = $"D_{doctorId}.json";
            string filePath = Path.Combine("Doctors", fileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Doctor>(json);
            }
            return null;
        }

        public static int GenerateDoctorId()
        {
            if (!Directory.Exists("Doctors"))
                return new Random().Next(10000, 100000);

            var files = Directory.GetFiles("Doctors", "D_*.json");
            var existingIds = new HashSet<int>();

            foreach (var file in files)
            {
                string json = File.ReadAllText(file);
                var doctor = JsonSerializer.Deserialize<Doctor>(json);
                if (doctor != null)
                    existingIds.Add(doctor.DoctorId);
            }

            Random random = new Random();
            int newId;
            do
            {
                newId = random.Next(1, 100000);
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
