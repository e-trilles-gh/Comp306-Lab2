using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStor;

        public LogoutCommand(NavigationStore navigationStor)
        {
            _navigationStor = navigationStor;
        }

        public override void Execute(object parameter)
        {
            _navigationStor.CurrentViewModel = new LoginViewModel(_navigationStor);
        }
    }
}
