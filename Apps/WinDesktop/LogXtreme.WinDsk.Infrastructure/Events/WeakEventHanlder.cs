using System;
using System.Diagnostics.Contracts;

namespace LogXtreme.WinDsk.Infrastructure.Events {

    /// <summary> 
    /// References
    /// Solution 4: Reusable Wrapper
    /// https://www.codeproject.com/Articles/29922/Weak-Events-in-C
    /// </summary>    
    public sealed class WeakEventHanlder : IWeakEventHanlder {

        public static WeakEventHanlder Register<TEventSource, TEventListener>(
            TEventSource senderObject,
            Action<TEventSource, EventHandler> registerEvent,
            Action<TEventSource, EventHandler> deregisterEvent,
            TEventListener listeningObject,
            Action<TEventListener, object, EventArgs> forwarderAction)
            where TEventSource : class
            where TEventListener : class {

            Contract.Requires(senderObject != null);
            Contract.Requires(listeningObject != null);

            VerifyDelegateDoesNotStrongReference(registerEvent, nameof(registerEvent));
            VerifyDelegateDoesNotStrongReference(deregisterEvent, nameof(deregisterEvent));
            VerifyDelegateDoesNotStrongReference(forwarderAction, nameof(forwarderAction));

            WeakEventHanlder weh = new WeakEventHanlder(listeningObject);
            EventHandler eh = weh.MakeDeregisterCodeAndWeakEventHandler(senderObject, deregisterEvent, forwarderAction);

            return weh;
        }

        /// <summary>
        /// The WeakEventHandler create a weak reference to a delegate. We do not want these delegates
        /// to capture a strong references to objects. It's possible to check whether a delegate captures 
        /// any variables. The compiler will generate an instance method for lambda expressions that 
        /// capture variables while it generates a static method for lambda expressions that don't. 
        /// WeakEventHandler checks this using Delegate.Method.IsStatic and will throw an exception if 
        /// the delegate is compiled to an instance method instead of a static method.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="parameterName"></param>
        private static void VerifyDelegateDoesNotStrongReference(Delegate d, string parameterName) {

            Contract.Requires(d != null);

            if (!d.Method.IsStatic) {
                throw new ArgumentException($"Delegates used for {nameof(WeakEventHanlder)} must not capture any variables (must point to static methods)", parameterName);
            }

        }

        internal WeakEventHanlder(object listeningObject) {

        }

        public bool TryDeregister() {
            throw new NotImplementedException();
        }
    }
}
