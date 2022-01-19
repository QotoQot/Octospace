using AppKit;
using Core.Logging;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using System;
using System.Reflection;

namespace Mac.MvvmCross.Bindings
{
    public class NSComboBoxStringValueTargetBinding : MvxPropertyInfoTargetBinding<NSComboBox>
    {
        public const string PropertyName = "StringValue";

        public NSComboBoxStringValueTargetBinding(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo) { }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public override void SubscribeToEvents()
        {
            if (View is NSComboBox comboBox)
            {
                comboBox.EditingEnded += ComboBox_EditingEnded;
                comboBox.SelectionChanged += ComboBox_SelectionChanged;
            }
            else
                MvxBindingLog.Error($"{TargetType} is null in {GetType()}");
        }

        void ComboBox_EditingEnded(object sender, EventArgs e)
        {
            if (View is NSComboBox comboBox)
                FireValueChanged(comboBox.StringValue);
        }

        void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            if (View is NSComboBox comboBox)
                FireValueChanged(comboBox.SelectedValue);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                if (View is NSComboBox comboBox)
                {
                    comboBox.EditingEnded -= ComboBox_EditingEnded;
                    comboBox.SelectionChanged -= ComboBox_SelectionChanged;
                }
            }
        }
    }

    public static class NSComboBoxStringValueTargetBindingExtensions
    {
        public static string BindStringValue(this NSComboBox comboBox)
            => NSComboBoxStringValueTargetBinding.PropertyName;
    }
}
