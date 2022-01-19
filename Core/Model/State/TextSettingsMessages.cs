using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.State
{
    public class TextSettingsFontNameChangedMessage : MvxMessage
    {
        public TextSettingsFontNameChangedMessage(object sender, string fontName) : base(sender) => FontName = fontName;
        public string FontName { get; }
    }

    public class TextSettingsUseSemiboldChangedMessage : MvxMessage
    {
        public TextSettingsUseSemiboldChangedMessage(object sender, bool useSemibold) : base(sender) => UseSemibold = useSemibold;
        public bool UseSemibold { get; }
    }

    public class TextSettingsPageFontSizeChangedMessage : MvxMessage
    {
        public TextSettingsPageFontSizeChangedMessage(object sender, int pageFontSize) : base(sender) => PageFontSize = pageFontSize;
        public int PageFontSize { get; }
    }

    public class TextSettingsPageLineSpacingChangedMessage : MvxMessage
    {
        public TextSettingsPageLineSpacingChangedMessage(object sender, float pageLineSpacing) : base(sender) => PageLineSpacing = pageLineSpacing;
        public float PageLineSpacing { get; }
    }

    public class TextSettingsPageLineWidthChangedMessage : MvxMessage
    {
        public TextSettingsPageLineWidthChangedMessage(object sender, PageLineWidth pageLineWidth) : base(sender) => PageLineWidth = pageLineWidth;
        public PageLineWidth PageLineWidth { get; }
    }

    public class TextSettingsSpaceFontSizeChangedMessage : MvxMessage
    {
        public TextSettingsSpaceFontSizeChangedMessage(object sender, int spaceFontSize) : base(sender) => SpaceFontSize = spaceFontSize;
        public int SpaceFontSize { get; }
    }

    public class TextSettingsSpaceLineSpacingChangedMessage : MvxMessage
    {
        public TextSettingsSpaceLineSpacingChangedMessage(object sender, float spaceLineSpacing) : base(sender) => SpaceLineSpacing = spaceLineSpacing;
        public float SpaceLineSpacing { get; }
    }

    public class TextSettingsCheckOrthographyChangedMessage : MvxMessage
    {
        public TextSettingsCheckOrthographyChangedMessage(object sender, bool checkOrthography) : base(sender) => CheckOrthography = checkOrthography;
        public bool CheckOrthography { get; }
    }

    public class TextSettingsFetchLinkTitlesChangedMessage : MvxMessage
    {
        public TextSettingsFetchLinkTitlesChangedMessage(object sender, bool fetchLinkTitles) : base(sender) => FetchLinkTitles = fetchLinkTitles;
        public bool FetchLinkTitles { get; }
    }

    public class TextSettingsInsertClosingBracketsChangedMessage : MvxMessage
    {
        public TextSettingsInsertClosingBracketsChangedMessage(object sender, bool insertClosingBrackets) : base(sender) => InsertClosingBrackets = insertClosingBrackets;
        public bool InsertClosingBrackets { get; }
    }
}