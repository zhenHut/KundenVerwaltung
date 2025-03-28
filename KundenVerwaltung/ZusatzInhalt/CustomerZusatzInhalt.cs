using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using ERPManager.ZusatzInhalt;
using ERPManager.Services;
using GeneratedORM;

namespace KundenVerwaltung.ZusatzInhalt
{
    class CustomerZusatzInhalt : ZusatzInhaltBase<Customers>
    {
        #region Constructor

        public CustomerZusatzInhalt(Customers entity) : base(entity)
        {
        }

        #endregion

        #region Fields

        private string? _customerTypeName;
        private string? _name;

        #endregion

        #region Properties

        public string? CustomerTypeName => _customerTypeName ??= LadeCustomerTypeName();

        public string? Name
        {
            get => _name ??= LadeCustomerName();
            set { }
        }

        #endregion

        #region Methods

        private string? LadeCustomerTypeName()
        {
            string sql = "SELECT type_name FROM \"Customer_types\" WHERE id = @id";
            var parameter = new Dictionary<string, object>
                {
                    { "id", Entity.customer_type_id }
                };

            return QueryManager.StaticExecuteScalar<string>(sql, parameter);
        }

        private string? LadeCustomerName()
        {
            string sql = "";
            var parameter = new Dictionary<string, object> { { "id", Entity.customer_ref_id } };

            if (Entity.customer_type_id == 1)  // Firma
            {
                sql = "SELECT company_name FROM \"Companies\" WHERE id = @id";
            }
            else if (Entity.customer_type_id == 2)  // Privatperson
            {
                sql = "SELECT first_name || ' ' || last_name FROM \"Private_customers\" WHERE id = @id";
            }
            else
            {
                return "Unbekannter Typ";
            }

            return QueryManager.StaticExecuteScalar<string>(sql, parameter);
        }

        protected override void OnEntityPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Entity.customer_ref_id):
                    OnPropertyChanged(nameof(Name));
                    break;

                case nameof(Entity.customer_type_id):
                    OnPropertyChanged(nameof(CustomerTypeName));
                    break;
            }
        }

        #endregion
    }
}
