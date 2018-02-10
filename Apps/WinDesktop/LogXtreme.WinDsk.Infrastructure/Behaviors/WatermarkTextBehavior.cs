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
            get { return GetIsWatermarked(AssociatedObject); }
            private set { AssociatedObject.SetValue(IsWatermarkedPropertyKey, value); }
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
        /// This readonly property is applied to the TextBox and indicates whether the watermark
        /// is currently being displayed.  It allows a style to change the visual appearance of 
        /// the TextBox. 
        /// </summary>
        public static readonly DependencyProperty IsWatermarkedProperty =
            IsWatermarkedPropertyKey.DependencyProperty;

        static readonly DependencyPropertyKey IsWatermarkedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsWatermarked",
                typeof(bool),
                typeof(WatermarkTextBehavior),
                new PropertyMetadata(0));

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
