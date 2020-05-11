using IRepository;
using IServices;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CostTypeServices : BaseServices<CostType>, ICostTypeServices
    {
        private readonly ICostTypeRepository _costTypeRepository;

        public CostTypeServices(ICostTypeRepository costTypeRepository)
        {
            _costTypeRepository = costTypeRepository;
            base.CurrentRepository = _costTypeRepository;
        }
    }
}