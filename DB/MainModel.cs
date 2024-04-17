using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.DB
{
    public class MainModel : BindableBase
    {
        private readonly RestService db;


        public MainModel()
        {
            db = new RestService();


            ReloadPlants();
        }

        private async void ReloadPlants()
        {
            var items = await db.GetPlantsAsync();
            var result = items;
            if (IsSorted)
            {
                if (IsByKind)
                {
                    result = items.OrderBy(x => x.PlantKind).ToList();
                }
                else if (IsByUmovi)
                {
                    result = items.OrderBy(x => x.UmoviZrost).ToList();
                }
                else if (IsByPeriod)
                {
                    result = items.OrderBy(x => x.PeriodCvit).ToList();
                }
            }
            else
            {
                result = items.OrderBy(x => x.Id).ToList();
            }

            Plants.Clear();
            result.ForEach(x =>
            {
                Plants.Add(x);
            });
        } 
        
                public ObservableCollection<Plant> Plants { get; set; } = new ObservableCollection<Plant>();


        private bool _isSorted;


        public bool IsSorted
        {
            get => _isSorted;
            set
            {
                SetProperty(ref _isSorted, value);
                ReloadPlants();
            }
        }


        private bool _isByKind = true;


        public bool IsByKind
        {
            get => _isByKind;
            set
            {
                SetProperty(ref _isByKind, value);
                ReloadPlants();
            }
        }


        private bool _isByUmovi;


        public bool IsByUmovi
        {
            get => _isByUmovi;
            set
            {
                SetProperty(ref _isByUmovi, value);
                ReloadPlants();
            }
        }
        private bool _isByPeriod;
        public bool IsByPeriod
        {
            get => _isByPeriod;
            set
            {
                SetProperty(ref _isByPeriod, value);
                ReloadPlants();
            }
        }
       

        private string _newPlantKind;
        public string NewPlantKind
        {
            get => _newPlantKind;
            set => SetProperty(ref _newPlantKind, value);
        }


        private string _newUmiviZrost;
        public string NewUmiviZrost
        {
            get => _newUmiviZrost;
            set => SetProperty(ref _newUmiviZrost, value);
        }


        private string _newPeriodZvit;
        public string NewPeriodZvit
        {
            get => _newPeriodZvit;
            set => SetProperty(ref _newPeriodZvit, value);
        }


        private string _newWaterNeeds;
        public string NewWaterNeeds
        {
            get => _newWaterNeeds;
            set => SetProperty(ref _newWaterNeeds, value);
        }
        private string _newDobrivaNeeds;
        public string NewDobrivaNeeds
        {
            get => _newDobrivaNeeds;
            set => SetProperty(ref _newDobrivaNeeds, value);
        }

        public async void SaveNewPlant()
        {
            await db.CreatePlantAsync(new Plant
            {
                PlantKind = NewPlantKind,
                UmoviZrost = NewUmiviZrost,
                PeriodCvit = NewPeriodZvit,
                WaterNeeds = NewWaterNeeds,
                DobrivaNeeds = NewDobrivaNeeds
            });
            ReloadPlants();
            ClearNewPlant();
        }


        public void ClearNewPlant()
        {
            NewPlantKind = "";
            NewUmiviZrost = "";
            NewPeriodZvit = "";
            NewWaterNeeds = "";
            NewDobrivaNeeds = "";

        }

    }
}
    

