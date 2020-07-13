using DAN_XLV_MilosPeric.Command;
using DAN_XLV_MilosPeric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLV_MilosPeric.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        Login login;
        public LoginViewModel(Login viewLogin)
        {
            login = viewLogin;
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private ICommand submit;
        public ICommand Submit
        {
            get
            {
                if (submit == null)
                {
                    submit = new RelayCommand(SubmitCommandExecute, param => CanSubmitCommandExecute());
                }
                return submit;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                if (UserName.Equals("Mag2019") && password.Equals("Mag2019"))
                {
                    WorkerView workerView = new WorkerView();
                    login.Close();
                    workerView.Show();
                    return;
                }
                else if (UserName.Equals("Man2019") && password.Equals("Man2019"))
                {
                    ManagerView managerView = new ManagerView();
                    login.Close();
                    managerView.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong usename or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSubmitCommandExecute()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
