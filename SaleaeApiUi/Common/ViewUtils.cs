using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace SaleaeApiUi.Common
{
    public abstract class ViewUtils
    {


        public static void NextAfterKeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                (sender as FrameworkElement)?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }


        public static void TextBoxUpdateBindOnEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
            }
        }


        public static void MouseDragWindow(Window window, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    window.DragMove();
                }
                catch (InvalidOperationException)
                {   // Ignore if mouse button is not down by the time this executes.
                }
            }
        }


    }
}
