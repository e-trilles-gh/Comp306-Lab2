using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2.Property
{
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox passwordBox)
            {
                return;
            }

            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if (passwordBox.Password != (string)e.NewValue)
            {
                passwordBox.Password = (string)e.NewValue;
            }

            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(
            object sender,
            RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            SetBoundPassword(
                passwordBox, passwordBox.Password);
        }
    }
}
