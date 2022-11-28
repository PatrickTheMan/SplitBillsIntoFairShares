﻿using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using Admin_Client.View.Windows.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupConfirmWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private object target;
		private Action action;

		private Window currentWindow;

		#endregion

		#region Properties

		private string actionText;

		public string ActionText
		{
			get { return actionText; }
			set { actionText = value; NotifyPropertyChanged(); }
		}

		private string targetText;

		public string TargetText
		{
			get { return targetText; }
			set { targetText = value; NotifyPropertyChanged(); }
		}

		#endregion

		#region Constructor

		public PopupConfirmWindowModel(Window currentWindow, object target, PopupMethod popupMethod)
		{
			switch (popupMethod)
			{
				case PopupMethod.Edit: this.action = Edit; break;
				case PopupMethod.Create: this.action = Create; break;
				case PopupMethod.Delete: this.action = Delete; break;
			}
			this.target = target;
			this.currentWindow = currentWindow;

			ActionText = action.GetMethodInfo().Name;
			if (action.GetMethodInfo().Name == "Create")
			{
				TargetText = target.GetType().Name;
			} else
			{
				switch (this.target.GetType().Name)
				{
					case "TblUser":
						{
							TblUser user = (TblUser)target;
							TargetText = user.FldUserId + " - " + user.FldFirstName + " " + user.FldLastName;
							break;
						}
					case "TblGroup":
						{
							TblGroup group = (TblGroup)target;
							TargetText = group.FldGroupId + " - " + group.FldGroupName;
							break;
						}
					default: break;
				}
			}
		}

		#endregion

		#region Public Methods

		public void Confirm()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(target.GetType().Name + "." + action.GetMethodInfo().Name + "() --> Invoke"));
			action.Invoke();
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, target.GetType().Name + "." + action.GetMethodInfo().Name + "() == Success"));

			currentWindow.Close();
		}

		public void Cancel()
		{
			currentWindow.Close();
		}

		#endregion

		#region Private Methods

		private void Edit()
		{
			switch (target.GetType().Name)
			{
				case "TblUser":
					{
						TblUser user = (TblUser)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + user.FldUserId + " - " + user.FldFirstName + " " + user.FldLastName));
						break;
					}
				case "TblGroup":
					{
						TblGroup group = (TblGroup)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + group.FldGroupId + " - " + group.FldGroupName));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name);
			}
		}

		private void Create()
		{
			switch (target.GetType().Name)
			{
				case "TblUser":
					{
						TblUser user = (TblUser)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "New: ID " + user.FldUserId + " - " + user.FldFirstName + " " + user.FldLastName));
						break;
					}
				case "TblGroup":
					{
						TblGroup group = (TblGroup)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "New: ID " + group.FldGroupId + " - " + group.FldGroupName));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name);
			}
		}

		private void Delete()
		{
			switch (target.GetType().Name)
			{
				case "TblUser":
					{
						TblUser user = (TblUser)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + user.FldUserId + " - " + user.FldFirstName + " " + user.FldLastName));
						break;
					}
				case "TblGroup":
					{
						TblGroup group = (TblGroup)target;

						/*Do Stuff*/

						LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.Success, "Target: ID " + group.FldGroupId + " - " + group.FldGroupName));
						break;
					}
				default: throw new Exception("Has not been implemented: " + target.GetType().Name + "." + action.GetMethodInfo().Name);
			}
		}

		#endregion

	}
}