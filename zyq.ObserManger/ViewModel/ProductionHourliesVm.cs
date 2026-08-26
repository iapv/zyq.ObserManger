using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zyq.ObserManger.Services;
using LiveCharts;
namespace zyq.ObserManger.ViewModel
{
    public class ProductionHourliesVm : INotifyPropertyChanged
    {
        private readonly CapacitySeries capacitySeries = new CapacitySeries();

        private SeriesCollection collection;

        

        public SeriesCollection CapacitySeriesData
        {
            get => collection;
            set {
                collection = value;
                OnPropertyChanged();
            }
        }

        public ProductionHourliesVm()
        {
            CapacitySeriesData = capacitySeries.Build(DateTime.Today);
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));





    }
}
