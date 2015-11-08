using INFOIBV.Filters;
using INFOIBV.Utilities;
using INFOIBV.Utilities.Enums;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace INFOIBV.Presentation
{
    public class FilterSelectorViewModel : INPC
    {
        public FilterSelectorViewModel()
        {
            // Initialization - Lists
            InactiveFilters = new ObservableCollection<FilterType>(FilterTypes.GetAllFilters());
            ActiveFilters = new ObservableCollection<FilterType>();

            ActiveFilters.Add(new FilterType(FilterType.Type.GrayscaleFilter, "Grayscale"));
            ActiveFilters.Add(new FilterType(FilterType.Type.SobelCompassOperation, "Sobel Compass Operation"));
            ActiveFilters.Add(new FilterType(FilterType.Type.ThresholdFilter160, "Threshold 160"));
            ActiveFilters.Add(new FilterType(FilterType.Type.InvertFilter, "Invert Filter"));
            ActiveFilters.Add(new FilterType(FilterType.Type.ClearBorderFilter, "Clear Border"));

            // Initialization - Lists Selectors
            SelectedInactiveFilter = -1;
            SelectedActiveFilter = -1;

            // Initialization - Buttons/Commands
            AddToActiveFilterCommand = new RelayCommand(a => AddToActiveFilter());
            RemoveFromActiveFilterCommand = new RelayCommand(a => RemoveFromActiveFilter());
            MoveActiveFilterUpCommand = new RelayCommand(a => MoveActiveFilterUp(), b => HasActiveFilterSelected());
            MoveActiveFilterDownCommand = new RelayCommand(a => MoveActiveFilterDown(), b => HasActiveFilterSelected());
            DeleteAllActiveFiltersCommand = new RelayCommand(a => DeleteAllActiveFilters(), b => HasActiveFilters());
        }

        public void AddToActiveFilter()
        {
            if (SelectedInactiveFilter < 0)
                return;

            ActiveFilters.Add(InactiveFilters[SelectedInactiveFilter]);
            OnPropertyChanged("ActiveFilters");
        }

        public void RemoveFromActiveFilter()
        {
            if (SelectedActiveFilter < 0)
                return;

            ActiveFilters.RemoveAt(SelectedActiveFilter);
            OnPropertyChanged("ActiveFilters");


            if (SelectedActiveFilter >= ActiveFilters.Count - 1)
                SelectedActiveFilter--;
        }

        public void MoveActiveFilterUp()
        {
            if (SelectedActiveFilter <= 0)
                return;

            ActiveFilters.Move(SelectedActiveFilter, SelectedActiveFilter - 1);
        }

        public void MoveActiveFilterDown()
        {
            if (SelectedActiveFilter >= ActiveFilters.Count - 1)
                return;

            ActiveFilters.Move(SelectedActiveFilter, SelectedActiveFilter + 1);
        }

        public void DeleteAllActiveFilters()
        {
            if(System.Windows.MessageBox.Show("Are you sure you want to remove all active filters?", "Remove all active filters", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            {
                ActiveFilters.Clear();
                SelectedActiveFilter = -1;
            }
        }

        public bool HasActiveFilterSelected()
        {
            return SelectedActiveFilter != -1;
        }

        public bool HasActiveFilters()
        {
            return ActiveFilters.Count > 0;
        }

        #region Properties
        private ObservableCollection<FilterType> _inactiveFilters;
        public ObservableCollection<FilterType> InactiveFilters
        {
            get { return _inactiveFilters; }
            set { _inactiveFilters = value; }
        }

        private int _selectedInactiveFilter;
        public int SelectedInactiveFilter
        {
            get { return _selectedInactiveFilter; }
            set
            {
                _selectedInactiveFilter = value;
                OnPropertyChanged("SelectedInactiveFilter");
            }
        }

        private ObservableCollection<FilterType> _activeFilters;
        public ObservableCollection<FilterType> ActiveFilters
        {
            get { return _activeFilters; }
            set { _activeFilters = value; }
        }

        private int _selectedActiveFilter;
        public int SelectedActiveFilter
        {
            get { return _selectedActiveFilter; }
            set
            {
                _selectedActiveFilter = value;
                OnPropertyChanged("SelectedActiveFilter");
            }
        }

        private RelayCommand _addToActiveFilterCommand;
        public RelayCommand AddToActiveFilterCommand
        {
            get { return _addToActiveFilterCommand; }
            private set { _addToActiveFilterCommand = value; }
        }

        private RelayCommand _removeFromActiveFilterCommand;
        public RelayCommand RemoveFromActiveFilterCommand
        {
            get { return _removeFromActiveFilterCommand; }
            private set { _removeFromActiveFilterCommand = value; }
        }

        private RelayCommand _moveActiveFilterUpCommand;
        public RelayCommand MoveActiveFilterUpCommand
        {
            get { return _moveActiveFilterUpCommand; }
            private set { _moveActiveFilterUpCommand = value; }
        }

        private RelayCommand _moveActiveFilterDownCommand;
        public RelayCommand MoveActiveFilterDownCommand
        {
            get { return _moveActiveFilterDownCommand; }
            private set { _moveActiveFilterDownCommand = value; }
        }

        private RelayCommand _deleteAllActiveFiltersCommand;
        public RelayCommand DeleteAllActiveFiltersCommand
        {
            get { return _deleteAllActiveFiltersCommand; }
            private set { _deleteAllActiveFiltersCommand = value; }
        }
        #endregion Properties
    }
}