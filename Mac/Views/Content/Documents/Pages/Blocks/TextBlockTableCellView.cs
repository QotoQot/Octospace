using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Mac.MvvmCross.Views;
using System.Drawing;
using Core.ViewModels.Content.Documents;
using Core.Logging;
using CoreGraphics;
using System.Text.RegularExpressions;
using Mac.Views.Content.Documents.Blocks;
using CoreText;
using Core.Model.Services.Documents.Markdown;
using Core.Basics.Utils;

// https://michelf.ca/blog/2019/font-substitution-missing-text/

// workaround: textView.PerformSelector(new Selector("someSelector:");)

//!missing-protocol-member! NSTextViewDelegate::textView:clickedOnCell:inRect: not found
//!missing-protocol-member! NSTextViewDelegate::textView:clickedOnLink: not found
//!missing-protocol-member! NSTextViewDelegate::textView:doubleClickedOnCell:inRect: not found
//!missing-protocol-member! NSTextViewDelegate::textView:draggedCell:inRect:event:atIndex: not found
//!missing-protocol-member! NSTextViewDelegate::textView:URLForContentsOfTextAttachment:atIndex: not found
//!missing-protocol-member! NSTextViewDelegate::textView:willShowSharingServicePicker:forItems: not found

//!missing-selector! NSTextContainer::initWithContainerSize: not bound
//!missing-selector! NSTextContainer::initWithSize: not bound
//!missing-selector! NSTextContainer::lineFragmentRectForProposedRect:sweepDirection: movementDirection: remainingRect: not bound

//!missing-selector! NSTextStorage::attributeRuns not bound
//!missing-selector! NSTextStorage::characters not bound
//!missing-selector! NSTextStorage::font not bound
//!missing-selector! NSTextStorage::foregroundColor not bound
//!missing-selector! NSTextStorage::paragraphs not bound
//!missing-selector! NSTextStorage::setAttributeRuns: not bound
//!missing-selector! NSTextStorage::setCharacters: not bound
//!missing-selector! NSTextStorage::setFont: not bound
//!missing-selector! NSTextStorage::setForegroundColor: not bound
//!missing-selector! NSTextStorage::setParagraphs: not bound
//!missing-selector! NSTextStorage::setWords: not bound
//!missing-selector! NSTextStorage::words not bound

namespace Mac.Views.Content.Documents.Pages
{
    public partial class TextBlockTableCellView : BaseBlockTableCellView<TextBlockViewModel>,
        INSTextViewDelegate
    {
        public static readonly NSNib Nib;
        public static readonly string SharedId = nameof(TextBlockTableCellView);
        static TextBlockTableCellView() => Nib = new NSNib(SharedId, NSBundle.MainBundle);

        public static readonly int LeftPadding = 20;

        [Export("initWithCoder:")]
        public TextBlockTableCellView(NSCoder coder) : base(coder) { }
        public TextBlockTableCellView(IntPtr handle) : base(handle) { }

        public override NSView ContentView => TextView;
        public override CGSize ContentSize => TextView.Bounds.Size;
        public override CGPoint ContentFocus => FindCursorRect().Location;
        //public override NSView ContentView => this;

        public event EventHandler<nfloat>? TestChanged;

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            //Layer.BackgroundColor = new CGColor(1, 0.7f, 1, 1);

            // TODO: content theme
            // TODO: shared font object (from global or page setting)
            //TextView.Font = NSFont.SystemFontOfSize(15);

            TextView.BackgroundColor = NSColor.Clear;
            TextView.FocusRingType = NSFocusRingType.None;

            //TextView.Selectable = true;
            //TextView.Editable = true;
            //TextView.AllowsUndo = true;
            //TextView.FieldEditor = false;

            //TextView.RichText = true;
            //TextView.AllowsDocumentBackgroundColorChange = true; // ???
            //TextView.DrawsBackground = false;

            //TextView.LayoutManager.AllowsNonContiguousLayout = true;

            //TextView.ImportsGraphics = false;
            //TextView.AllowsImageEditing = false;

            //TextView.UsesFontPanel = false;
            //TextView.UsesRuler = false;
            //TextView.UsesInspectorBar = false;

            //TextView.UsesFindBar = false;
            //TextView.UsesFindPanel = false;
            //TextView.IsIncrementalSearchingEnabled = false;


            // TODO: get from the content theme
            //TextView.TextColor = NSColor.Black;
            //TextView.InsertionPointColor = NSColor.Blue;

            // TODO: get from settings / menu bar
            //TextView.ContinuousSpellCheckingEnabled = false;
            //TextView.AutomaticSpellingCorrectionEnabled = false;
            //TextView.GrammarCheckingEnabled = false;

            // TODO: get from settings / menu bar
            //TextView.AutomaticDataDetectionEnabled = false; // dates, addresses, and phone numbers
            //TextView.AutomaticDashSubstitutionEnabled = false;
            //TextView.AutomaticQuoteSubstitutionEnabled = false;
            //TextView.AutomaticTextCompletionEnabled = true;

            // non-configurable for now
            //TextView.AutomaticLinkDetectionEnabled = false;
            //TextView.AutomaticTextReplacementEnabled = true; // user system-wide snippets
            //TextView.SmartInsertDeleteEnabled = false;
            //TextView.AllowsCharacterPickerTouchBarItem = true;

            TextView.DoCommandBySelector = DoCommandBySelector;
            //TextView.SetContentHuggingPriorityForOrientation(200, NSLayoutConstraintOrientation.Vertical);

            TextView.HeightChanged += TextView_HeightChanged;
            TextView.DidBecomeFirstResponder += TextView_DidBecomeFirstResponder;

            TextView.TextDidChange += TextView_TextDidChange;
        }
        
        void TextView_TextDidChange(object sender, EventArgs e)
        {
            Dlog.Info(TextView.String);
            ViewModel?.UpdateText(TextView.String);

            if (ViewModel?.GetMarkdownReplacement() is List<KeyValuePair<XRange, MarkdownReplacement>> formatting)
            {
                foreach (var item in formatting)
                {
                    if (item.Value is MarkdownReplacementFormatting markdownFormatting &&
                        markdownFormatting.Formatting is NSStringAttributes attributes)
                        TextView.TextStorage.AddAttributes(attributes, new NSRange(item.Key.Start, item.Key.Length));
                }
            }


            //ViewModel?.UpdateText(TextView.String);

            //var storage = TextView.TextStorage;
            //storage.BeginEditing();

            //var emptyAttributes = new CoreText.CTStringAttributes();
            //var fullRange = new NSRange(0, storage.Length);
            //storage.SetAttributes(emptyAttributes, fullRange);


            ////var s = new NSStringAttributes();

            //storage.EndEditing();
        }

        public override CGRect Frame
        {
            get => base.Frame;
            set
            {
                base.Frame = value;
            }
        }

        public string TempText => TextView.String;

        public void TempSetText(string text)
        {
            TextView.TextStorage.InvalidateAttributes(new NSRange(0, TextView.String.Length));
            TextView.Value = text;
            //TestMarkup();

            //TextView.UpdateHeight();

            // Cursor:
            // TextView.SelectedRange.Location


            //TextView.TextStorage.SetString(new NSAttributedString(text));
            //TextView.BackgroundColor = NSColor.Clear;


            // try spelling-and-grammar setting

            // https://stackoverflow.com/questions/25590912/how-to-implement-undo-with-nstextview

            // kerning & ligatures:
            // https://developer.apple.com/library/archive/documentation/Cocoa/Conceptual/TextUILayer/Tasks/SetTextAttributes.html#//apple_ref/doc/uid/20000936-85009
        }

        void TestMarkup()
        {
            // watch out for `NSTextStorage` issues for syntax highlighting:
            // https://stackoverflow.com/questions/5323830/modifying-nstextstorage-attributes-causes-scroll-view-to-jump-around/8697502#8697502

            var font = NSFont.SystemFontOfSize(12, NSFontWeight.Bold);
            var attributes = new NSStringAttributes { Font = font };

            var matches = Regex.Matches(TextView.Value, @"#\w*");
            foreach (Match item in matches)
            {
                var range = new NSRange(item.Index, item.Length);
                TextView.TextStorage.SetAttributes(attributes.Dictionary, range);
            }
        }


        #region Events

        void TextView_DidBecomeFirstResponder(object sender, bool isFirstResponder)
        {
            if (isFirstResponder)
                ViewModel?.Messenger.Publish(new PageManipulationRequestMessage(this, PageManipulation.DeselectAll));
        }

        void TextView_HeightChanged(object sender, nfloat newHeight)
        {
            TestChanged?.Invoke(this, newHeight);
            //Messenger?.Publish(...);
        }
        #endregion


        #region Cursor Manipulation

        public void MoveCursorToBeginning() => TextView.SelectedRange = new NSRange(0, 0);

        public void MoveCursorToEnd() => TextView.SelectedRange = new NSRange(TextView.String.Length, 0);

        public override void FocusContentAt(CGPoint point)
        {
            Window.MakeFirstResponder(TextView);
            var index = TextView.CharacterIndex(point);
            TextView.SelectedRange = new NSRange((nint)index, 0);
        }

        bool DoCommandBySelector(NSTextView textView, ObjCRuntime.Selector commandSelector)
        {
            if (commandSelector.Name == "moveUp:" &&
                FindCursorRect().Y == 0)
            {
                ViewModel?.Messenger.Publish(new PageManipulationRequestMessage(this, PageManipulation.GoToBlockAbove));
                return true;
            }
            else if (commandSelector.Name == "moveDown:" &&
                     FindCursorRect() is CGRect cursorRect &&
                     cursorRect.Y == textView.Bounds.Height - cursorRect.Height)
            {
                ViewModel?.Messenger.Publish(new PageManipulationRequestMessage(this, PageManipulation.GoToBlockBelow));
                return true;
            }
            else
                return false;
        }

        CGRect FindCursorRect()
        {
            var cursorLocation = new NSRange(TextView.SelectedRange.Location, 0);
            return TextView.LayoutManager.BoundingRectForGlyphRange(cursorLocation, TextView.TextContainer);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TextView.HeightChanged -= TextView_HeightChanged;
                TextView.DidBecomeFirstResponder -= TextView_DidBecomeFirstResponder;
            }

            base.Dispose(disposing);
        }
    }
}
