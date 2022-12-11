﻿using Admin_Client.Model;
using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
    public class PopupAddUserWindowModel : NotifyPropertyChangedHandler
    {
        private Window currentWindow;

        #region Properties
        private ObservableCollection<tblUser> users = new ObservableCollection<tblUser>();
        public ObservableCollection<tblUser> Users
        {
            get { return users; }
            set { users = value; }
        }
        public string Searchbar { get; set; }
        List<tblUser> templist = HttpClientHandler.GetUsers();
        List<tblUser> userList = HttpClientHandler.GetUsers();
        #endregion

        #region Constructor
        public PopupAddUserWindowModel(Window currentWindow, object o)
        {
            this.currentWindow = currentWindow;
        }
        #endregion

        #region Public Methods
        public void Add(ListBox listBox)
        {

        }
        public void Search()
        {
            //created a temp list to use for searching and not ruining the original list it is based on
            //also need to find a way to search by anything in the list, and not specify by say fldfirstname
            templist = new List<tblUser>(userList.Where(
                x => x.fldFirstName.IndexOf(Searchbar, StringComparison.InvariantCultureIgnoreCase) >= 0
                ||      x.fldLastName.IndexOf(Searchbar, StringComparison.InvariantCultureIgnoreCase) >= 0
                ||      x.fldEmail.IndexOf(Searchbar, StringComparison.InvariantCultureIgnoreCase) >= 0));
        }

        public void Cancel()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
            currentWindow.Close();
            MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
        }


        CancellationTokenSource tokenSource;
        public void Update()
        {
            Users.Clear();
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Users Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
        }
        #endregion

        #region Private Methods
        private void UpdateUsersListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                // CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
                

                bool found;
                foreach (var userItem in templist)
                {
                    found = false;
                    foreach (var UserItem in Users)
                    {
                        if (userItem.fldUserID == UserItem.fldUserID)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { Users.Add(userItem); }));
                    }
                }
                break;
            }
        }
        #endregion
    }

}
