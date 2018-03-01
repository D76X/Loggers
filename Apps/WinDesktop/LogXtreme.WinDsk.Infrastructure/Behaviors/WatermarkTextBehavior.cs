using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LogXtreme.WinDsk.Infrastructure.Behaviors {

    /// <summary>
    /// Refs
    /// http://julmar.com/blog/programming/playing-with-wpf-behaviors-a-watermarktext-behavior/
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/system.windows.dependencypropertykey(v=vs.110).aspx
    /// https://stackoverflow.com/questions/1122595/how-do-you-create-a-read-only-dependency-property
    /// </summary>
    public class WatermarkTextBehavior : Behavior<TextBox> {

        /// <summary>
        /// The DP for the watermark text.
        /// </summary>
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                @"Text",
                typeof(string),
                typeof(WatermarkTextBehavior),
                new FrameworkPropertyMetadata(@""));


        /// <summary>
        /// Retrieves the current watermarked state of the TextBox.
        /// </summary>
        public bool IsWatermarked {
            get => GetIsWatermarked(AssociatedObject); 
            private set => AssociatedObject.SetValue(IsWatermarkedPropertyKey, value); 
        }

        /// <summary>
        /// Retrieves the current watermarked state of the TextBox.
        /// </summary>
        /// <param name=”tb”>Reference to the TextBox</param>
        /// <returns></returns>
        public static bool GetIsWatermarked(TextBox tb) {
            return (bool)tb.GetValue(IsWatermarkedProperty);
        }

        /// <summary>
        /// Important!
        /// The Key for a read only attached property must be declared before 
        /// the corresponding public DP. The reason is that static declarations 
        /// are run in the order they are declared in the source prior to the 
        /// creation of the first instance of the class in which the static code 
        /// is defined or on the first invokation of static code. When the 
        /// IsWatermarkedProperty is read the Key must be already available 
        /// or a NullReference exception is thrown.
        /// </summary>
        private static readonly DependencyPropertyKey IsWatermarkedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsWatermarked",
                typeof(bool),
                typeof(WatermarkTextBehavior),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// This readonly property is applied to the TextBox and indicates whether the watermark
        /// is currently being applied to the target of the behavior.  It allows to take actions
        /// according to whther IsWatermarked is set to true or false. The logic that sets this 
        /// read-only property to either ftrue or false is completely encapsulated in the behavior
        /// and for example a style can be used to change the visual appearance of the target
        /// TexBox by means of property triggers.
        /// the TextBox. 
        /// </summary>
        public static readonly DependencyProperty IsWatermarkedProperty =
            IsWatermarkedPropertyKey.DependencyProperty;        

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.GotFocus += OnGotFocus;
            AssociatedObject.LostFocus += OnLostFocus;
            OnLostFocus(null, null);
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.GotFocus -= OnGotFocus;
            AssociatedObject.LostFocus -= OnLostFocus;
        }

        /// <summary>
        /// This method is called when the textbox gains focus.  
        /// It removes the watermark.
        /// </summary>
        /// <param name=”sender”></param>
        /// <param name=”e”></param>
        private void OnGotFocus(object sender, RoutedEventArgs e) {

            if (string.Compare(
                AssociatedObject.Text,
                this.Text,
                StringComparison.OrdinalIgnoreCase) == 0) {

                AssociatedObject.Text = @"";
                IsWatermarked = false;
            }
        }

        /// <summary>
        /// This method is called when focus is lost from the TextBox.  
        /// It puts the watermark into place if no text is in the textbox.
        /// </summary>
        /// <param name=”sender”></param>
        /// <param name=”e”></param>
        private void OnLostFocus(object sender, RoutedEventArgs e) {

            if (string.IsNullOrEmpty(AssociatedObject.Text)) {

                AssociatedObject.Text = this.Text;
                IsWatermarked = true;
            }
        }
    }
}
