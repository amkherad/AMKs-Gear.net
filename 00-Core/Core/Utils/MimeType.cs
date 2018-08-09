using System;

namespace AMKsGear.Core.Utils
{
    /// <summary>
    /// Multipurpose Internet Mail Extensions wrapper type.
    /// </summary>
    public struct MimeType : IEquatable<MimeType>
    {
        public string Media { get; }
        public string Type { get; }

        
        public MimeType(string media, string type)
        {
            Media = media;
            Type = type;
        }
        
        public override string ToString() => $"{Media}/{Type}";
        

//        /// <summary>
//        /// Parse a string in XX/XX format to a <see cref="MimeType"/>.
//        /// </summary>
//        /// <param name="mimeString">A string in XX/XX format.</param>
//        /// <returns>A <see cref="MimeType"/>.</returns>
//        /// <exception cref="ArgumentNullException"></exception>
//        /// <exception cref="FormatException">If no slash found or either parts being empty a <see cref="FormatException"/> will be thrown.</exception>
//        public static MimeType Parse(string mimeString)
//        {
//            if (mimeString == null) throw new ArgumentNullException(nameof(mimeString));
//
//            var slash = mimeString.IndexOf('/');
//            if (slash <= 0 || slash == mimeString.Length - 1)
//            {
//                throw new FormatException();
//            }
//            
//            var span = mimeString.AsSpan();
//
//            var media = span.Slice(0, slash);
//            var type = span.Slice(slash + 1);
//            
//            return new MimeType(media.ToString(), type.ToString());
//        }
//
//        /// <summary>
//        /// Tries to parse a string in XX/XX format to a <see cref="MimeType"/>.
//        /// </summary>
//        /// <param name="mimeString"></param>
//        /// <param name="mimeType"></param>
//        /// <returns>A boolean indicating the parse is successful or not.</returns>
//        public static bool TryParse(string mimeString, out MimeType mimeType)
//        {
//            if (mimeString == null)
//            {
//                mimeType = default;
//                return false;
//            }
//
//            var slash = mimeString.IndexOf('/');
//            if (slash <= 0 || slash == mimeString.Length - 1)
//            {
//                mimeType = default;
//                return false;
//            }
//            
//            var span = mimeString.AsSpan();
//
//            var media = span.Slice(0, slash);
//            var type = span.Slice(slash + 1);
//            
//            mimeType = new MimeType(media.ToString(), type.ToString());
//            return true;
//        }
        
        public bool Equals(MimeType other)
        {
            return string.Equals(Media, other.Media) && string.Equals(Type, other.Type);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is MimeType && Equals((MimeType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                //ReSharper generated code!
                return ((Media != null ? Media.GetHashCode() : 0) * 397) ^ (Type != null ? Type.GetHashCode() : 0);
            }
        }
    }
}