using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace DesignPattern.Proxy
{

    /// MVVM
    /// Model

    public class Person
    {
        public string FirstName, LastName;

    }

    public class PersonViewModel
    {
        public PersonViewModel(Person person)
        {
            this.Person = person;
        }

        public Person Person { get; set; }

        public string FirstName 
        { 
            get => this.Person.FirstName;
            set
            {
                if (this.Person.FirstName == value) return;
                this.Person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string LastName 
        { 
            get => this.Person.LastName;
            set
            {
                if (this.Person.LastName == value) return;
                this.Person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get => $"{this.Person.FirstName} {this.Person.LastName}";
            set
            {
                if(value == null)
                {
                    FirstName = LastName = null;
                    return;
                }
                string[] names = value.Split("");
                if(names.Length > 0) this.Person.FirstName = names[0];
                if(names.Length > 1) this.Person.LastName = names[1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
