using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.ViewModel.Common
{
    public class EntitySelection<TEntity> : BindableBase where TEntity : class, new()
    {
        private TEntity _entity;

        public TEntity Entity
        {
            get
            {
                if (_entity == null)
                    _entity = new TEntity();

                return _entity;
            }
            set
            {
                _entity = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}
