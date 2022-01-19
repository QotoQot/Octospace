using AppKit;
using System;

namespace Mac
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable IDE0059 // Unnecessary assignment of a value

    [Foundation.Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public void Include(NSView view)
        {
            view.Hidden = true;
        }

        public void Include(NSButton button)
        {
            button.State = NSCellStateValue.On;
            button.Activated += (sender, args) => { button.Title = ""; };
        }

        public void Include(NSPopUpButton popUpButton)
        {
            popUpButton.SelectItem(0);
            popUpButton.SelectItemWithTag(0);
            popUpButton.Activated += (sender, args) => { popUpButton.Title = ""; };
            popUpButton.AddItem("");
            popUpButton.AddItems(new string[] { "" });
        }

        public void Include(NSComboBox comboBox)
        {
            comboBox.SelectItem(0);
            comboBox.Activated += (sender, args) => { comboBox.DoubleValue = 1.0; };
            comboBox.Changed += (sender, args) => { comboBox.IntValue = 1; };
            comboBox.EditingBegan += (sender, args) => { comboBox.FloatValue = 1.0f; };
            comboBox.EditingEnded += (sender, args) => { comboBox.StringValue = ""; };
            comboBox.SelectionIsChanging += (sender, args) => { comboBox.StringValue = ""; };
            comboBox.SelectionChanged += (sender, args) => { comboBox.StringValue = ""; };
            comboBox.WillPopUp += (sender, args) => { comboBox.StringValue = ""; };
            comboBox.WillDismiss += (sender, args) => { comboBox.StringValue = ""; };
        }

        public void Include(NSTextField textField)
        {
            textField.Activated += (sender, args) => { textField.IntValue = 1; };
            textField.Changed += (sender, args) => { textField.FloatValue = 1.0f; };
            textField.EditingBegan += (sender, args) => { textField.DoubleValue = 1.0; };
            textField.EditingEnded += (sender, args) => { textField.StringValue = ""; };
            textField.DidFailToValidatePartialString += (sender, args) => { textField.StringValue = ""; };
        }

        public void Include(NSTextView textView)
        {
            textView.DidChangeSelection += (sender, args) => { textView.Value = ""; };
            textView.DidChangeTypingAttributes += (sender, args) => { textView.Value = ""; };
            textView.TextDidBeginEditing += (sender, args) => { textView.Value = ""; };
            textView.TextDidChange += (sender, args) => { textView.Value = ""; };
            textView.TextDidEndEditing += (sender, args) => { textView.Value = ""; };
        }

        public void Include(NSSlider slider)
        {
            slider.DoubleValue = 1.0;
            slider.FloatValue = 1.0f;
            slider.Activated += (sender, args) => { slider.IntValue = 1; };
        }

        public void Include(NSDatePicker datePicker)
        {
            datePicker.Activated += (sender, args) => { datePicker.DateValue = new Foundation.NSDate(); };
        }

        public void Include(NSMenuItem menuItem)
        {
            menuItem.Activated += (sender, args) => { menuItem.State = NSCellStateValue.On; };
        }

        public void Include(NSSearchField searchField)
        {
            searchField.Action = null;
            searchField.Activated += (sender, args) => { searchField.StringValue = ""; };
            searchField.Changed += (sender, args) => { searchField.StringValue = ""; };
            searchField.DidFailToValidatePartialString += (sender, args) => { searchField.StringValue = ""; };
            searchField.EditingBegan += (sender, args) => { searchField.StringValue = ""; };
            searchField.EditingEnded += (sender, args) => { searchField.StringValue = ""; };
            searchField.SearchingStarted += (sender, args) => { searchField.StringValue = ""; };
            searchField.SearchingEnded += (sender, args) => { searchField.StringValue = ""; };
        }


        public void Include(NSSegmentedControl segmentedControl)
        {
            segmentedControl.Activated += (sender, args) => { segmentedControl.SelectSegment(0); };
        }

        public void Include(NSTabViewController tabViewController)
        {
            tabViewController.SelectedTabViewItemIndex = 0;
        }

        //public void Include(MvxTaskBasedBindingContext c)
        //{
        //    c.Dispose();
        //    var c2 = new MvxTaskBasedBindingContext();
        //    c2.Dispose();
        //}

        //public void Include(UIButton uiButton)
        //{
        //    uiButton.TouchUpInside += (s, e) =>
        //                              uiButton.SetTitle(uiButton.Title(UIControlState.Normal), UIControlState.Normal);
        //}

        //public void Include(UIBarButtonItem barButton)
        //{
        //    barButton.Clicked += (s, e) =>
        //                         barButton.Title = barButton.Title + "";
        //}

        //public void Include(UITextField textField)
        //{
        //    textField.Text = textField.Text + "";
        //    textField.EditingChanged += (sender, args) => { textField.Text = ""; };
        //    textField.EditingDidEnd += (sender, args) => { textField.Text = ""; };
        //}

        //public void Include(UITextView textView)
        //{
        //    textView.Text = textView.Text + "";
        //    textView.TextStorage.DidProcessEditing += (sender, e) => textView.Text = "";
        //    textView.Changed += (sender, args) => { textView.Text = ""; };
        //}

        //public void Include(UILabel label)
        //{
        //    label.Text = label.Text + "";

        //    label.AttributedText = new NSAttributedString(label.AttributedText.ToString() + "");
        //}

        //public void Include(UIImageView imageView)
        //{
        //    imageView.Image = new UIImage(imageView.Image.CGImage);
        //}

        //public void Include(UIDatePicker date)
        //{
        //    date.Date = date.Date.AddSeconds(1);
        //    date.ValueChanged += (sender, args) => { date.Date = NSDate.DistantFuture; };
        //}

        //public void Include(UISlider slider)
        //{
        //    slider.Value = slider.Value + 1;
        //    slider.ValueChanged += (sender, args) => { slider.Value = 1; };
        //}

        //public void Include(UIProgressView progress)
        //{
        //    progress.Progress = progress.Progress + 1;
        //}

        //public void Include(UISwitch sw)
        //{
        //    sw.On = !sw.On;
        //    sw.ValueChanged += (sender, args) => { sw.On = false; };
        //}

        //public void Include(MvxViewController vc)
        //{
        //    vc.Title = vc.Title + "";
        //}

        //public void Include(UIStepper s)
        //{
        //    s.Value = s.Value + 1;
        //    s.ValueChanged += (sender, args) => { s.Value = 0; };
        //}

        //public void Include(UIPageControl s)
        //{
        //    s.Pages = s.Pages + 1;
        //    s.ValueChanged += (sender, args) => { s.Pages = 0; };
        //}

        //public void Include(INotifyCollectionChanged changed)
        //{
        //    changed.CollectionChanged += (s, e) => { var test = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}"; };
        //}

        //public void Include(ICommand command)
        //{
        //    command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        //}

        //public void Include(MvxPropertyInjector injector)
        //{
        //    injector = new MvxPropertyInjector();
        //}

        //public void Include(System.ComponentModel.INotifyPropertyChanged changed)
        //{
        //    changed.PropertyChanged += (sender, e) => { var test = e.PropertyName; };
        //}

        //public void Include(MvxNavigationService service, IMvxViewModelLoader loader)
        //{
        //    service = new MvxNavigationService(loader, null, null);
        //}

        //public void Include(UIImagePickerController uIImagePickerController)
        //{
        //    var x = uIImagePickerController.SourceType;
        //    uIImagePickerController.FinishedPickingMedia += (s, e) => { };
        //    uIImagePickerController.FinishedPickingImage += (s, e) => { };
        //    uIImagePickerController.Canceled += (s, e) => { };
        //}

        //public void Include(ConsoleColor color)
        //{
        //    Console.Write("");
        //    Console.WriteLine("");
        //    color = Console.ForegroundColor;
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    Console.ForegroundColor = ConsoleColor.Magenta;
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //    Console.ForegroundColor = ConsoleColor.DarkGray;
        //}
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore IDE0059 // Unnecessary assignment of a value
}
