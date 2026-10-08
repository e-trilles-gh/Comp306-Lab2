using _301434046_eskim__Lab2.Commands;
using _301434046_eskim__Lab2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _301434046_eskim__Lab2.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public ICommand LoginUser { get; }
        public ICommand ExitProgram { get; }

        public LoginViewModel(NavigationStore navigationStore)
        {
            LoginUser = new LoginCommand(navigationStore);
            ExitProgram = new ExitCommand();
        }
    }
}
