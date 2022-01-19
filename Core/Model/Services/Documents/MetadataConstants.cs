namespace Core.Model.Services.Documents
{
    public static class MetadataLimits
    {
        public const string Start = "[≡";
        public const int StartLength = 2;

        public const string End = "≡]::";
        public const int EndLength = 4;

        public const int MinLength = 24;  // [≡Type:text|Id:wt8w3vwx]::
    }

    public static class MetadataKeys
    {
        public const string Id = "Id";
        public const string Type = "Type";

        // Document
        public const string Tags = "Tags";
        public const string Style = "Style";
        public const string LastEdited = "LastEdited";

        // Block
        public const string Decks = "Decks";

        // Space
        public const string X = "X";
        public const string Y = "Y";
        public const string Z = "Z";
    }

    public static class MetadataType
    {
        public const string Page = "page";
        public const string Space = "space";
        public const string Text = "text";
        public const string Relation = "relation";
        // embedded
        // card
    }

    public static class MetadataHashes
    {
        // no aeiouy and AEIOUY
        public const string Alphabet = "BCDFGHJKLMNPQRSTVWXZbcdfghjklmnpqrstvwxz";
    }
}
