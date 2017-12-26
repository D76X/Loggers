using Microsoft.Practices.Unity;

namespace LogXtreme.WinDsk.Infrastructure.Unity {

    public static class UnityExtensions {

        /// <summary>
        /// This extension method is to make it easier and more explicit to register views 
        /// and view models types for navigation via Prism mavigation framework with the 
        /// DI container. Notice that the navigatiuon name typeof(T).FullName is the fully
        /// qualified name of the type.
        /// </summary>
        /// <typeparam name="T">The type of a view or view model to navigate to</typeparam>
        /// <param name="container">The refence to THIS of teh extension method</param>
        public static void RegisterTypeForNavigation<T>(this IUnityContainer container) {
            container.RegisterType(typeof(object), typeof(T), typeof(T).FullName);
        }
    }
}
