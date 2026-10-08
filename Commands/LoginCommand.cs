using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly NavigationStore _navigateStore;

        public LoginCommand(NavigationStore navigateStore)
        {
            _navigateStore = navigateStore;
        }

        public override void Execute(object parameter)
        {
            _navigateStore.CurrentViewModel = new EBookReaderViewModel(_navigateStore);
        }
    }
}
