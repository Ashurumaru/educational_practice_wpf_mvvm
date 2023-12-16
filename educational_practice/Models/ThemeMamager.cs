using educational_practice.ViewModels;
using System;
using System.Windows.Media.Imaging;

namespace educational_practice.Models
{
    internal class ThemeMamager
    {
        private BitmapImage defaultImage;
        private BitmapImage currentImage;

        public BitmapImage LoadImage(string filePath)
        {
            try
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(filePath));
                currentImage = bitmapImage;
                return bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox($"Ошибка загрузки изображения: {ex.Message}");
                return defaultImage;
            }
        }

        public BitmapImage LoadDefaultImage()
        {
            try
            {
                string defaultImagePath = "pack://application:,,,/Images/cifrovoe_iskusstvo.png";
                defaultImage = new BitmapImage(new Uri(defaultImagePath));
                return defaultImage;
            }
            catch (Exception ex)
            {
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox($"Ошибка загрузки изображения: {ex.Message}");
                return null;
            }
        }

        public BitmapImage GetImage()
        {
            return currentImage;
        }

        public BitmapImage GetWhiteImage()
        {
            string whiteImagePath = "pack://application:,,,/Images/white.jpg";
            currentImage = new BitmapImage(new Uri(whiteImagePath));
            return currentImage;
        }

        public BitmapImage GetDarkImage()
        {
            string darkImagePath = "pack://application:,,,/Images/dark.png";
            currentImage = new BitmapImage(new Uri(darkImagePath));
            return currentImage;
        }
    }
}
