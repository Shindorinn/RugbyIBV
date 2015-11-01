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

            // Initialization - Buttons/Commands
            AddToActiveFilterCommand = new RelayCommand(a => AddToActiveFilter());
            RemoveFromActiveFilterCommand = new RelayCommand(a => RemoveFromActiveFilter());
        }

        public void AddToActiveFilter()
        {
            if (SelectedInactiveFilter == null)
                return;

            ActiveFilters.Add(SelectedInactiveFilter);
            //InactiveFilters.Remove(SelectedInactiveFilter);
            //SelectedInactiveFilter = null;
            OnPropertyChanged("ActiveFilters");
            //UpdateLists();
        }

        public void RemoveFromActiveFilter()
        {
            if (SelectedActiveFilter == null)
                return;

            //InactiveFilters.Add(SelectedActiveFilter);
            ActiveFilters.Remove(SelectedActiveFilter);
            SelectedActiveFilter = null;
            OnPropertyChanged("ActiveFilters");
            //UpdateLists();
        }

        private void UpdateLists()
        {
            OnPropertyChanged("InactiveFilters");
            OnPropertyChanged("ActiveFilters");
        }

        #region Properties
        private ObservableCollection<FilterType> _inactiveFilters;
        public ObservableCollection<FilterType> InactiveFilters
        {
            get { return _inactiveFilters; }
            set { _inactiveFilters = value; }
        }

        private FilterType _selectedInactiveFilter;
        public FilterType SelectedInactiveFilter
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

        private FilterType _selectedActiveFilter;
        public FilterType SelectedActiveFilter
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
        #endregion Properties
    }
}