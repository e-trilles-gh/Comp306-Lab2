using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.Commands
{
    public class ExitCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
