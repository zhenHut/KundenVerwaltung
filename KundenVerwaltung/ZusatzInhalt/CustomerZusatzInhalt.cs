using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ERPManager.Collection.ZusatzInhalt;
using ERPManager.Services;
using GeneratedORM;

namespace KundenVerwaltung.ZusatzInhalt
{
    class CustomerZusatzInhalt : ZusatzInhaltBase
    {
        #region Constructor

        public CustomerZusatzInhalt(Customers entity)
        {
            _entity = entity;

            PropertyChanged += Entity_PropertyChanged;
            {
                if (_entity == null)
                    return;

                _entity.OnPropertyChanged("ZusatzInhalt"); 
            }
        }

        #endregion


        #region Fields

        private Customers _entity;

        private string _customerTypeName;
        #endregion

        #region Properties

        public string CustomerTypeName
        {
            get
            {
                if (_customerTypeName == null)
                {
                    
                    _customerTypeName = LadeCustomerTypeName();
                }

                return _customerTypeName;
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = LadeCustomerName();
                }

                return _name;
            }
            set { }
        }

        #endregion

        #region Methods

        private string LadeCustomerTypeName()
        {
            string sql = "SELECT type_name FROM \"Customer_types\" WHERE id = @id";
            var parameter = new Dictionary<string, object>
                {
                { "id", _entity.customer_type_id }
                };

            return QueryManager.StaticExecuteScalar<string>(sql, parameter);
        }

        private string LadeCustomerName() 
        {
            string sql = "";
            var parameter = new Dictionary<string, object> { { "id", _entity.customer_ref_id } };

            if (_entity.customer_type_id == 1)  // Firma
            {
                sql = "SELECT company_name FROM \"Companies\" WHERE id = @id";
            }
            else if (_entity.customer_type_id == 2)  // Privatperson
            {
                sql = "SELECT first_name || ' ' || last_name FROM \"Private_customers\" WHERE id = @id";
            }
            else
            {
                return "Unbekannter Typ";
            }

            return QueryManager.StaticExecuteScalar<string>(sql, parameter);
        }


        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_entity.customer_ref_id):
                    PropertyReload(Name);
                    break;

                case nameof(_entity.customer_type_id):
                    PropertyReload(CustomerTypeName);
                    break; 
            }
        }


        #endregion
    }
}
