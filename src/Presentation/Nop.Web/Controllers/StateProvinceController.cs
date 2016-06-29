using System;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Infrastructure.Cache;

namespace Nop.Web.Controllers
{
    //JXzfl
    public partial class StateProvinceController : BasePublicController
	{
		#region Fields

        private readonly ICityService _cityService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

	    #endregion

		#region Constructors

        public StateProvinceController(ICityService cityService, 
            IStateProvinceService stateProvinceService, 
            ILocalizationService localizationService, 
            IWorkContext workContext,
            ICacheManager cacheManager)
		{
            this._cityService = cityService;
            this._stateProvinceService = stateProvinceService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
		}

        #endregion

        #region States / provinces

        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCitysByStateProvinceId(string stateProvinceId, bool addSelectCityItem)
        {
            //this action method gets called via an ajax request
            if (String.IsNullOrEmpty(stateProvinceId))
                throw new ArgumentNullException("stateProvinceId");

            string cacheKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, stateProvinceId, addSelectCityItem, _workContext.WorkingLanguage.Id);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                var states = _stateProvinceService.GetStateProvinceById(Convert.ToInt32(stateProvinceId));
                var citys = _cityService.GetCityByStateProvinceId(states != null ? states.Id : 0).ToList();
                var result = (from s in citys
                              select new { id = s.Id, name = s.GetLocalized(x => x.Name) })
                              .ToList();


                if (states == null)
                {
                    //country is not selected ("choose country" item)
                    if (addSelectCityItem)
                    {
                        result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.SelectCity") });
                    }
                    else
                    {
                        result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.OtherNonUS") });
                    }
                }
                else
                {
                    //some country is selected
                    if (result.Count == 0)
                    {
                        //country does not have states
                        result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.OtherNonUS") });
                    }
                    else
                    {
                        //country has some states
                        if (addSelectCityItem)
                        {
                            result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.SelectCity") });
                        }
                    }
                }

                return result;
            });
            
            return Json(cacheModel, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
