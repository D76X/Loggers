using LogXtreme.Ifrastructure.TypeConverters;
using System.ComponentModel;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ResizeObservableCollectionCycleModeEnum {
        None,
        Roll,
        Flush,
        Queue
    }

}
