using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace LogXtreme.WinDsk.Infrastructure.Exceptions {

    /// <summary>
    /// Refs
    /// https://docs.microsoft.com/en-us/dotnet/framework/misc/security-and-serialization
    /// https://blogs.msdn.microsoft.com/agileer/2013/05/17/the-correct-way-to-code-a-custom-exception-class/
    /// https://docs.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions
    /// https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions
    /// https://stackoverflow.com/questions/94488/what-is-the-correct-way-to-make-a-custom-net-exception-serializable
    /// https://msdn.microsoft.com/en-us/library/system.security.permissions.securitypermissionattribute(v=vs.110).aspx
    /// </summary>
    [Serializable]
    public class StrategyException : Exception {

        public StrategyException() { }
        public StrategyException(string message) : base(message) { }
        public StrategyException(string message, Exception inner) : base(message, inner) { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected StrategyException(SerializationInfo info, StreamingContext context) :
            base(info, context) {

            // rehydrate class properties
            // this.myBackingField = info.GetString("MyProperty");
            // this.myList<T> = (IList<T>)info.GetValue("MyList", typeof(IList<T>));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {

            if (info == null) {
                throw new ArgumentNullException(@"info");
            }

            // other class properties serialization           
            // info.AddValue("MyProperty", this.MyProperty)

            // special handling may be required for collections
            // info.AddValue("MyListProp", this.MyListProp, typeof(IList<string>));

            base.GetObjectData(info, context);
        }
    }

    /// <summary>
    /// Refer to base class for references.
    /// </summary>
    [Serializable]
    public sealed class StrategyNullReferenceException : StrategyException {

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private StrategyNullReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context) {

            // rehydrate class properties - not on the base class
            // this.myBackingField = info.GetString("MyProperty");
            // this.myList<T> = (IList<T>)info.GetValue("MyList", typeof(IList<T>));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {

            if (info == null) {
                throw new ArgumentNullException("info");
            }

            // Deserialize Only this class properties

            // other class properties serialization           
            // info.AddValue("MyProperty", this.MyProperty)

            // special handling may be required for collections
            // info.AddValue("MyListProp", this.MyListProp, typeof(IList<string>));

            base.GetObjectData(info, context);
        }
    }
}
