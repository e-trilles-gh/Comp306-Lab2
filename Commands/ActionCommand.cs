using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301434046_eskim__Lab2.Commands
{
    public class ActionCommand : CommandBase
    {
        private readonly Action<object> _execute;

        private readonly Predicate<object> _canExecute;

        public ActionCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
