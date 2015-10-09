using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace INFOIBV
{
    public class MainViewModel : INPC
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;

        public MainViewModel()
        {
            // Initial startup
            HasProgress = Visibility.Hidden;

            // Setup Commands
            LoadImageButton = new RelayCommand(a => LoadImage());
            ApplyButton = new RelayCommand(a => ApplyImage());
            SaveButton = new RelayCommand(a => SaveImage());
        }

        public void LoadImage()
        {
            Console.WriteLine("LOADING A IMAGE!");

            OpenFileDialog openImageDialog = new OpenFileDialog();
            openImageDialog.Filter = "Bitmap files|*.bmp;*.gif;*.png;*.tiff;*.jpeg";
            openImageDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            
            if (openImageDialog.ShowDialog().Value)
            {
                string file = openImageDialog.FileName;                     // Get the filename
                ImagePath = file;                                           // Show filename
                if (InputImage != null) 
                    InputImage.Dispose();                                   // Reset image, clean it up

                InputImage = new Bitmap(file);                              // Create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512)    // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    OldImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(    // Display input image
                                       InputImage.GetHbitmap(),
                                       IntPtr.Zero,
                                       System.Windows.Int32Rect.Empty,
                                       BitmapSizeOptions.FromWidthAndHeight(InputImage.Size.Width, InputImage.Size.Height));
                }              
            }  
            
        }

        public void ApplyImage()
        {
            if (InputImage == null) return;                                 // Get out if no input image
            if (OutputImage != null) 
                OutputImage.Dispose();                                      // Reset output image

            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image
            System.Drawing.Color[,] Image = new System.Drawing.Color[InputImage.Size.Width, InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)

            // Setup progress bar
            HasProgress = Visibility.Visible;
            MaxProgress = InputImage.Size.Width * InputImage.Size.Height;




            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Image[x, y] = InputImage.GetPixel(x, y);                // Set pixel color in array at (x,y)
                }
            }

            //==========================================================================================
            // TODO: include here your own code
            // example: create a negative image
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    System.Drawing.Color pixelColor = Image[x, y];          // Get the pixel color at coordinate (x,y)
                    System.Drawing.Color updatedColor = System.Drawing.Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
                    Image[x, y] = updatedColor;                             // Set the new pixel color at coordinate (x,y)
                    Progress = Progress++;                                  // Increment progress bar
                }
            }
            //==========================================================================================

            // Copy array to output Bitmap
            for (int x = 0; x < InputImage.Size.Width; x++)
                for (int y = 0; y < InputImage.Size.Height; y++)
                    OutputImage.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)

            
            NewImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(    // Display output image
                                OutputImage.GetHbitmap(),
                                IntPtr.Zero,
                                System.Windows.Int32Rect.Empty,
                                BitmapSizeOptions.FromWidthAndHeight(OutputImage.Size.Width, OutputImage.Size.Height));

            HasProgress = Visibility.Hidden;                                // Hide progress bar
        }

        public void SaveImage()
        {
            if (OutputImage == null) 
                return;                                                     // Get out if no output image

            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Filter = "Bitmap file|*.bmp";
            saveImageDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (saveImageDialog.ShowDialog().Value)
                OutputImage.Save(saveImageDialog.FileName);                 // Save the output image
        }


        #region Properties
        private RelayCommand _loadImageButton;
        public RelayCommand LoadImageButton
        {
            get { return _loadImageButton; }
            private set { _loadImageButton = value; }
        }

        private String _imagePath;
        public String ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        private RelayCommand _applyButton;
        public RelayCommand ApplyButton
        {
            get { return _applyButton; }
            private set { _applyButton = value; }
        }

        private Double _progress;
        public Double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }

        private Double _maxProgress;
        public Double MaxProgress
        {
            get { return _maxProgress; }
            set
            {
                _maxProgress = value;
                OnPropertyChanged("MaxProgress");
            }
        }

        private Visibility _hasProgress;
        public Visibility HasProgress
        {
            get { return _hasProgress; }
            set
            {
                _hasProgress = value;
                OnPropertyChanged("HasProgress");
            }
        }

        private RelayCommand _saveButton;
        public RelayCommand SaveButton
        {
            get { return _saveButton; }
            private set { _saveButton = value; }
        }

        private ImageSource _oldImage;
        public ImageSource OldImage
        {
            get { return _oldImage; }
            set
            {
                _oldImage = value;
                OnPropertyChanged("OldImage");
            }
        }

        private ImageSource _newImage;
        public ImageSource NewImage
        {
            get { return _newImage; }
            set
            {
                _newImage = value;
                OnPropertyChanged("NewImage");
            }
        }
        #endregion Properties
    }
}