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
    public partial class CityController : BasePublicController
	{
		#region Fields

        private readonly ICityService _cityService;
        private readonly ICountyService _countyService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

	    #endregion

		#region Constructors

        public CityController(ICityService cityService,
            ICountyService countyService, 
            ILocalizationService localizationService, 
            IWorkContext workContext,
            ICacheManager cacheManager)
		{
            this._cityService = cityService;
            this._countyService = countyService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
		}

        #endregion

        #region States / provinces

        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCountysByCityId(string cityId, bool addSelectCountyItem)
        {
            //this action method gets called via an ajax request
            if (String.IsNullOrEmpty(cityId))
                throw new ArgumentNullException("cityId");

            string cacheKey = string.Format(ModelCacheEventConsumer.STATEPROVINCES_BY_COUNTRY_MODEL_KEY, cityId, addSelectCountyItem, _workContext.WorkingLanguage.Id);
            var cacheModel = _cacheManager.Get(cacheKey, () =>
            {
                var city = _cityService.GetCityById(Convert.ToInt32(cityId));
                var county = _countyService.GetCountyByCityId(city != null ? city.Id : 0).ToList();
                var result = (from s in county
                              select new { id = s.Id, name = s.GetLocalized(x => x.Name) })
                              .ToList();


                if (city == null)
                {
                    //country is not selected ("choose country" item)
                    if (addSelectCountyItem)
                    {
                        result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.SelectCounty") });
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
                        if (addSelectCountyItem)
                        {
                            result.Insert(0, new { id = 0, name = _localizationService.GetResource("Address.SelectCounty") });
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
