using System;
using System.Windows.Markup;

namespace LogXtreme.WinDsk.Infrastructure.MarkupExtensions {

    /// <summary>
    /// Refs
    /// http://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    /// https://app.pluralsight.com/player?course=wpf-productivity-playbook&author=brian-noyes&name=wpf-productivity-playbook-m5&clip=3&mode=live
    /// </summary>
    public class EnumBindingSourceExtension :
        MarkupExtension {

        private Type enumType;

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType) {
            this.EnumType = enumType;
        }

        public Type EnumType {

            get => this.enumType;

            set {

                if (value != this.enumType) {

                    if (value != null) {

                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        if (!enumType.IsEnum) {
                            throw new ArgumentException("Type must be for an Enum.");
                        }

                        this.enumType = value;
                    }
                }

            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {

            if (this.enumType == null) {
                throw new InvalidOperationException("The EnumType must be specified.");
            }

            Type actualEnumType = Nullable.GetUnderlyingType(this.enumType) ?? this.enumType;

            Array enumValues = Enum.GetValues(actualEnumType);

            if(actualEnumType == this.enumType) {
                return enumValues;
            }

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
