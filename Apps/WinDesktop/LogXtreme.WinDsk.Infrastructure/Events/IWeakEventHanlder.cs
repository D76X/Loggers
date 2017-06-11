namespace LogXtreme.WinDsk.Infrastructure.Events {
    internal interface IWeakEventHanlder {

        /// <summary>
        /// Deregisters the event handler if the listener is no longer alive.
        /// </summary>
        /// <returns></returns>
        bool TryDeregister();
    }
}
