using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using zyq.ObserManger.Services;
using zyq.ObserManger.Models;
using System.Collections.ObjectModel;

namespace zyq.ObserManger.ViewModel
{
    public class PanelAdapterVm : INotifyPropertyChanged
    {
        
        private readonly IMonitoringDataSource _monitoringDataSource;
        private readonly DispatcherTimer _timer;


        private ObservableCollection<PanelAdapterModel> _panelAdapterModels = new ObservableCollection<PanelAdapterModel>();
        public ObservableCollection<PanelAdapterModel> PanelAdapterModels
        {
            get => _panelAdapterModels;
            set
            {
                if (_panelAdapterModels != value)
                {
                    _panelAdapterModels = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _itemName = "";
        public string ItemName
        {
            get => _itemName;
            set => _itemName = value;
        }

        private int _itemValue = 0;
        public int ItemValue
        {
            get => _itemValue;
            set => _itemValue = value;
        }



        public PanelAdapterVm(IMonitoringDataSource monitoringDataSource)
        {
            _monitoringDataSource = monitoringDataSource;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            _timer.Tick += async (s, e) => await Update();
            _ = Update();
            _timer.Start();
        }

        private async Task Update()
        {
            try
            {
                var data = await _monitoringDataSource.GetPanelAdapterModelAsync();
                if (data == null) return;
                while(_panelAdapterModels.Count > data.Count)
                {
                    _panelAdapterModels.RemoveAt(_panelAdapterModels.Count - 1);
                }
                while (_panelAdapterModels.Count < data.Count) 
                {
                    _panelAdapterModels.Add(new PanelAdapterModel("",0));
                }

                for (int i = 0; i < data.Count; i++)
                {
                    _panelAdapterModels[i].ItemName = data[i].ItemName;
                    _panelAdapterModels[i].ItemValue = data[i].ItemValue;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PanelAdapter错误: " + ex);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}