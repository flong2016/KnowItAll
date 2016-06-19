using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;

namespace Nop.Services.Directory
{
    public  partial class CityService:ICityService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {1} : country ID
        /// </remarks>
        private const string CITYS_ALL_KEY = "Nop.city.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CITYS_PATTERN_KEY = "Nop.city.";

        #endregion

        #region Fields

        private readonly IRepository<City> _cityRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="stateProvinceRepository">State/province repository</param>
        /// <param name="eventPublisher">Event published</param>
        public CityService(ICacheManager cacheManager,
            IRepository<City> cityRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _cityRepository = cityRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion
        public void DeleteCity(City city)
        {
            if(city==null)
                throw new ArgumentNullException("city");
            _cityRepository.Delete(city);
            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);
            _eventPublisher.EntityDeleted(city);
        }

        public City GetCityById(int cityId)
        {
            if (cityId == 0)
            {
                return null;
            }
            return _cityRepository.GetById(cityId);
        }

        public City GetByAbbreviation(string abbreviation)
        {
            var query = from sp in _cityRepository.Table
                where sp.Abbreviation == abbreviation
                select sp;
            var city = query.FirstOrDefault();
            return city;
        }

        public IList<City> GetCityByStateProvinceId(int stateProvinceId, bool showHidden = false)
        {
            string key = string.Format(CITYS_ALL_KEY, stateProvinceId);
            return _cacheManager.Get(key, () =>
            {
                var query= from sp in _cityRepository.Table 
                                 orderby  sp.DisplayOrder
                           where sp.StateProvinceId == stateProvinceId &&
                       (showHidden || sp.Published)
                           select sp;
                var city = query.ToList();
                return city;
            });
        }

        public void InsertCity(City city)
        {
            if (city == null)
                throw new ArgumentNullException("city");

            _cityRepository.Insert(city);

            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(city);
        }

        public void UpdateCity(City city)
        {
            if (city == null)
                throw new ArgumentNullException("city");

            _cityRepository.Update(city);

            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(city);
        }
    }
}
