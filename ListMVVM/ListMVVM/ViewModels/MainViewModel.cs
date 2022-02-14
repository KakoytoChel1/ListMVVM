using ListMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ListMVVM.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Phone> Phones { get; set; }

        public MainViewModel()
        {
            DataBaseLogic.StartSettings();
            Phones = new ObservableCollection<Phone>(DataBaseLogic.GetAllData());
        }

        //add properties
        private string _addTitle;
        public string AddTitle
        {
            get { return _addTitle; }
            set { _addTitle = value; OnPropertyChanged("AddTitle"); }
        }

        private string _addCompany;
        public string AddCompany
        {
            get { return _addCompany; }
            set { _addCompany = value; OnPropertyChanged("AddCompany"); }
        }

        private double _addPrice;
        public double AddPrice
        {
            get { return _addPrice; }
            set { _addPrice = value; OnPropertyChanged("AddPrice"); }
        }

        //selected item
        private Phone selectedphone;
        public Phone SelectedPhone
        {
            get { return selectedphone; }
            set { selectedphone = value; OnPropertyChanged("SelectedPhone"); }
        }

        //tab switch
        private int _selectedIndex;
        public int TSelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("TSelectedIndex"); }
        }


        //menu commands
        private DelegateCommand _openAddMenu;
        public DelegateCommand OpenAddMenuCommand
        {
            get
            {
                return _openAddMenu ??
                  (_openAddMenu = new DelegateCommand(obj =>
                  {
                      TSelectedIndex = 1;
                  }));
            }
        }

        private DelegateCommand _openChangeMenu;
        public DelegateCommand OpenChangeMenuCommand
        {
            get
            {
                return _openChangeMenu ??
                  (_openChangeMenu = new DelegateCommand(obj =>
                  {
                      TSelectedIndex = 2;
                  } ,(obj) => SelectedPhone != null ));
            }
        }


        //CRUD commands (without read)
        private DelegateCommand _addItem;
        public DelegateCommand AddItemCommand
        {
            get
            {
                return _addItem ??
                  (_addItem = new DelegateCommand(obj =>
                  {
                      Phone phone = new Phone { Title = AddTitle, Company = AddCompany, Price = AddPrice };
                      //insert new data item to the list
                      Phones.Insert(0, phone);

                      DataBaseLogic.AddNewItem(phone);
                      //zeroing 
                      AddCompany = String.Empty;
                      AddTitle = String.Empty;
                      AddPrice = 0;
                      //switch to main menu
                      TSelectedIndex = 0;
                  }));
            }
        }

        //save changes (send to data base)
        private DelegateCommand _changeItem;
        public DelegateCommand ChangeItemCommand
        {
            get
            {
                return _changeItem ??
                  (_changeItem = new DelegateCommand(obj =>
                  {
                      DataBaseLogic.UpdateItem(SelectedPhone);
                      TSelectedIndex = 0;
                  }));
            }
        }

        private DelegateCommand _deleteItem;
        public DelegateCommand DeleteItemCommand
        {
            get
            {
                return _deleteItem ??
                  (_deleteItem = new DelegateCommand(obj =>
                  {
                      DataBaseLogic.RemoveItem(SelectedPhone);
                      Phones.Remove(SelectedPhone);
                  }, (obj) => SelectedPhone != null));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
