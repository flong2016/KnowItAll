using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Services.Events;

namespace Nop.Services.Directory
{
    public  partial class CountyService:ICountyService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {1} : country ID
        /// </remarks>
        private const string CITYS_ALL_KEY = "Nop.county.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CITYS_PATTERN_KEY = "Nop.county.";

        #endregion

        #region Fields

        private readonly IRepository<County> _countyRepository;
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
        public CountyService(ICacheManager cacheManager,
            IRepository<County> countyRepository,
            IEventPublisher eventPublisher)
        {
            _cacheManager = cacheManager;
            _countyRepository = countyRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion
        public void DeleteCounty(County county)
        {
            if (county == null)
                throw new ArgumentNullException("county");
            _countyRepository.Delete(county);
            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);
            _eventPublisher.EntityDeleted(county);
        }

        public County GetCountyById(int cityId)
        {
            if (cityId == 0)
            {
                return null;
            }
            return _countyRepository.GetById(cityId);
        }

        public County GetByAbbreviation(string abbreviation)
        {
            var query = from sp in _countyRepository.Table
                where sp.Abbreviation == abbreviation
                select sp;
            var city = query.FirstOrDefault();
            return city;
        }

        public IList<County> GetCountyByCityId(int cityId, bool showHidden = false)
        {
            string key = string.Format(CITYS_ALL_KEY, cityId);
            return _cacheManager.Get(key, () =>
            {
                var query = from sp in _countyRepository.Table 
                                 orderby  sp.DisplayOrder
                            where sp.CityId == cityId &&
                       (showHidden || sp.Published)
                           select sp;
                var city = query.ToList();
                return city;
            });
        }
        public void InsertCounty(County county)
        {
            if (county == null)
                throw new ArgumentNullException("county");

            _countyRepository.Insert(county);

            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(county);
        }

        public void UpdateCounty(County county)
        {
            if (county == null)
                throw new ArgumentNullException("county");

            _countyRepository.Update(county);

            _cacheManager.RemoveByPattern(CITYS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(county);
        }
    }
}
