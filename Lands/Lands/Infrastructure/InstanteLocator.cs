using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.Infrastructure
{
    using ViewModels;

    class InstanteLocator
    {

        #region Properties

        public MainViewModel Main
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public InstanteLocator()
        {
            this.Main = new MainViewModel();
        }

        #endregion
    }
}
