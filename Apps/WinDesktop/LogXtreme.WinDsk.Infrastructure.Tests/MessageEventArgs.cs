using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {
    internal class MessageEventArgs : EventArgs {

        public readonly string Message;

        public MessageEventArgs(string message) {
            this.Message = message;
        }
    }
}
