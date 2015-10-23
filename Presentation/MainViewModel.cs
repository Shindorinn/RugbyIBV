using INFOIBV.Filters;
using INFOIBV.Utilities;

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace INFOIBV.Presentation
{
    public class MainViewModel : INPC
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;

        private FilterSelectorWindow fsWindow;
        private IApplicableFilter decoratedFilter;

        public MainViewModel()
        {
            // Initial startup
            HasProgress = Visibility.Hidden;
            IsBusy = false;

            // Setup Commands
            LoadImageButton = new RelayCommand(a => LoadImage(), e => IsNotBusy());
            SelectFiltersButton = new RelayCommand(a => SelectFilters(), e => IsNotBusy());
            ApplyButton = new RelayCommand(a => ApplyImage(), e => IsNotBusy());
            SaveButton = new RelayCommand(a => SaveImage(), e => IsNotBusy());

            // Setup for FilterSelectorWindow with ViewModel
            fsWindow = new FilterSelectorWindow() { DataContext = new FilterSelectorViewModel() };
            decoratedFilter = null;
        }

        public void LoadImage()
        {
            IsBusy = true;
            OpenFileDialog openImageDialog = new OpenFileDialog();
            openImageDialog.Filter = "Bitmap files|*.bmp;*.gif;*.png;*.tiff;*.jpg;*.jpeg";
            openImageDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (openImageDialog.ShowDialog().Value)
            {
                string file = openImageDialog.FileName; // Get the filename
                ImagePath = file; // Show filename
                if (InputImage != null)
                    InputImage.Dispose(); // Reset image, clean it up

                InputImage = new Bitmap(file); // Create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512)    // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    OldImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap( // Display input image
                                       InputImage.GetHbitmap(),
                                       IntPtr.Zero,
                                       System.Windows.Int32Rect.Empty,
                                       BitmapSizeOptions.FromWidthAndHeight(InputImage.Size.Width, InputImage.Size.Height));
                }
            }
            IsBusy = false;
        }

        public void SelectFilters()
        {
            IsBusy = true;
            fsWindow.ShowDialog();
            decoratedFilter = FilterFactory.Construct(((FilterSelectorViewModel)fsWindow.DataContext).ActiveFilters.ToList());
            IsBusy = false;

            // Debug ?
            Console.WriteLine("The following filters have been selected: ");
            foreach (var item in ((FilterSelectorViewModel)fsWindow.DataContext).ActiveFilters)
            {
                Console.WriteLine("- {0}", item);
            }
        }

        public void ApplyImage()
        {
            if (InputImage == null || decoratedFilter == null) return; // Get out if no input image or filter selected
            if (OutputImage != null)
            {
                OutputImage.Dispose(); // Reset output image
                Progress = 0; // Reset progress
            }

            IsBusy = true;
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height);
            System.Drawing.Color[,] InputColors = new System.Drawing.Color[InputImage.Size.Width, InputImage.Size.Height];
            System.Drawing.Color[,] OutputColors;
            for (int x = 0; x < InputImage.Size.Width; x++)
                for (int y = 0; y < InputImage.Size.Height; y++)
                    InputColors[x, y] = InputImage.GetPixel(x, y);

            HasProgress = Visibility.Visible;
            MaxProgress = decoratedFilter.GetMaximumProgress(InputImage.Size.Width, InputImage.Size.Height);

            ThreadPool.QueueUserWorkItem(o =>
            {
                OutputColors = decoratedFilter.apply(InputColors, this);

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    for (int x = 0; x < InputImage.Size.Width; x++)
                        for (int y = 0; y < InputImage.Size.Height; y++)
                            OutputImage.SetPixel(x, y, OutputColors[x, y]);

                    NewImage = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap( // Display output image
                                        OutputImage.GetHbitmap(),
                                        IntPtr.Zero,
                                        System.Windows.Int32Rect.Empty,
                                        BitmapSizeOptions.FromWidthAndHeight(OutputImage.Size.Width, OutputImage.Size.Height));

                    HasProgress = Visibility.Hidden;
                    IsBusy = false;
                }));

            });
        }

        public void SaveImage()
        {
            if (OutputImage == null)
                return; // Get out if no output image

            SaveFileDialog saveImageDialog = new SaveFileDialog();
            saveImageDialog.Filter = "Bitmap file|*.bmp";
            saveImageDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            if (saveImageDialog.ShowDialog().Value)
                OutputImage.Save(saveImageDialog.FileName); // Save the output image
        }

        public Boolean IsNotBusy() // Necessary for buttons to go offline while work has to be done.
        {
            return !IsBusy;
        }

        #region Properties
        private Boolean _isBusy;
        public Boolean IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                CommandManager.InvalidateRequerySuggested(); // Makes every RelayCommand to be re-evaluated (a good thing!)
            }
        }

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
        private RelayCommand _selectFiltersButton;
        public RelayCommand SelectFiltersButton
        {
            get { return _selectFiltersButton; }
            private set { _selectFiltersButton = value; }
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