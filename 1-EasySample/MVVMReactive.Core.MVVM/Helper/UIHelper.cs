using System.Windows;
using System.Windows.Media;

namespace MVVMReactive.Core.MVVM.Core.MVVM.Helper
{
    public static class UIHelper
    {
        public static T GetParentRecursive<T>(string name, FrameworkElement child)
            where T : FrameworkElement
        {
            DependencyObject parent = child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            while (
                parent != null && !(parent is FrameworkElement) ||
                parent != null && parent is FrameworkElement && ((FrameworkElement)parent).Name != name);

            if (parent != null)
                return parent as T;

            return null;
        }
    }
}