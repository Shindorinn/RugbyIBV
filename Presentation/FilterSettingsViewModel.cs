using INFOIBV.Filters;
using INFOIBV.Utilities;
using INFOIBV.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace INFOIBV.Presentation
{
    public class FilterSettingsViewModel : INPC
    {
        public FilterSettingsViewModel()
        {
            // Do something, initialize or something

            // Initialize CompassTypeList, SelectedCompassType & OperationTypeList
            CompassTypeList = new List<string>();
            foreach (CompassType compass in Enum.GetValues(typeof(CompassType)))
                CompassTypeList.Add(compass.ToString());
            SelectedCompassType = CompassType.Sobel.ToString();
            OperationTypeList = new List<string>();
            OperationTypeList.Add(""); // For if the user does not want it
            foreach (OperationType operation in Enum.GetValues(typeof(OperationType)))
                OperationTypeList.Add(operation.ToString());

            //Initialize Dilations & Erosions
            FirstNumDilations = "0";
            FirstNumErosions = "0";
            SecondNumDilations = "0";
            SecondNumErosions = "0";
            ThirdNumDilations = "0";
            ThirdNumErosions = "0";
            FourthNumDilations = "0";
            FourthNumErosions = "0";
        }

        //Might be handy (to recommend a value or something)
        public void SetThresholdLevel(int threshold)
        {
            ThresholdValue = threshold;
        }

        #region Properties
        private int _thresholdValue;
        public int ThresholdValue
        {
            get { return _thresholdValue; }
            set
            {
                value = value > 255 ? 255 : value;
                _thresholdValue = value;
                OnPropertyChanged("ThresholdValue");
            }
        }

        private List<string> _compassTypeList;
        public List<string> CompassTypeList
        {
            get { return _compassTypeList; }
            set
            {
                _compassTypeList = value;
                OnPropertyChanged("CompassTypeList");
            }
        }

        private string _selectedCompassType;
        public string SelectedCompassType
        {
            get { return _selectedCompassType; }
            set
            {
                _selectedCompassType = value;
                OnPropertyChanged("SelectedCompassType");
            }
        }

        private List<string> _operationTypeList;
        public List<string> OperationTypeList
        {
            get { return _operationTypeList; }
            set
            {
                _operationTypeList = value;
                OnPropertyChanged("OperationTypeList");
            }
        }
        // - First Round Selection
        private string _selectedFirstOperationType;
        public string SelectedFirstOperationType
        {
            get { return _selectedFirstOperationType; }
            set
            {
                _selectedFirstOperationType = value;
                OnPropertyChanged("SelectedFirstOperationType");
            }
        }

        private string _firstNumDilations;
        public string FirstNumDilations
        {
            get { return _firstNumDilations; }
            set
            {
                _firstNumDilations = value;
                OnPropertyChanged("FirstNumDilations");
            }
        }

        private string _firstNumErosions;
        public string FirstNumErosions
        {
            get { return _firstNumErosions; }
            set
            {
                _firstNumErosions = value;
                OnPropertyChanged("FirstNumErosions");
            }
        }
        // - Second Round Selection
        private string _selectedSecondOperationType;
        public string SelectedSecondOperationType
        {
            get { return _selectedSecondOperationType; }
            set
            {
                _selectedSecondOperationType = value;
                OnPropertyChanged("SelectedSecondOperationType");
            }
        }

        private string _SecondNumDilations;
        public string SecondNumDilations
        {
            get { return _SecondNumDilations; }
            set
            {
                _SecondNumDilations = value;
                OnPropertyChanged("SecondNumDilations");
            }
        }

        private string _SecondNumErosions;
        public string SecondNumErosions
        {
            get { return _SecondNumErosions; }
            set
            {
                _SecondNumErosions = value;
                OnPropertyChanged("SecondNumErosions");
            }
        }
        // - Third Round Selection
        private string _selectedThirdOperationType;
        public string SelectedThirdOperationType
        {
            get { return _selectedThirdOperationType; }
            set
            {
                _selectedThirdOperationType = value;
                OnPropertyChanged("SelectedThirdOperationType");
            }
        }

        private string _ThirdNumDilations;
        public string ThirdNumDilations
        {
            get { return _ThirdNumDilations; }
            set
            {
                _ThirdNumDilations = value;
                OnPropertyChanged("ThirdNumDilations");
            }
        }

        private string _ThirdNumErosions;
        public string ThirdNumErosions
        {
            get { return _ThirdNumErosions; }
            set
            {
                _ThirdNumErosions = value;
                OnPropertyChanged("ThirdNumErosions");
            }
        }
        // - Fourth Round Selection
        private string _selectedFourthOperationType;
        public string SelectedFourthOperationType
        {
            get { return _selectedFourthOperationType; }
            set
            {
                _selectedFourthOperationType = value;
                OnPropertyChanged("SelectedFourthOperationType");
            }
        }

        private string _FourthNumDilations;
        public string FourthNumDilations
        {
            get { return _FourthNumDilations; }
            set
            {
                _FourthNumDilations = value;
                OnPropertyChanged("FourthNumDilations");
            }
        }

        private string _FourthNumErosions;
        public string FourthNumErosions
        {
            get { return _FourthNumErosions; }
            set
            {
                _FourthNumErosions = value;
                OnPropertyChanged("FourthNumErosions");
            }
        }
        #endregion
    }
}