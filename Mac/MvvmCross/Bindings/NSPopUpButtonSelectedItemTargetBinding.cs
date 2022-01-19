using AppKit;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using System;
using System.Reflection;

namespace Mac.MvvmCross.Bindings
{
    public class NSPopUpButtonIndexOfSelectedItemTargetBinding : MvxPropertyInfoTargetBinding<NSPopUpButton>
    {
        public const string PropertyName = "IndexOfSelectedItem";

        public NSPopUpButtonIndexOfSelectedItemTargetBinding(object target, PropertyInfo targetPropertyInfo)
            : base(target, targetPropertyInfo) { }

        public override MvxBindingMode DefaultMode => MvxBindingMode.TwoWay;

        public override void SubscribeToEvents()
        {
            if (View is NSPopUpButton popupButton)
                popupButton.Activated += PopupButton_ValueChanged;
            else
                MvxBindingLog.Error($"{TargetType} is null in {GetType()}");
        }

        protected override void SetValueImpl(object target, object value)
        {
            if (target is NSPopUpButton popupButton)
                popupButton.SelectItem((int)value);
        }

        void PopupButton_ValueChanged(object sender, EventArgs e)
        {
            if (View is NSPopUpButton popupButton)
                FireValueChanged((int)popupButton.IndexOfSelectedItem);
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                if (View is NSPopUpButton popupButton)
                    popupButton.Activated -= PopupButton_ValueChanged;
            }
        }
    }

    public static class NSPopUpButtonIndexOfSelectedItemTargetBindingExtensions
    {
        public static string BindIndexOfSelectedItem(this NSPopUpButton popupButton)
            => NSPopUpButtonIndexOfSelectedItemTargetBinding.PropertyName;
    }
}
