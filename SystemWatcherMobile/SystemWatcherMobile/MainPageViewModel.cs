using FireSharp.Config;
using FireSharp.Interfaces;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SystemWatcherMobile
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "JuN06Hm78gQ5sMRoNPjUDHOiprp7B3Jq1GVxYY2h",
            BasePath = "https://watcherservice-1810a-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;


        public MainPageViewModel()
        {
            Task.Run(() => TrySelect());
        }




        public void TrySelect()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                
            }
            ObservableCollection<Data> TempDataColl = new ObservableCollection<Data>();
            for (int i = 0; i < 31; i++)
            {
                var result = client?.Get("DatasList/" + i);
                Data TempData = result.ResultAs<Data>();
                if (TempData != null)
                {
                    TempDataColl.Add(TempData);
                }
            }
            Datas = TempDataColl;

        }

        public ObservableCollection<Data> _data;

        public ObservableCollection<Data> Datas
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

       
        /// <summary>
        /// /////////////////////////////////////////////////////
        /// </summary>
        private ICommand _myCommand;
        public ICommand MyCommand
        {
            get
            {
                if (_myCommand == null)
                { _myCommand = new RelayCommand<object>(this.MyCommand_Execute); }
                return _myCommand;
            }
        }

        private async void MyCommand_Execute(object parameter)
        {
            await Task.Run(() => TrySelect());
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////
        /// </summary>


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
