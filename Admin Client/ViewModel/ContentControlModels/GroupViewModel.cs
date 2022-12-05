﻿using Admin_Client.Model;
using Admin_Client.Model.DB;
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

namespace Admin_Client.ViewModel.ContentControlModels
{
	public class GroupViewModel : NotifyPropertyChangedHandler
	{

        private ObservableCollection<TblUser> users = new ObservableCollection<TblUser>();
        public ObservableCollection<TblUser> Users
        {
            get { return users; }
            set { users = value; }
        }

        public GroupViewModel()
        {

        }
        public void Add()
        {
            MainWindowModelSingleton.Instance.StartPopupConfirm(new TblUser(), PopupMethod.Add);
        }

        CancellationTokenSource tokenSource;
        public void Update()
        {
            LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Update Click"));
            if (tokenSource != null && tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
            tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(UpdateUsersListThread, new object[] { tokenSource.Token });
        }
     


        private void UpdateUsersListThread(object o)
        {
            object[] array = o as object[];
            CancellationToken token = (CancellationToken)array[0];

            while (!token.IsCancellationRequested)
            {
                // CHANGE THE FAKEDATEBASE.GETUSERS() - TODO
                List<TblUser> users = FAKEDATABASE.GetUsers();  

                bool found;
                    foreach (var userItem in users)
                    {
                        found = false;
                        foreach (var UserItem in Users)
                        {
                            if (userItem.FldUserId == UserItem.FldUserId)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                        //(change this) temp nested if statement to sort a group by its users admin level, this can easily be changed to be based on the groups associated userIDs
                        if (userItem.FldIsAdmin.Equals(false)) {
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { Users.Add(userItem); }));
                        }
                    }
                }
                break;
            }
        }

    }
}
