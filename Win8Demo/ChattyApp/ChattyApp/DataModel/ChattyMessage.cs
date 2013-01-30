using System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ChattyApp.Data
{
    public class ChattyMessage : ChattyApp.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public ChattyMessage(String title, String text)
        {
            this._title = title;
            this._text = text;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        private ImageSource _image = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null)
                {
                    this._image = new BitmapImage(new Uri(ChattyMessage._baseUri, "Assets/LightGray.png"));
                }
                return this._image;
            }
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}