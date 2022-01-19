using Core.Resources.Fonts;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace Core.Model.State
{
    public enum PageLineWidth
    {
        Compact = 620,
        Spacious = 700,
        Wide = 800
    }

    // TODO: broadcast messages when the properties are set

    public class TextSettings
    {
        public TextSettings()
        {
            
        }

        public List<string> AvaliableFonts
        {
            get
            {
                if (DeviceInfo.Platform == DevicePlatform.UWP)
                    return new() { Fonts.Calibri, Fonts.Cambria, Fonts.Consolas, Fonts.Constantia, Fonts.Corbel, Fonts.CourierNew, Fonts.Georgia };
                else
                    return new() { Fonts.AvenirNext, Fonts.CourierNew, Fonts.Garamond, Fonts.Georgia, Fonts.HelveticaNeue, Fonts.Menlo, Fonts.SFPro };
            }
        }

        public string FontName
        {
            get;
            set;
        } = DeviceInfo.Platform == DevicePlatform.UWP ? Fonts.Corbel : Fonts.AvenirNext;

        // TODO: selectable
        public string MonospaceFontName = DeviceInfo.Platform == DevicePlatform.UWP ? Fonts.Consolas : Fonts.Menlo;

        public bool UseSemibold
        {
            get;
            set;
        } = false;


        #region Pages

        public int PageFontSize
        {
            get;
            set;
        } = 14;

        public float PageLineSpacing
        {
            get;
            set;
        } = 1.4f;

        public PageLineWidth PageLineWidth
        {
            get;
            set;
        } = PageLineWidth.Spacious;
        #endregion


        #region Spaces

        public int SpaceFontSize
        {
            get;
            set;
        } = 14;

        public float SpaceLineSpacing
        {
            get;
            set;
        } = 1.4f;
        #endregion


        #region Editing

        public bool CheckOrthography
        {
            get;
            set;
        } = true;

        public bool FetchLinkTitles
        {
            get;
            set;
        } = true;

        public bool InsertClosingBrackets
        {
            get;
            set;
        } = true;
        #endregion
    }
}
