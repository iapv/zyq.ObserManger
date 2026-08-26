using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using zyq.ObserManger.Data;
using zyq.ObserManger.Services;

namespace zyq.ObserManger.ViewModel
{
    public class MainWindowVm : INotifyPropertyChanged
    {
        private readonly IMonitoringDataSource _dataSource = new ModbusRtuDataSource();

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowVm()
        {
            // Set the initial view to HomeView
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();   // 库/表不存在就自动建
            }
            CurrentView = new MonitorAViewModel(_dataSource);
        }

        public void ShowMonitorA() => CurrentView = new MonitorAViewModel(_dataSource);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
