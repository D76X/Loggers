
using System;
using System.ComponentModel;

namespace LogXtreme.Ifrastructure.TypeConverters {

    /// <summary>
    /// This class implements a TypeConverter that converts a 
    /// String type into  Point type and conversely a Point 
    /// type into a string.
    /// 
    /// This is a sample implemetation that illustrates how a 
    /// TypeConverter may be impleneted by overrides of some
    /// of the methods in teh vase class. Not all the overrides
    /// need implementing at all times.
    /// 
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/ayybcxe5.aspx
    /// 
    /// 1-Define a class that derives from TypeConverter.
    /// 
    /// 2-Override the CanConvertFrom method that specifies 
    /// which type the converter can convert from.This method 
    /// is overloaded.
    ///
    /// 3-Override the ConvertFrom method that implements 
    /// the conversion.This method is overloaded.
    /// 
    /// 4-Override the CanConvertTo method that specifies which 
    /// type the converter can convert to.It is not necessary to 
    /// override this method for conversion to a string type. 
    /// This method is overloaded.
    ///
    /// 5-Override the ConvertTo method that implements the 
    /// conversion. This method is overloaded.
    /// 
    /// 6-Override the IsValid(ITypeDescriptorContext, Object) 
    /// method that performs validation.This method is overloaded.
    /// 
    /// </summary>
    public class PointTypeConverter : TypeConverter {

        public override bool CanConvertFrom(
            ITypeDescriptorContext context, 
            Type sourceType) {

            return base.CanConvertFrom(context, sourceType);
        }
    }
}
