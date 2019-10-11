using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;
using System.Data.Entity;

namespace RegistrySystem
{
    public class ContractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ChangeCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Action CancelAction { get; set; }
        public Action ConfirmAction { get; set; }

        public Contract Contract { get; set; }
        public string Title { get; set; } = "Добавление договора";
        public string ButtonName { get; set; } = "Добавить";
        public IEnumerable<Customer> Customers { get => Enum.GetValues(typeof(Customer)).Cast<Customer>(); }
        public IEnumerable<EducationForm> EducationForms { get => Enum.GetValues(typeof(EducationForm)).Cast<EducationForm>(); }
        public IEnumerable<ProgramType> ProgramTypes { get => Enum.GetValues(typeof(ProgramType)).Cast<ProgramType>(); }
        public List<Group> Groups { get; set; }
        public Group SelectedGroup { get; set; }


        public ContractViewModel()
        {
            ChangeCommand = new RelayCommand(() => AddContract());
            CancelCommand = new RelayCommand(() => Cancel());
            Contract = new Contract();
            SetGroupBox();
        }

        public ContractViewModel(Contract contract)
        {
            ChangeCommand = new RelayCommand(() => EditContract());
            CancelCommand = new RelayCommand(() => Cancel());
            Title = $"Изменение договора " + contract.Num;
            ButtonName = "Изменить";
            Contract = contract;
            SetGroupBox();
            using (RegistryContext db = new RegistryContext())
            {
                SelectedGroup = db.Groups.Find(Contract.GroupId);
            }
        }

        public void SetGroupBox()
        {
            using (RegistryContext db = new RegistryContext())
            {
                Groups = db.Groups.ToList();
            }
        }

        public void AddContract()
        {
            using (RegistryContext db = new RegistryContext())
            {
                db.Contracts.Add(Contract);
                db.SaveChanges();
            }
            ConfirmAction();
        }

        public void EditContract()
        {
            using (RegistryContext db = new RegistryContext())
            {
                if (Contract != null)
                {
                    Contract.Group = SelectedGroup;
                    db.Entry(Contract).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ConfirmAction();
        }

        public void Cancel()
        {
            CancelAction();
        }
    }
}
