using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zyq.ObserManger.Models
{
    public class PanelAdapterModel : INotifyPropertyChanged
    {

        public PanelAdapterModel(string itemName, int itemValue)
        {
            _itemName = itemName;
            _itemValue = itemValue;
        }
        public string ItemName { 
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged();
            }
            
        }

        private string _itemName;


        public int ItemValue
        {
            get => _itemValue;
            set
            {
                _itemValue = value;
                OnPropertyChanged();
            }
        }

        private int _itemValue;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
