
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.TriggerActions {

    /// <summary>
    /// Trigger actions are very similar in concept to behaviors and are based on the same pattern. 
    /// You may provide overrides for OnAttached, OnDetaching but in addition you must implement the 
    /// abstract method Invoke. In behaviors normally the OnAttached override is used to subcribe 
    /// to events or change properties available on the public API of the target object. Conversely,
    /// teardown ops are performed in the OnDetaching i.e. unsubscribing from events, etc. 
    /// 
    /// For example in the ClickBehavior we do something like below.
    /// 
    /// this.AssociatedObject.MouseDown += this.MouseDown 
    /// 
    /// And then provide the logic of the behavior in the MouseDown private implementation. However,
    /// this also binds the AssociatedObject.MouseDown event to the behavior and prevents the user 
    /// of the behavior from specifying an event different from the MouseDown event on the target 
    /// object as a trigger of the behavior.
    /// 
    /// In this respect a trigger action introduces a further degree of freedom ove the equvalent 
    /// behavior. 
    /// 
    /// Refs
    /// http://putridparrot.com/blog/a-simple-wpf-triggeraction-to-turn-events-into-commands/
    /// https://msdn.microsoft.com/en-gb/magazine/dn237302.aspx
    /// </summary>
    public class CommandAction : TriggerAction<DependencyObject> {

        /// <summary>
        /// Command should be bound to a property of type ICommand exposed
        /// by the VM.
        /// </summary>
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(CommandAction),
                new PropertyMetadata(null, OnCommandChanged));

        public object CommandParameter {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private IDisposable canExecuteChanged;

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(CommandAction),
                new PropertyMetadata(0, OnCommandPrameterChanged));        

        private static void OnCommandChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var ca = d as CommandAction;

            if (ca != null) {

                ca.canExecuteChanged?.Dispose();

                var command = e.NewValue as ICommand;

                if (command != null) {

                    ca.canExecuteChanged = Observable
                        .FromEventPattern(
                        h => command.CanExecuteChanged += h,
                        h => command.CanExecuteChanged -= h)
                        .Subscribe(_ => ca.SynchronizeElementState());
                }
            }
        }

        private static void OnCommandPrameterChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var ca = d as CommandAction;

            if (ca != null) {
                ca.SynchronizeElementState();
            }
        }

        private void SynchronizeElementState() {

            ICommand command = this.Command;

            if (command != null) {

                var fe = AssociatedObject as FrameworkElement;

                if (fe != null) {
                    fe.IsEnabled = command.CanExecute(this.CommandParameter);
                }
            }
        }

        protected override void Invoke(object parameter) {

            var cmd = this.Command;

            // need DP for payload
            if (cmd != null && cmd.CanExecute(null)) {
                this.Command.Execute(this.CommandParameter);
            }
        }

        protected override void OnAttached() {

            // you may do something in here by accessing the AssociatedObject
            // for example subscribe to events, change properties, etc.           

            base.OnAttached();
        }

        protected override void OnDetaching() {

            // you may do something in here by accessing the AssociatedObject
            // for example unsubscribe from events, change properties, etc.   

            base.OnDetaching();
        }
    }
}
