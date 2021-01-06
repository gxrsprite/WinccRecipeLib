
using Ruifei.Models;
using Ruifei.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ruifei.AppMain
{
    public class BatchDataListViewModel : ViewModelBase
    {

        public BatchDataListViewModel()
        {
            ComputerList = new ObservableRangeCollection<string>();
            DatabaseEnums = new ObservableRangeCollection<string>();
            //PageSize = 10000;
            //PageSizeList = new ObservableCollection<int>(new List<int>() { 100, 1000, 10000, 100000, 1000000 });

            SelectItem[] interpolArray = new SelectItem[]
            {
                new SelectItem(0,"Without Aggreg."),
                new SelectItem(1,"FIRST"), new SelectItem(2,"LAST"),
                new SelectItem(3,"MIN"), new SelectItem(4,"MAX"),
                new SelectItem(5,"AVG"), new SelectItem(6,"SUM"),
                new SelectItem(7,"COUNT"),
                new SelectItem(257,"FIRST_INTERPOLATED"), new SelectItem(258,"LAST_INTERPOLATED"),
                new SelectItem(259,"MIN_INTERPOLATED"), new SelectItem(260,"MAX_INTERPOLATED"),
                new SelectItem(261,"AVG_INTERPOLATED"), new SelectItem(262,"SUM_INTERPOLATED"),
                new SelectItem(263,"COUNT_INTERPOLATED")
            };

            InterpolList = new ObservableRangeCollection<SelectItem>(interpolArray);
        }

        public ObservableRangeCollection<string> ComputerList { get; set; }
        public string Computer { get; set; } = ".\\WINCC";
        public ObservableRangeCollection<string> DatabaseEnums { get; set; }
        public string DatabaseName { get; set; }
        public List<WinccData> BatchDataList { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
        public string ArchiveName { get; set; }
        public List<string> ArchiveNameList { get; set; }
        public SelectItem SelectedInterpol { get; set; }
        public ObservableRangeCollection<SelectItem> InterpolList { get; set; }

        public ObservableRangeCollection<int> PageSizeList { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

    }
}
