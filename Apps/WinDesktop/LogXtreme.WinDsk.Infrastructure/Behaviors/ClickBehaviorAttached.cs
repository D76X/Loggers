using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LogXtreme.WinDsk.Infrastructure.Behaviours {

    public class ClickBehaviorAttached {

        public static ICommand GetCommand(DependencyObject obj) {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(
            DependencyObject obj, 
            ICommand value) {

            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command", 
                typeof(ICommand), 
                typeof(ClickBehaviorAttached), 
                new PropertyMetadata(CommandPropertyChanged));

        private static void CommandPropertyChanged(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e) {

            var uiElement = d as UIElement;

            if (uiElement == null) { return; }

            uiElement.MouseDown += MouseDown;
        }

        private static void MouseDown(object sender, MouseButtonEventArgs e) {

            var dependencyObject = sender as DependencyObject;

            if(dependencyObject == null) { return; }

            var command = GetCommand(dependencyObject);

            if (command == null) { return; }

            if (command.CanExecute(null)) { command.Execute(null); }
        }
    }
}
